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
            if (model.CustomerId == Guid.Empty)
            {
                throw new ArgumentException("Невалиден клиент.");
            }

            if (model.CustomerOrderItems == null || !model.CustomerOrderItems.Any())
            {
                throw new ArgumentException("Поръчката трябва да съдържа поне едно ястие.");
            }

            var dishIds = model.CustomerOrderItems.Select(i => i.DishId).Distinct().ToList();
            var dishes = await _context.Dishes
                .Where(d => dishIds.Contains(d.DishId))
                .ToDictionaryAsync(d => d.DishId);

            if (dishes.Count != dishIds.Count)
            {
                throw new ArgumentException("Поръчката съдържа невалидно ястие.");
            }

            var customerProfileExists = await _context.Set<CustomerProfile>()
                .AnyAsync(cp => cp.UserId == model.CustomerId);

            if (!customerProfileExists)
            {
                var newCustomerProfile = new CustomerProfile
                {
                    UserId = model.CustomerId
                };

                _context.Set<CustomerProfile>().Add(newCustomerProfile);
                await _context.SaveChangesAsync();
            }

            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = model.CustomerId,
                OrderDate = DateTime.Now,
                TotalAmount = model.CustomerOrderItems.Sum(item => dishes[item.DishId].PriceOfTheDish * item.Quantity),
                Status = "Обработва се"
            };

            foreach (var item in model.CustomerOrderItems)
            {
                if (item.Quantity < 1)
                {
                    throw new ArgumentException("Количеството трябва да бъде поне 1.");
                }

                order.CustomerOrderItems.Add(new CustomerOrderItem
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.OrderId,
                    DishId = item.DishId,
                    Quantity = item.Quantity,
                    Price = dishes[item.DishId].PriceOfTheDish
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
