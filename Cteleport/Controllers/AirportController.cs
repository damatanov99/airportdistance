using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cteleport.Interfaces;
using Cteleport.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cteleport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private IGeoService geoService;

        public AirportController(IGeoService geoService)
        {
            this.geoService = geoService;
        }

        [HttpPost("test")]
        public async Task<IActionResult> GetDistance([FromBody] DistanceRequest request)
        {
            try
            {
                var result = await geoService.CalculateDistance(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                var response = new ResponseResult<bool>();
                response.Error(ErrorCodes.Unknown, "Unhandeled error");
                return Ok(response);
            }
        }
       

    }
}