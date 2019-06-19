using System;
using System.Threading.Tasks;

using LogParser.BLL.Services.Interfaces;
using LogParser.DataModels.Models;

using Microsoft.AspNetCore.Mvc;

namespace LogParser.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : BaseController
    {
        private readonly ILogsService _service;
        public LogsController(ILogsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ClientResponse>> Get([FromQuery] int offset=0, [FromQuery] int limit=10, [FromQuery] DateTimeOffset? start = null,[FromQuery] DateTimeOffset? end = null)
        {
            var result = await _service.GetLogsAsync(offset, limit, start, end);

            return ResponseData(result);
        }
    }
}
