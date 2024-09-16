using Core.Models;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }

        public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
        {
            return await _warehouseRepository.GetAllWarehousesAsync();
        }

        public async Task<Warehouse> GetWarehouseByIdAsync(int id)
        {
            return await _warehouseRepository.GetWarehouseByIdAsync(id);
        }

        public async Task AddWarehouseAsync(Warehouse warehouse)
        {
            if (await _warehouseRepository.WarehouseExistsByNameAsync(warehouse.Name))
            {
                throw new InvalidOperationException("A warehouse with this name already exists.");
            }

            await _warehouseRepository.AddWarehouseAsync(warehouse);
        }

        public async Task UpdateWarehouseAsync(Warehouse warehouse)
        {
            await _warehouseRepository.UpdateWarehouseAsync(warehouse);
        }

        public async Task DeleteWarehouseAsync(int id)
        {
            await _warehouseRepository.DeleteWarehouseAsync(id);
        }

        public async Task UpdateWarehouseStocksAsync(int warehouseId, List<WarehouseStock> stocks)
        {
            await _warehouseRepository.UpdateWarehouseStocksAsync(warehouseId, stocks);
        }

        public async Task TransferStocksAsync(int sourceWarehouseId, int destinationWarehouseId, List<WarehouseStock> stocks)
        {
            // Deduct stocks from the source warehouse
            foreach (var stock in stocks)
            {
                var sourceStock = await _warehouseRepository.GetWarehouseStockAsync(sourceWarehouseId, stock.ProductId);
                if (sourceStock == null || sourceStock.Quantity < stock.Quantity)
                {
                    throw new InvalidOperationException("Insufficient stock in the source warehouse.");
                }
                sourceStock.Quantity -= stock.Quantity;
                await _warehouseRepository.UpdateWarehouseStockAsync(sourceStock);
            }

            // Add stocks to the destination warehouse
            foreach (var stock in stocks)
            {
                var destinationStock = await _warehouseRepository.GetWarehouseStockAsync(destinationWarehouseId, stock.ProductId);
                if (destinationStock == null)
                {
                    destinationStock = new WarehouseStock
                    {
                        WarehouseId = destinationWarehouseId,
                        ProductId = stock.ProductId,
                        Quantity = stock.Quantity
                    };
                    await _warehouseRepository.AddWarehouseStockAsync(destinationStock);
                }
                else
                {
                    destinationStock.Quantity += stock.Quantity;
                    await _warehouseRepository.UpdateWarehouseStockAsync(destinationStock);
                }
            }
        }
    }
}
