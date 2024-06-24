using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    public class FinalcialController
    {
        [ApiController]
        [Route("api/[controller]")]
        public class FinancialController : ControllerBase
        {
            private readonly IFinancialManagementService _financialService;

            public FinancialController(IFinancialManagementService financialService)
            {
                _financialService = financialService;
            }

            [HttpGet("report")]
            public async Task<IActionResult> GenerateReport()
            {
                var result = await _financialService.GenerateReportAsync();
                return Ok(result);
            }
        }
    }
}
