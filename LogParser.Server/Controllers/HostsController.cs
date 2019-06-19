using System;
using System.Threading.Tasks;

using LogParser.BLL.Services.Interfaces;
using LogParser.DataModels.Models;

using Microsoft.AspNetCore.Mvc;

namespace LogParser.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostsController : BaseController
    {
        private readonly IHostService _service;
        public HostsController(IHostService service)
        {
            _service = service;
        }

        [HttpGet]
        public  async Task<ActionResult<ClientResponse>> Get([FromQuery] int amount=10, [FromQuery] DateTimeOffset? start = null,[FromQuery] DateTimeOffset? end = null)
        {
            var result = await _service.GetTopHosts(amount, start, end);

            return ResponseData(result);
        }
    }
}