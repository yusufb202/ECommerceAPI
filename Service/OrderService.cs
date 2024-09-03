using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDTO orderDTO)
        {
            var order = new Order
            {
                UserId = orderDTO.UserId,
                OrderDate = DateTime.UtcNow,
                Items = new List<OrderItem>()
            };

            foreach(var itemDTO in orderDTO.Items)
            {
                var product = await _productRepository.GetByIdAsync(itemDTO.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with id {itemDTO.ProductId} not found");
                }

                if (product.Stock < itemDTO.Quantity)
                {
                    throw new Exception($"Not enough stock for product with id {itemDTO.ProductId}");
                }

                product.Stock -= itemDTO.Quantity;

                await _productRepository.UpdateAsync(product);

                var orderItem= new OrderItem
                {
                    ProductId = itemDTO.ProductId,
                    Quantity = itemDTO.Quantity,
                    Price = product.Price
                };

                order.Items.Add(orderItem);
            }
            return await _orderRepository.AddAsync(order);
        }

        public async Task<Order> UpdateOrderAsync(Order order)
        {
            return await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteOrderAsync(id);
        }
    }
}
