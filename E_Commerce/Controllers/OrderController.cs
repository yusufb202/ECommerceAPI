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

        private int? GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(string.IsNullOrEmpty(userIdClaim))
            {
                return null;
            }
            return int.Parse(userIdClaim);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized("User is not logged in.");
            }

            var order = await _orderService.GetOrderById(id, userId.Value);
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized("User is not logged in.");
            }
            var orders = await _orderService.GetAllOrdersAsync(userId.Value);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO orderDTO)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized("User is not logged in.");
            }

            var order = await _orderService.CreateOrderAsync(orderDTO, userId.Value);
            return Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderDTO orderDTO)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized("User is not logged in.");
            }

            var order = await _orderService.UpdateOrderAsync(id, orderDTO, userId.Value);
            return Ok(order);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return Unauthorized("User is not logged in.");
            }
            await _orderService.DeleteOrderAsync(id, userId.Value);
            return NoContent();
        }
    }
}
