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

        public async Task<Order> GetOrderById(int id, int userId)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null || order.UserId != userId)
            {
                throw new Exception($"Order with id {id} not found or access denied");
            }
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync(int userId)
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Where(o => o.UserId == userId);
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDTO orderDTO, int userId)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                Items = new List<OrderItem>()
            };

            foreach (var itemDTO in orderDTO.Items)
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

                var orderItem = new OrderItem
                {
                    ProductId = itemDTO.ProductId,
                    Quantity = itemDTO.Quantity,
                    Price = product.Price
                };

                order.Items.Add(orderItem);
            }
            return await _orderRepository.AddAsync(order);
        }

        public async Task<Order> UpdateOrderAsync(int orderId, UpdateOrderDTO orderDTO, int userId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null || order.UserId != userId)
            {
                throw new Exception($"Order with id {orderId} not found or access denied");
            }

            // Add back the stock of the previous order items
            foreach (var item in order.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    product.Stock += item.Quantity;
                    await _productRepository.UpdateAsync(product);
                }
            }

            order.UserId = userId;
            order.OrderDate = DateTime.UtcNow;
            order.Items.Clear();

            foreach (var itemDTO in orderDTO.Items)
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

                var orderItem = new OrderItem
                {
                    ProductId = itemDTO.ProductId,
                    Quantity = itemDTO.Quantity,
                    Price = product.Price
                };

                order.Items.Add(orderItem);
            }

            return await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task DeleteOrderAsync(int id, int userId)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null || order.UserId != userId)
            {
                throw new Exception($"Order with id {id} not found or access denied");
            }

            // Add back the stock of the order items
            foreach (var item in order.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId);
                if (product != null)
                {
                    product.Stock += item.Quantity;
                    await _productRepository.UpdateAsync(product);
                }
            }

            await _orderRepository.DeleteOrderAsync(id);
        }
    }
}
