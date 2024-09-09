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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
        }
        public async Task<string> GetCategoryByProductIdAsync(int productId)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .Where(c => c.Products.Any(p => p.Id == productId))
                .Select(c => c.Name)
                .FirstOrDefaultAsync();
        }
    }
}
