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
    public class ProductRepository: GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) 
        {
        }
    }
}
