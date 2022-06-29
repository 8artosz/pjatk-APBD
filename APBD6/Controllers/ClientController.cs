using cw7.Models.DTOs.Requests;
using cw7.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw7.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IDbService _dbService;
        public ClientController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("trips")]
        public async Task<IActionResult> GetClients()
        {
            return Ok(await _dbService.getClientsAsync());
        }
        [HttpDelete("clients/{idClient}")]
        public async Task<IActionResult> DeleteClient([FromRoute] int idClient)
        {

            var c = await _dbService.DeleteClientAsync(idClient);
            if (c == true)
                return Ok();
            else return BadRequest();
        }
        [HttpPost("trips/{idTrip}/clients")]
        public async Task<IActionResult> PostClient([FromRoute] int idTrip, [FromBody] InsertClientDtoRequest client)
        {
            var c = await _dbService.PostClientAsync(idTrip,client);
            if (c == true)
                return Ok();
            else return BadRequest();
        }

    }
}
