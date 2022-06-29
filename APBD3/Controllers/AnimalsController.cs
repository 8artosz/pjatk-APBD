using cw4.Models;
using cw4.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw4.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public AnimalsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAnimals([FromQuery] string orderBy)
        {
            return Ok(_dbService.GetAnimal(orderBy));
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Animal animal)
        {
            return Ok(_dbService.PostAnimal(animal));
        }
        [HttpPut("{idAnimal}")]
        public async Task<IActionResult> PutAnimal([FromRoute] int idAnimal, [FromBody] Animal animal)
        {
            if (_dbService.CheckAnimalById(idAnimal) == false)
                return NotFound("Not found animal");
            else return Ok(_dbService.PutAnimal(idAnimal, animal));
        }

        [HttpDelete("{idAnimal}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] int idAnimal)
        {
            if (_dbService.CheckAnimalById(idAnimal) == false)
                return NotFound("Not found animal");
            else return Ok(_dbService.DeleteAnimal(idAnimal));
        }

    }
}
