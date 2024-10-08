﻿using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
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

        [HttpGet("Items")]
        public async Task<IActionResult> GetAllItems()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var cart = await _cartRepository.GetCartByUserIdAsync(userId.Value);
            if (cart == null || cart.Items == null)
            {
                return NotFound("No items found in the shopping cart.");
            }

            return Ok(cart.Items);
        }

        [Authorize(Policy = "Admin")]
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

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteItemFromWishList(int productId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var wishList = await _cartRepository.GetCartByUserIdAsync(userId.Value);
            if (wishList == null)
            {
                return NotFound();
            }

            var item = wishList.Items?.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                return NotFound();
            }

            await _cartRepository.RemoveItemFromCartAsync(item.Id);
            return NoContent();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            await _cartRepository.ClearCartAsync(userId.Value);
            return NoContent();
        }
    }
}
