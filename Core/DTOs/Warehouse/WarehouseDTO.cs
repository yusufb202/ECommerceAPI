using Core.DTOs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Warehouse
{
    public class WarehouseDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public List<WarehouseStockDTO> WarehouseStocks { get; set; }
    }

    public class WarehouseStockDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
