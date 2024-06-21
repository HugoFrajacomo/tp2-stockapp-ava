using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Infra.Data.Repositories;

namespace StockApp.API.Controllers
{
    public class PromotionController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IPromotionService _promotionService;

        public PromotionController(IDiscountService discountService, IPromotionService promotionService)
        {
            _discountService = discountService;
            _promotionService = promotionService;
        }

        [HttpPost("apply")]
        public IActionResult ApplyDiscount([FromBody] ApplyDiscountDTO dto)
        {
            var discountedPrice = _discountService.ApplyDiscount(dto.Price, dto.DiscountPercentage);
            return Ok(new { DiscountedPrice = discountedPrice });
        }

        [HttpPost("create")]
        public IActionResult CreatePromotion([FromBody] CreatePromotionDTO dto)
        {
            _promotionService.CreatePromotion(dto.Name, dto.DiscountPercentage, dto.StartDate, dto.EndDate);
            return Ok();
        }
    }
}
