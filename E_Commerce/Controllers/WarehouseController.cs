using Core.DTOs;
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
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetWarehouses()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAsync();
            return Ok(warehouses);
        }

        // GET: api/Warehouse/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse>> GetWarehouse(int id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return Ok(warehouse);
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
