using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListRepository _wishListRepository;

        public WishListController(IWishListRepository wishListRepository)
        {
            _wishListRepository = wishListRepository;
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

        [HttpGet]
        public async Task<IActionResult> GetWishList()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var wishList = await _wishListRepository.GetWishListByUserIdAsync(userId.Value);
            if (wishList == null)
            {
                return NotFound();
            }
            return Ok(wishList);
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("{ByUserId}")]
        public async Task<IActionResult> GetWishListByUserId(int ByuserId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var wishList = await _wishListRepository.GetWishListByUserIdAsync(ByuserId);
            if (wishList == null)
            {
                return NotFound();
            }
            return Ok(wishList);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToWishList([FromBody] WishListItemDTO itemDTO)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var wishList = await _wishListRepository.GetWishListByUserIdAsync(userId.Value);

            if (wishList == null)
            {
                wishList = new WishList { UserId = userId.Value };
                await _wishListRepository.CreateWishListAsync(wishList);
            }

            var item = new WishListItem
            {
                WishListId = wishList.Id,
                ProductId = itemDTO.ProductId,
                Quantity = itemDTO.Quantity
            };

            await _wishListRepository.AddItemToWishListAsync(item);
            return Ok(item);
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteItemFromWishList(int productId)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var wishList = await _wishListRepository.GetWishListByUserIdAsync(userId.Value);
            if (wishList == null)
            {
                return NotFound();
            }

            var item = wishList.Items?.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                return NotFound();
            }

            await _wishListRepository.RemoveItemFromWishListAsync(item.Id);
            return NoContent();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearWishList()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var wishList = await _wishListRepository.GetWishListByUserIdAsync(userId.Value);
            if (wishList == null)
            {
                return NotFound();
            }
            await _wishListRepository.ClearItemsAsync(wishList.Id);
            return NoContent();
        }
    }
}
