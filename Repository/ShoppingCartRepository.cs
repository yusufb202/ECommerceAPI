using Core.Models;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;

        public ShoppingCartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ShoppingCart> GetCartByUserIdAsync(int userId)
        {
            return await _context.ShoppingCarts.Include(c => c.Items).FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddItemToCartAsync(ShoppingCartItem item)
        {
            _context.ShoppingCartItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task CreateCartAsync(ShoppingCart cart)
        {
            _context.ShoppingCarts.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromCartAsync(int cartItemId)
        {
            var item = await _context.ShoppingCartItems.FindAsync(cartItemId);
            _context.ShoppingCartItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                // Handle the case where the cart is not found
                throw new InvalidOperationException("Shopping cart not found for the given user ID.");
            }
            _context.ShoppingCartItems.RemoveRange(cart.Items);
            await _context.SaveChangesAsync();
        }
    }
}
