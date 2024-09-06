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
    public class WishListRepository : IWishListRepository
    {
        private readonly AppDbContext _context;

        public WishListRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<WishList> GetWishListByUserIdAsync(int userId)
        {
            return await _context.WishLists.Include(w => w.Items).FirstOrDefaultAsync(w => w.UserId == userId);
        }

        public async Task AddItemToWishListAsync(WishListItem item)
        {
            _context.WishListItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task CreateWishListAsync(WishList wishList)
        {
            _context.WishLists.AddAsync(wishList);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromWishListAsync(int wishListItemId)
        {
            var item = await _context.WishListItems.FindAsync(wishListItemId);
            _context.WishListItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        public async Task ClearItemsAsync(int wishListId)
        {
            var items = await _context.WishListItems.Where(i => i.WishListId == wishListId).ToListAsync();
            _context.WishListItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
    }
