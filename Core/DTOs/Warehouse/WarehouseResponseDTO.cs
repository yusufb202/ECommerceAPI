using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Warehouse
{
    internal class WarehouseResponseDTO
    {
    }
}
namespace DTOs
{
    public class WarehouseResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<WarehouseStockResponseDTO> WarehouseStocks { get; set; }
    }

    public class WarehouseStockResponseDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ProductDTO Product { get; set; }
    }
}
