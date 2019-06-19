using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using LogParser.DataModels.Models;

namespace LogParser.BLL.Services.Interfaces
{
    public interface ILogsService
    {
        Task<ICollection<RequestUrlDto>> GetLogsAsync(int offset, int limit, DateTimeOffset? start = null, DateTimeOffset? end = null);
    }
}