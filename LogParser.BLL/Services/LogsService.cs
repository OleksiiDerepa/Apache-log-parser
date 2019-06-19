using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using LogParser.BLL.Services.Interfaces;
using LogParser.DAL.Context;
using LogParser.DAL.Entities;
using LogParser.DataModels.Models;
using LogParser.Infrastructure.Validation;

using Microsoft.EntityFrameworkCore;

namespace LogParser.BLL.Services
{
    public class LogsService :  BaseService, ILogsService
    {
        private readonly IMapper _mapper;
        public LogsService(ApacheLogDbContext context, IMapper mapper) : base(context)
        {
            Require.NotNull(mapper, () => nameof(mapper));

            _mapper = mapper;
        }

        public async Task<ICollection<RequestUrlDto>> GetLogsAsync(int offset, int limit, DateTimeOffset? start = null, DateTimeOffset? end = null)
        {
            Require.IsTrue(offset >= 0, () => $"{nameof(offset)} should be more that 0 or equals");
            Require.IsTrue(limit > 0, () => $"{nameof(limit)} should be more that 0 or equals");
            Require.IsValid(start, end, ()=> $"{nameof(end)} should be greeter than {nameof(start)}");

            List<RequestUrl> result = await _context.RequestUrls
                .Include(x => x.Route)
                .Include(x => x.ProtocolVersion)
                .Include(x => x.IpAddress)
                    .ThenInclude(x=> x.Country)
                .Include(x => x.Method)
                .Include(x => x.Protocol)
                .Include(x => x.StatusCode)
                .Where(x=> start == null || x.Date >= start)
                .Where(x=> end == null || x.Date <= end)
                .OrderBy(x => x.Date)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            var convertedResult = _mapper.Map<ICollection<RequestUrlDto>>(result);

            return convertedResult;
        }

    }
}