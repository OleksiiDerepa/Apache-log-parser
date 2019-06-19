using System.Collections.Generic;

using LogParser.DataModels.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace LogParser.Server.Controllers
{
    public class BaseController : ControllerBase
    {
        public ActionResult<ClientResponse> ResponseData<T>(ICollection<T> data = null)
        {
            if(data == null ||!data.Any())
                return NotFound();

            var result = new ClientResponse
            {
                Count = data.Count,
                Data = data, 
            };

            return Ok(result);
        }
    }
}