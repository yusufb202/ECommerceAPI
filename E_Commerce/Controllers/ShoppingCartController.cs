using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _cartRepository;

        public ShoppingCartController(IShoppingCartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        private int? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return null;
            }
            return int.Parse(userIdClaim);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserId(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToCart([FromBody] ShoppingCartItemDTO itemDTO)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var shoppingCart = await _cartRepository.GetCartByUserIdAsync(userId.Value);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart { UserId = userId.Value };
                await _cartRepository.CreateCartAsync(shoppingCart);
            }

            var item = new ShoppingCartItem
            {
                ShoppingCartId = shoppingCart.Id,
                ProductId = itemDTO.ProductId,
                Quantity = itemDTO.Quantity
            };

            await _cartRepository.AddItemToCartAsync(item);
            return Ok();
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveItemFromCart(int cartItemId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            await _cartRepository.RemoveItemFromCartAsync(cartItemId);
            return NoContent();
        }

        [HttpDelete("clear/{clearUserId}")]
        public async Task<IActionResult> ClearCart(int clearUserId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            await _cartRepository.ClearCartAsync(clearUserId);
            return NoContent();
        }
    }
}
