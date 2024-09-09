﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<string> GetCategoryByProductIdAsync(int productId);

        Task<Category> GetByNameAsync(string name);
    }
}
