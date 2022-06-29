using cw8.DTOs.Requests;
using cw8.Models;
using cw8.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw8.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDbService _dbService;
        public DoctorController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("doctor/{idDoctor}")]
        public async Task<IActionResult> GetDoctor([FromRoute] int idDoctor)
        {
            return Ok(await _dbService.GetDoctorAsync(idDoctor));
        }
        [HttpDelete("doctor/{idDoctor}")]
        public async Task<IActionResult> DeleteDoctor([FromRoute] int idDoctor)
        {

            var c = await _dbService.DeleteDoctorAsync(idDoctor);
            if (c == true)
                return Ok();
            else return BadRequest("Such person doesn't exist");
        }
        [HttpPost]
        public async Task<IActionResult> PostDoctor([FromBody] PostDoctorDtoRequest doctor)
        {
           var c = await _dbService.PostDoctorAsync(doctor);
            if (c == true)
                return Ok();
            else return BadRequest("Such person already exists");
        }

        [HttpPut("doctor/{idDoctor}")]
        public async Task<IActionResult> PutDoctor([FromBody] PostDoctorDtoRequest doctor, [FromRoute] int idDoctor)
        {
            var c = await _dbService.PutDoctorAsync(doctor,idDoctor);
            if (c == true)
                return Ok();
            else return BadRequest("Such person doesn't exist");

        }
        [HttpGet("/prescription/{idPrescription}")]
        public async Task<IActionResult> GetPrescription ([FromRoute] int idPrescription)
        {
            return Ok(await _dbService.GetPrescriptionAsync(idPrescription));
        }

    }
}
