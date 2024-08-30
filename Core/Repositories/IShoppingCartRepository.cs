using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCart> GetCartByUserIdAsync(int userId);
        Task AddItemToCartAsync(ShoppingCartItem item);
        Task RemoveItemFromCartAsync(int cartItemId);
        Task ClearCartAsync(int userId);
    }
}
