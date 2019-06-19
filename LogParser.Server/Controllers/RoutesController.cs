using System;
using System.Threading.Tasks;

using LogParser.BLL.Services.Interfaces;
using LogParser.DataModels.Models;

using Microsoft.AspNetCore.Mvc;

namespace LogParser.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : BaseController
    {
        private readonly IRouteService _service;
        public RoutesController(IRouteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ClientResponse>> Get([FromQuery] int amount=10, [FromQuery] DateTimeOffset? start = null,[FromQuery] DateTimeOffset? end = null)
        {
            var result = await _service.GetTopRoutes(amount, start, end);
            
            return ResponseData(result);
        }
    }
}