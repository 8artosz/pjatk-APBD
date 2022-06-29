using cw11.Server.Service;
using cw11.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw11.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet("students")]
        public async Task<IActionResult> GetStudents()
        {
            return Ok(await _dbService.GetStudentsAsync());

        }
        [HttpGet("students/{id}")]
        public async Task<IActionResult> GetStudent([FromRoute] int id)
        {
            Student tmp = await _dbService.GetStudentAsync(id);
            if (tmp != null)
                return Ok(tmp);
            else return BadRequest();
        }

        [HttpDelete("students/delete/{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {

            return Ok(await _dbService.DeleteStudentAsync(id));
            
        }
    }
}
