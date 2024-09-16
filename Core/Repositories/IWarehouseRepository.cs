using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IWarehouseRepository
    {
        Task<IEnumerable<Warehouse>> GetAllWarehousesAsync();
        Task<Warehouse> GetWarehouseByIdAsync(int id);
        Task AddWarehouseAsync(Warehouse warehouse);
        Task UpdateWarehouseAsync(Warehouse warehouse);
        Task DeleteWarehouseAsync(int id);
        Task<bool> WarehouseExistsByNameAsync(string name);
        Task UpdateWarehouseStocksAsync(int warehouseId, List<WarehouseStock> stocks);

        Task<WarehouseStock> GetWarehouseStockAsync(int warehouseId, int productId);
        Task UpdateWarehouseStockAsync(WarehouseStock stock);
        Task AddWarehouseStockAsync(WarehouseStock stock);

    }
}
