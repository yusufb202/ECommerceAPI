using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IWishListRepository
    {
        Task<WishList> GetWishListByUserIdAsync(int userId);
        Task AddItemToWishListAsync(WishListItem item);
        Task RemoveItemFromWishListAsync(int wishListItemId);
        Task CreateWishListAsync(WishList wishList);
        Task ClearItemsAsync(int wishListId);
    }
}
