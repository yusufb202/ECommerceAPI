﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId);
        Task SaveChangesAsync();
        Task<IEnumerable<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds);

    }
}
