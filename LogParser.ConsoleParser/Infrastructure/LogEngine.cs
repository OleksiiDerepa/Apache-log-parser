using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LogParser.Common.Extensions;
using LogParser.ConsoleParser.Extensions;
using LogParser.DAL.Context;
using LogParser.DAL.Entities;
using LogParser.DataModels.Models;
using LogParser.Infrastructure.Http.Client;
using LogParser.Infrastructure.Readers;
using LogParser.Infrastructure.Validation;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

namespace LogParser.ConsoleParser.Infrastructure
{
    public class LogEngine
    {
        private readonly ApacheLogDbContext _context;
        private readonly IInnerHttpClient _httpClient;
        private readonly string _filePath;
        private readonly string _accessKey;
        private readonly string _outerIpServiceUrl;

        private List<string> _badIp = new List<string>();
        private List<RestMethod> _restMethods;
        private List<ProtocolVersion> _protocolVersions;
        private List<Protocol> _protocols;
        private List<RestStatusCode> _restStatusCodes;

        readonly int _counterForSave;
        long _lineCounter;

        public LogEngine()
        {
            JsonConfigReader configReader = new JsonConfigReader(JsonConfigReader.ConfigFileName);
            Require.NotNull(configReader, nameof(configReader));
            
            Console.WriteLine("data reading from config file");
            var connectionString = configReader.GetConnectionString("ApacheLogs");
            _filePath = configReader.GetValue<string>("FilePath");
            _accessKey = configReader.GetValue<string>("AccessKey");
            _counterForSave  = configReader.GetValue<int>("CounterForSave");
            _outerIpServiceUrl = configReader.GetValue<string>("OuterIpServiceUrl");
            _httpClient = new InnerHttpClient();

            Require.NotEmpty(connectionString, ()=>$"{connectionString} should be specified in the appsettings.json file");
            Require.NotNull(_httpClient, nameof(_httpClient));
            Require.NotEmpty(_filePath,"'FilePath' should be specified in the appsettings.json file");
            Require.NotEmpty(_accessKey,"'AccessKey' should be specified in the appsettings.json file");
            Require.NotEmpty(_outerIpServiceUrl,"'OuterIpServiceUrl' should be specified in the appsettings.json file");
            Require.IsTrue(_counterForSave > 0,"'CounterForSave' should be specified in the appsettings.json file");
            
            Console.WriteLine("data base connecting");

            var optionsBuilder = new DbContextOptionsBuilder<ApacheLogDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            DbContextOptions<ApacheLogDbContext> options = optionsBuilder.Options;

            _context = new ApacheLogDbContext(options);
            Require.NotNull(_context, nameof(_context));

            _context.Initialize();
        }

        private static void ShowMessage(int left, int top, object message)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(message);
        }
        public async Task Run()
        {
            Require.IsTrue(File.Exists(_filePath),$"Specified file: {_filePath} does not exist");
            Console.Clear();
            List<string> lines = new List<string>(_counterForSave);
            ShowMessage(0, 0, "rows was read: 0");
            await ReadCommonData();
            using (var fileStream = new FileStream(_filePath, FileMode.Open, FileAccess.Read))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string currentLine;
                    while ((currentLine = streamReader.ReadLine()) != null)
                    {
                        ShowMessage(15, 0, _lineCounter);

                        if (_lineCounter++ % _counterForSave == 0 && lines.Any())
                        {
                            await ComputeDataAsync(lines);
                            lines = new List<string>(_counterForSave);
                        }
                        lines.Add(currentLine);
                    }
                }
            }
            await ComputeDataAsync(lines);
        }
        public async Task ComputeDataAsync(List<string> data)
        {
            //first data filtering 
            data = data.Where(RegexExtensions.IsCorrectRecord).ToList();
            //convert string to row item
            List<RowLogItem> rowLogItems = data
                .Select(x => new RowLogItem(x))
                .Select(LogExtensions.GetIP)
                .Where(x => x != null)
                .ToList();

            //get unique Ips from incoming data
            var uniqueIps = rowLogItems
                .Select(x => x.IP)
                .Distinct()
                .Except(_badIp)
                .ToList();

            //get all ips from db 
            var dbIps = _context.Addresses.Include(x=>x.Country)
                .Where(x => uniqueIps.Any(xx => xx.IsEqual(x.Ip)))
                .ToList();

            //exclude ips from collection
            uniqueIps = uniqueIps
                .Where(x => !dbIps.Any(mm => mm.Ip.IsEqual(x)))
                .ToList();

            List<IpGeoLocation> ipGeoLocations = GetIpGeoLocations(uniqueIps, dbIps);

            rowLogItems = rowLogItems
                .Select(LogExtensions.UpdateRootObject(ipGeoLocations))
                .Where(LogExtensions.CheckIpGeoLocation)
                .ToList();

            var routes = await _context.Routes
                .Where(route => rowLogItems.Any(r => r.Route.IsEqual(route.Name)))
                .ToListAsync();

            List<Country> countries = await _context.Countries
                .Where(country => rowLogItems.Any(r => r.IpGeoLocation.CountryName.IsEqual(country.Name)))
                .ToListAsync();

            var dataForSaveToDb = rowLogItems
                .Select(CreateRequestUrl(dbIps, routes, countries))
                .ToList();

            await _context.RequestUrls.AddRangeAsync(dataForSaveToDb);
            await _context.SaveChangesAsync();
        }

        private List<IpGeoLocation> GetIpGeoLocations(List<string> uniqueIps, List<Address> dbIps)
        {
            //get the ip position from the external server
            var ipGeoLocations = uniqueIps.AsParallel()
                .Select(currentIP => _httpClient.GetJson(currentIP, _outerIpServiceUrl, _accessKey))
                .Select(x => x?.Result)
                .Select(JsonConvert.DeserializeObject<IpGeoLocation>)
                .ToList();

            _badIp.AddRange(ipGeoLocations.Where(x => x.Latitude == null && x.Longitude == null).Select(x => x.Ip));
            _badIp = _badIp.Distinct().ToList();
            ipGeoLocations = ipGeoLocations.Where(LogExtensions.CheckIpGeoLocation).ToList();

            var additionalData = dbIps
                .Where(x => !ipGeoLocations.Any() || !ipGeoLocations.Any(ro => ro.Ip.IsEqual(x.Ip)))
                .Select(x => new IpGeoLocation { Ip = x.Ip, Latitude = x.Latitude, Longitude = x.Longitude , CountryName = x.Country.Name})
                .ToList();

            ipGeoLocations.AddRange(additionalData);
            return ipGeoLocations;
        }

        private Func<RowLogItem, RequestUrl> CreateRequestUrl(List<Address> dbips, List<Route> routes, List<Country> countries)
        {
            return rowLogItem => new RequestUrl
            {
                ProtocolVersion = _protocolVersions.GetProtocolVersionOrCreate(rowLogItem),
                Date = rowLogItem.Date.ConvertToDateTimeOffset(),
                Protocol = _protocols.GetProtocolOrCreate(rowLogItem),
                Method = _restMethods.GetMethod(rowLogItem),
                Params = rowLogItem.Params,
                Route = routes.GetRouteOrCreate(rowLogItem),
                IpAddress = dbips.GetIpAddressOrCreate(rowLogItem, countries),
                StatusCode = _restStatusCodes.GetStatusCode(rowLogItem),
                ResponseSize = rowLogItem.GetResponseSize()
            };
        }

        private async Task ReadCommonData()
        {
            _restMethods = await _context.RestMethods.ToListAsync();
            _protocolVersions = await _context.ProtocolVersions.ToListAsync();
            _protocols = await _context.Protocols.ToListAsync();
            _restStatusCodes = await _context.RestStatusCodes.ToListAsync();
        }
    }
}
