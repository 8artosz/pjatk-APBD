using FirsApi.Models;
using FirsApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirsApi.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
      public async Task<IActionResult> GetStudents()
        {
          
            return Ok(_dbService.GetStudent());
        }
        [HttpGet("{idStudent}")]
        public async Task<IActionResult> GetStudentsById([FromRoute] string idStudent)
        {
            return Ok(_dbService.GetStudent(idStudent));
        }

        [HttpPut("{idStudent}")]
        public async Task<IActionResult> PutStudentsById([FromRoute] string idStudent, [FromBody] Student s)
        {
            if (s != null)
                return Ok(_dbService.PutStudent(idStudent, s));
            else return NotFound("Wrong input");
        }

        [HttpPost]
        public async Task<IActionResult> PostStudent([FromBody] Student s)
        {
            if (s == null)
                return NotFound("Wrong input");
            else if (_dbService.PostStudent(s) == false)
                return BadRequest("Wrong input");
            else return Ok(_dbService.PostStudent(s));
        }
        [HttpDelete("{idStudent}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] string idStudent)
        {
            return Ok(_dbService.DeleteStudent(idStudent));
        }
    }
}
