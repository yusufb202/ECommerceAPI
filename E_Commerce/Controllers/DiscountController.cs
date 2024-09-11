using Core.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IProductService _productService;

        public DiscountController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("apply-discount")]
        public async Task<IActionResult> ApplyDiscount([FromBody] ApplyDiscountDTO applyDiscountDTO)
        {
            await _productService.ApplyDiscountToProductsAsync(applyDiscountDTO.ProductIds, applyDiscountDTO.DiscountPercentage);
            return Ok();
        }

    }
}
