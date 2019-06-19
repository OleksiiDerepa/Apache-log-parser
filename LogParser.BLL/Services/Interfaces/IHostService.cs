using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogParser.BLL.Services.Interfaces
{
    public interface IHostService
    {
        Task<ICollection<string>> GetTopHosts(int amount, DateTimeOffset? start, DateTimeOffset? end);
    }
}