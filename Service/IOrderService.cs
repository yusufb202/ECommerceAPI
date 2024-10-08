﻿using Core.DTOs;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public interface IOrderService
    {
        Task<Order> GetOrderById(int id, int userId);
        Task<IEnumerable<Order>> GetAllOrdersAsync(int userId);
        Task<Order> CreateOrderAsync(CreateOrderDTO orderDTO, int userId);
        Task<Order> UpdateOrderAsync(int orderId, UpdateOrderDTO orderDTO, int userId);
        Task DeleteOrderAsync(int id, int userId);
    }
}
