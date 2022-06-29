using kolokwium.DTOs.Requests;
using kolokwium.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium.Controllers
{
    [Route("api")]
    [ApiController]
    public class FireBrigadeController : ControllerBase
    {
        private readonly IDbService _dbService;
        public FireBrigadeController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("firefighter/{idFirefighter}")]
        public async Task<IActionResult> GetActions([FromRoute] int idFirefighter)
        {
            return Ok(await _dbService.GetActionsAsync(idFirefighter));
        }
        [HttpPost("firetruck")]
        public async Task<IActionResult> PostFireTruck ([FromBody] FireTruckDtoRequest firetruck)
        {
            var c = await _dbService.PostFireTruckAsync(firetruck);
            if (c)
                return Ok();
            else return BadRequest();
        }
    }
}
