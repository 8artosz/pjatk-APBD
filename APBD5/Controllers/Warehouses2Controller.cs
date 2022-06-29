using cw5.Models;
using cw5.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.Controllers
{
    [Route("api/warehouses2")]
    [ApiController]
    public class Warehouses2Controller : ControllerBase
    {
        private readonly IDbService _dbService;
        public Warehouses2Controller(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> PostProductByProc([FromBody] NewProduct product)
        {
                await _dbService.PostProductByStoredProcedureAsync(product);
                return Ok();
        }
    }
}
