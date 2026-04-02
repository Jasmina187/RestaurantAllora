using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly RestaurantAlloraProjectContext _context;

        public OrderService(RestaurantAlloraProjectContext context)
        {
            this._context = context;
        }

        public async Task<Guid> CreateOrderAsync(OrderViewModel model)
        {
            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = model.CustomerId,
                OrderDate = DateTime.Now,
                TotalAmount = model.TotalAmount,
                Status = "Обработва се"
            };

            foreach (var item in model.CustomerOrderItems)
            {
                order.CustomerOrderItems.Add(new CustomerOrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    DishId = item.DishId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task<IEnumerable<OrderViewModel>> GetCustomerOrdersAsync(Guid customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status
                }).ToListAsync();
        }
    }
}
