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
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;

        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
        {
            return await _context.Warehouses
                .Include(w => w.WarehouseStocks)
                    .ThenInclude(ws => ws.Product)
                        .ThenInclude(p => p.Category)
                .ToListAsync();
        }

        public async Task<Warehouse> GetWarehouseByIdAsync(int id)
        {
            return await _context.Warehouses
                .Include(w => w.WarehouseStocks)
                    .ThenInclude(ws => ws.Product)
                        .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task AddWarehouseAsync(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWarehouseAsync(Warehouse warehouse)
        {
            _context.Warehouses.Update(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWarehouseAsync(int id)
        {
            var warehouse = await _context.Warehouses
                .Include(w => w.WarehouseStocks)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (warehouse == null)
            {
                throw new KeyNotFoundException("Warehouse not found");
            }

            foreach (var stock in warehouse.WarehouseStocks)
            {
                stock.WarehouseId = 1;
            }

            _context.Warehouses.Remove(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> WarehouseExistsByNameAsync(string name)
        {
            return await _context.Warehouses.AnyAsync(w => w.Name == name);
        }
    }
}
