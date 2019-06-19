using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using LogParser.Common.Extensions;
using LogParser.DataModels.Models;
using LogParser.DAL.Entities;
using LogParser.Infrastructure.Http.Client;

namespace LogParser.ConsoleParser.Extensions
{
    public static class LogExtensions
    {
        public static Address GetIpAddressOrCreate(
           this List<Address> ipAddresses,
            RowLogItem rowLogItem, 
           List<Country> countries)
        {
            Address ipAddress = ipAddresses.FirstOrDefault(x => x.Ip.IsEqual(rowLogItem.IP));

                if (ipAddress == null)
                { 

                    ipAddress = new Address
                    {
                        Ip = rowLogItem.IP,
                        Longitude = rowLogItem.IpGeoLocation.Latitude.Value,
                        Latitude = rowLogItem.IpGeoLocation.Latitude.Value,
                        Country = countries.GetCountryOrCreate(rowLogItem)
                    };
                    ipAddresses.Add(ipAddress);
                }

            return ipAddress;
        }

        public static Task<string> GetJson(this IInnerHttpClient httpClient,
            string ip, 
            string outerIpServiceUrl, 
            string accessKey)
            => httpClient.Execute(string.Format(outerIpServiceUrl, ip, accessKey));

        public static RowLogItem GetIP(this RowLogItem rowLogItem)
        {
            if (rowLogItem.HostNameOrAddress.IsIpv4() || rowLogItem.HostNameOrAddress.IsIpv6())
            {
                rowLogItem.IP = rowLogItem.HostNameOrAddress;
                return rowLogItem;
            }    
            try
            {
                rowLogItem.IP = Dns.GetHostEntry(rowLogItem.HostNameOrAddress)
                        .AddressList
                        .FirstOrDefault()
                        ?.ToString();

                return rowLogItem;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static RestStatusCode GetStatusCode(
            this IReadOnlyCollection<RestStatusCode> restStatusCodes,
            RowLogItem rowLogItem)
        {
            return Int32.TryParse(rowLogItem.StatusCode, out int sc)
                ? restStatusCodes.FirstOrDefault(x => x.Number == sc)
                : null;
        }
        public static Country GetCountryOrCreate(
            this List<Country> countries, 
            RowLogItem rowLogItem)
        {
            Country country = countries.FirstOrDefault(x => x.Name.IsEqual(rowLogItem.IpGeoLocation.CountryName));
            if (country == null)
            {
                country = new Country { Name = rowLogItem.IpGeoLocation.CountryName };
                countries.Add(country);
            }

            return country;
        }
        public static Route GetRouteOrCreate(
            this List<Route> routes, 
            RowLogItem rowLogItem)
        {
            Route route = routes.FirstOrDefault(x => x.Name.IsEqual(rowLogItem.Route));
            if (route == null)
            {
                route = new Route { Name = rowLogItem.Route };
                routes.Add(route);
            }

            return route;
        }
        public static RestMethod GetMethod(
            this IReadOnlyCollection<RestMethod> restMethods,
            RowLogItem rowLogItem)
        {
            return restMethods.FirstOrDefault(x => x.Name.IsEqual(rowLogItem.Method));
        }
        public static Protocol GetProtocolOrCreate(
           this IReadOnlyCollection<Protocol> protocols,
            RowLogItem rowLogItem)
        {
            return protocols.FirstOrDefault(x => x.Name.IsEqual(rowLogItem.Protocol))
                ?? new Protocol { Name = rowLogItem.Protocol };
        }

        public static ProtocolVersion GetProtocolVersionOrCreate(
           this IReadOnlyCollection<ProtocolVersion> protocolVersions,
            RowLogItem rowLogItem)
        {
            Double.TryParse(rowLogItem.ProtocolVersion, out double pvd);

            ProtocolVersion protocolVersion =
                protocolVersions.FirstOrDefault(x => x.Version.IsEqual(pvd))
                ?? new ProtocolVersion { Version = pvd };

            return protocolVersion;
        }

        public static bool CheckIpGeoLocation(RowLogItem rowLogItem)
        {
            return rowLogItem.IpGeoLocation.CheckIpGeoLocation();
        }

        public static bool CheckIpGeoLocation(this IpGeoLocation ipGeoLocation)
        {
            return ipGeoLocation != null 
                && !string.IsNullOrEmpty(ipGeoLocation.CountryName)
                && ipGeoLocation.Latitude.HasValue
                && ipGeoLocation.Longitude.HasValue;
        }

        public static Func<RowLogItem, RowLogItem> UpdateRootObject(List<IpGeoLocation> allRootObjects)
        {
            return x =>
            {
                x.IpGeoLocation = allRootObjects.FirstOrDefault(ro => ro.Ip == x.IP);
                return x;
            };
        }
        public static long GetResponseSize(this RowLogItem rowLogItem)
        {
            long.TryParse(rowLogItem.ResponseSize, out long size);
            return size;
        }

    }
}