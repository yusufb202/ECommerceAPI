using Core.DTOs;
using Core.DTOs.Warehouse;
using Core.Models;
using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        // GET: api/Warehouse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseResponseDTO>>> GetWarehouses()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAsync();
            var warehouseDtos = warehouses.Select(w => new WarehouseResponseDTO
            {
                Id = w.Id,
                Name = w.Name,
                Location = w.Location,
                WarehouseStocks = w.WarehouseStocks.Select(ws => new WarehouseStockResponseDTO
                {
                    Id = ws.Id,
                    ProductId = ws.ProductId,
                    Quantity = ws.Quantity,
                    Product = new ProductDTO
                    {
                        Id = ws.Product.Id,
                        Name = ws.Product.Name,
                        Description = ws.Product.Description,
                        Price = ws.Product.Price,
                        TotalStock = ws.Product.Stock,
                        Category = ws.Product.Category.Name // Map only the Category Name
                    }
                }).ToList()
            }).ToList();

            return Ok(warehouseDtos);
        }

        // GET: api/Warehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseResponseDTO>> GetWarehouse(int id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            var warehouseDto = new WarehouseResponseDTO
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Location = warehouse.Location,
                WarehouseStocks = warehouse.WarehouseStocks.Select(ws => new WarehouseStockResponseDTO
                {
                    Id = ws.Id,
                    ProductId = ws.ProductId,
                    Quantity = ws.Quantity,
                    Product = new ProductDTO
                    {
                        Id = ws.Product.Id,
                        Name = ws.Product.Name,
                        Description = ws.Product.Description,
                        Price = ws.Product.Price,
                        TotalStock = ws.Product.Stock,
                        Category = ws.Product.Category.Name // Map only the Category Name
                    }
                }).ToList()
            };

            return Ok(warehouseDto);
        }

        // POST: api/Warehouse
        [HttpPost]
        public async Task<ActionResult<Warehouse>> PostWarehouse(WarehouseDTO warehouseDto)
        {
            var warehouse = new Warehouse
            {
                Name = warehouseDto.Name,
                Location = warehouseDto.Location,
                WarehouseStocks = warehouseDto.WarehouseStocks.Select(ws => new WarehouseStock
                {
                    ProductId = ws.ProductId,
                    Quantity = ws.Quantity
                }).ToList()
            };

            await _warehouseService.AddWarehouseAsync(warehouse);

            return CreatedAtAction(nameof(GetWarehouse), new { id = warehouse.Id }, warehouse);
        }

        // PUT: api/Warehouse/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouse(int id, WarehouseDTO warehouseDto)
        {
            var warehouse = new Warehouse
            {
                Id = id,
                Name = warehouseDto.Name,
                Location = warehouseDto.Location,
                WarehouseStocks = warehouseDto.WarehouseStocks.Select(ws => new WarehouseStock
                {
                    ProductId = ws.ProductId,
                    Quantity = ws.Quantity
                }).ToList()
            };

            await _warehouseService.UpdateWarehouseAsync(warehouse);
            return NoContent();
        }

        // DELETE: api/Warehouse/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(int id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }

            await _warehouseService.DeleteWarehouseAsync(id);
            return NoContent();
        }
    }
}
