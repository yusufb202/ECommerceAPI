using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var userId = GetUserId();
            var order = await _orderService.GetOrderById(id, userId);
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var userId = GetUserId();
            var orders = await _orderService.GetAllOrdersAsync(userId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO orderDTO)
        {
            var userId = GetUserId();
            var order = await _orderService.CreateOrderAsync(orderDTO, userId);
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDTO orderDTO)
        {
            var userId = GetUserId();
            var order = await _orderService.UpdateOrderAsync(id, orderDTO, userId);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var userId = GetUserId();
            await _orderService.DeleteOrderAsync(id, userId);
            return NoContent();
        }
    }
}
