using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(int id);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
