using cw5.Models;
using cw5.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw5.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IDbService _dbService;
        public WarehousesController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] NewProduct product)
        {
            if (await _dbService.IsCorrectValueAsync(product) == -1)
                return NotFound("Provided IdProduct does not exist");
            else if (await _dbService.IsCorrectValueAsync(product) == -2)
                return NotFound("Provided IdWarehouse does not exist");
            else if (product.Amount <= 0)
                return NotFound("Provided amount is too low");
            else if (await _dbService.IsCorrectValueAsync(product) == -3)
                return NotFound("There is no order to fullfill");
            else if (await _dbService.IsCorrectValueAsync(product) == -4)
                return NotFound("The order has already been completed");
            else
                return Ok(await _dbService.PostProductAsync(product));
        }
    }
}
