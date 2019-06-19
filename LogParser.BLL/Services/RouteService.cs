using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LogParser.BLL.Services.Interfaces;
using LogParser.DAL.Context;
using LogParser.Infrastructure.Validation;
using Microsoft.EntityFrameworkCore;

namespace LogParser.BLL.Services
{
    public class RouteService : BaseService, IRouteService
    {
        public RouteService(ApacheLogDbContext context) : base(context)
        {
        }

        public async Task<ICollection<string>> GetTopRoutes(int amount, DateTimeOffset? start, DateTimeOffset? end)
        {
            Require.IsTrue(amount > 0, () => $"{nameof(amount)} should be more that 0");
            Require.IsValid(start, end, ()=> $"{nameof(end)} should be greeter than {nameof(start)}");

            var result = await _context.Routes
                .Join(_context.RequestUrls
                        .Where(x => start == null || x.Date >= start)
                        .Where(x => end == null || x.Date <= end)
                        .GroupBy(x => x.RouteId)
                        .Select(x => new { Id = x.Key, Count = x.Count() })
                        .OrderByDescending(x => x.Count).Take(amount),
                    p => p.Id,
                    a => a.Id,
                    (p, a) => p.Name)
                .ToArrayAsync();

            return result;
        }
    }
}