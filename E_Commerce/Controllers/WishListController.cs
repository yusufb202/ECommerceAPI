using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWishListByUserId(int userId)
        {
            var wishList = await _wishListRepository.GetWishListByUserIdAsync(userId);
            if (wishList == null)
            {
                return NotFound();
            }
            return Ok(wishList);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToWishList([FromBody] WishListItemDTO itemDTO)
        {
            var item = new WishListItem
            {
                ProductId = itemDTO.ProductId,
                Quantity = itemDTO.Quantity
            };
            await _wishListRepository.AddItemToWishListAsync(item);
            return Ok();
        }

        [HttpDelete("{wishListItemId}")]
        public async Task<IActionResult> RemoveItemFromWishList(int wishListItemId)
        {
            await _wishListRepository.RemoveItemFromWishListAsync(wishListItemId);
            return NoContent();
        }
    }
}
