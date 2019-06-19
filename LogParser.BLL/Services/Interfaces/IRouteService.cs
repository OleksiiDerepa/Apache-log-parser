using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogParser.BLL.Services.Interfaces
{
    public interface IRouteService
    {
        Task<ICollection<string>> GetTopRoutes(int amount, DateTimeOffset? start, DateTimeOffset? end);
    }
}