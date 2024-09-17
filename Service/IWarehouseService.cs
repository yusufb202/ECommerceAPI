using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IWarehouseService
    {
        Task<IEnumerable<Warehouse>> GetAllWarehousesAsync();
        Task<Warehouse> GetWarehouseByIdAsync(int id);
        Task AddWarehouseAsync(Warehouse warehouse);
        Task UpdateWarehouseAsync(Warehouse warehouse);
        Task DeleteWarehouseAsync(int id);
        Task UpdateWarehouseStocksAsync(int warehouseId, List<WarehouseStock> stocks);
        Task TransferStocksAsync(int sourceWarehouseId, int destinationWarehouseId, List<WarehouseStock> stocks);
        Task AddWarehouseStockAsync(WarehouseStock stock);
    }
}
