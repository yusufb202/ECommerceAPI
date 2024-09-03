using Core.DTOs;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> CreateOrderAsync(CreateOrderDTO orderDTO);
        Task<Order> UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
