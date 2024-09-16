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
    }
}
