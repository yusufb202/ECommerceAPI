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
                .FirstOrDefaultAsync(w => w.Id == id) ?? throw new KeyNotFoundException("Warehouse not found");
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

        public async Task UpdateWarehouseStocksAsync(int warehouseId, List<WarehouseStock> stocks)
        {
            var warehouse = await _context.Warehouses
                .Include(w => w.WarehouseStocks)
                .FirstOrDefaultAsync(w => w.Id == warehouseId);

            if (warehouse == null)
            {
                throw new KeyNotFoundException("Warehouse not found");
            }

            warehouse.WarehouseStocks = stocks;
            await _context.SaveChangesAsync();
        }

        public async Task<WarehouseStock> GetWarehouseStockAsync(int warehouseId, int productId)
        {
            return await _context.WarehouseStocks
                .FirstOrDefaultAsync(ws => ws.WarehouseId == warehouseId && ws.ProductId == productId);
        }

        public async Task UpdateWarehouseStockAsync(WarehouseStock stock)
        {
            _context.WarehouseStocks.Update(stock);
            await _context.SaveChangesAsync();
        }

        public async Task AddWarehouseStockAsync(WarehouseStock stock)
        {
            _context.WarehouseStocks.Add(stock);
            await _context.SaveChangesAsync();
        }
    }
}
