using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Services;
using System;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly Application.Interfaces.IJustInTimeInventoryService _jitInventoryService;

        public InventoryController(Application.Interfaces.IJustInTimeInventoryService jitInventoryService)
        {
            _jitInventoryService = jitInventoryService ?? throw new ArgumentNullException(nameof(jitInventoryService));
        }

        [HttpPost("optimize")]
        public async Task<IActionResult> OptimizeInventory()
        {
            try
            {
                await _jitInventoryService.OptimizeInventoryAsync();
                return Ok("Inventory optimized successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while optimizing inventory: {ex.Message}");
            }
        }
    }
}
