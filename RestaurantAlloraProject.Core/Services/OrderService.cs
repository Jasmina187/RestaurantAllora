using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.CustomerOrderItem;
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
        private static readonly string[] OrderStatuses =
        {
            "Обработва се",
            "Приета",
            "Приготвя се",
            "Готова",
            "Доставена",
            "Отказана"
        };

        private readonly RestaurantAlloraProjectContext _context;
        public OrderService(RestaurantAlloraProjectContext context)
        {
            this._context = context;
        }

        public async Task<OrderViewModel> PrepareCheckoutAsync(OrderViewModel model)
        {
            if (model.CustomerOrderItems == null || !model.CustomerOrderItems.Any())
            {
                throw new ArgumentException("Количката е празна.");
            }

            var normalizedItems = await BuildValidatedOrderItemsAsync(model.CustomerOrderItems);

            model.CustomerOrderItems = normalizedItems;
            model.TotalAmount = normalizedItems.Sum(item => item.Price * item.Quantity);
            model.OrderDate = DateTime.Now;
            model.Status = string.IsNullOrWhiteSpace(model.Status) ? "Обработва се" : model.Status;

            return model;
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

            if (model.FulfillmentType != "Доставка" && model.FulfillmentType != "Вземане на място")
            {
                throw new ArgumentException("Изберете доставка или вземане на място.");
            }

            if (string.IsNullOrWhiteSpace(model.CustomerFullName))
            {
                throw new ArgumentException("Името е задължително.");
            }

            if (string.IsNullOrWhiteSpace(model.CustomerPhone))
            {
                throw new ArgumentException("Телефонът е задължителен.");
            }

            if (model.FulfillmentType == "Доставка" && string.IsNullOrWhiteSpace(model.DeliveryAddress))
            {
                throw new ArgumentException("Адресът е задължителен при доставка.");
            }

            var validatedItems = await BuildValidatedOrderItemsAsync(model.CustomerOrderItems);

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
                TotalAmount = validatedItems.Sum(item => item.Price * item.Quantity),
                Status = "Обработва се",
                FulfillmentType = model.FulfillmentType,
                CustomerFullName = model.CustomerFullName.Trim(),
                CustomerPhone = model.CustomerPhone.Trim(),
                DeliveryAddress = model.FulfillmentType == "Доставка" ? model.DeliveryAddress?.Trim() : null,
                Notes = string.IsNullOrWhiteSpace(model.Notes) ? null : model.Notes.Trim()
            };

            foreach (var item in validatedItems)
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
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    FulfillmentType = o.FulfillmentType,
                    CustomerFullName = o.CustomerFullName,
                    CustomerPhone = o.CustomerPhone,
                    DeliveryAddress = o.DeliveryAddress,
                    Notes = o.Notes,
                    CustomerId = o.CustomerId
                }).ToListAsync();
        }

        public async Task<OrderListViewModel> GetCustomerOrdersPageAsync(Guid customerId, int page, int pageSize)
        {
            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);

            var query = _context.Orders
                .Where(o => o.CustomerId == customerId);

            var totalOrders = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalOrders / (double)pageSize);

            if (totalPages > 0 && page > totalPages)
            {
                page = totalPages;
            }

            var orders = await query
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    FulfillmentType = o.FulfillmentType,
                    CustomerFullName = o.CustomerFullName,
                    CustomerPhone = o.CustomerPhone,
                    DeliveryAddress = o.DeliveryAddress,
                    Notes = o.Notes,
                    CustomerId = o.CustomerId
                })
                .ToListAsync();

            return new OrderListViewModel
            {
                Orders = orders,
                CurrentPage = page,
                PageSize = pageSize,
                TotalOrders = totalOrders
            };
        }

        public async Task<OrderViewModel?> GetCustomerOrderDetailsAsync(Guid orderId, Guid customerId)
        {
            return await _context.Orders
                .Where(o => o.OrderId == orderId && o.CustomerId == customerId)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    FulfillmentType = o.FulfillmentType,
                    CustomerFullName = o.CustomerFullName,
                    CustomerPhone = o.CustomerPhone,
                    DeliveryAddress = o.DeliveryAddress,
                    Notes = o.Notes,
                    CustomerId = o.CustomerId,
                    CustomerOrderItems = o.CustomerOrderItems
                        .Select(item => new CustomerOrderItemViewModel
                        {
                            Id = item.Id,
                            OrderId = item.OrderId,
                            DishId = item.DishId,
                            DishName = item.Dish.NameOfTheDish,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            EmployeeId = item.EmployeeId
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    FulfillmentType = o.FulfillmentType,
                    CustomerFullName = o.CustomerFullName,
                    CustomerPhone = o.CustomerPhone,
                    DeliveryAddress = o.DeliveryAddress,
                    Notes = o.Notes,
                    CustomerId = o.CustomerId,
                    CustomerUserName = o.Customer.User.UserName,
                    CustomerEmail = o.Customer.User.Email
                })
                .ToListAsync();
        }

        public async Task<OrderListViewModel> GetAllOrdersPageAsync(int page, int pageSize)
        {
            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);

            var totalOrders = await _context.Orders.CountAsync();
            var totalPages = (int)Math.Ceiling(totalOrders / (double)pageSize);

            if (totalPages > 0 && page > totalPages)
            {
                page = totalPages;
            }

            var orders = await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    FulfillmentType = o.FulfillmentType,
                    CustomerFullName = o.CustomerFullName,
                    CustomerPhone = o.CustomerPhone,
                    DeliveryAddress = o.DeliveryAddress,
                    Notes = o.Notes,
                    CustomerId = o.CustomerId,
                    CustomerUserName = o.Customer.User.UserName,
                    CustomerEmail = o.Customer.User.Email
                })
                .ToListAsync();

            return new OrderListViewModel
            {
                Orders = orders,
                CurrentPage = page,
                PageSize = pageSize,
                TotalOrders = totalOrders
            };
        }

        public async Task<OrderViewModel?> GetOrderDetailsAsync(Guid orderId)
        {
            return await _context.Orders
                .Where(o => o.OrderId == orderId)
                .Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    FulfillmentType = o.FulfillmentType,
                    CustomerFullName = o.CustomerFullName,
                    CustomerPhone = o.CustomerPhone,
                    DeliveryAddress = o.DeliveryAddress,
                    Notes = o.Notes,
                    CustomerId = o.CustomerId,
                    CustomerUserName = o.Customer.User.UserName,
                    CustomerEmail = o.Customer.User.Email,
                    CustomerOrderItems = o.CustomerOrderItems
                        .Select(item => new CustomerOrderItemViewModel
                        {
                            Id = item.Id,
                            OrderId = item.OrderId,
                            DishId = item.DishId,
                            DishName = item.Dish.NameOfTheDish,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            EmployeeId = item.EmployeeId
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task UpdateStatusAsync(Guid orderId, string status)
        {
            if (!OrderStatuses.Contains(status))
            {
                throw new ArgumentException("Невалиден статус на поръчка.");
            }

            var order = await _context.Orders.FindAsync(orderId);

            if (order == null)
            {
                throw new ArgumentException("Поръчката не е намерена.");
            }

            order.Status = status;
            await _context.SaveChangesAsync();
        }

        public IEnumerable<string> GetOrderStatuses()
        {
            return OrderStatuses;
        }

        private async Task<List<CustomerOrderItemViewModel>> BuildValidatedOrderItemsAsync(IEnumerable<CustomerOrderItemViewModel> items)
        {
            var requestedItems = items
                .Where(item => item.DishId != Guid.Empty)
                .GroupBy(item => item.DishId)
                .Select(group => new
                {
                    DishId = group.Key,
                    Quantity = group.Sum(item => item.Quantity)
                })
                .ToList();

            if (!requestedItems.Any())
            {
                throw new ArgumentException("Поръчката трябва да съдържа поне едно ястие.");
            }

            if (requestedItems.Any(item => item.Quantity < 1))
            {
                throw new ArgumentException("Количеството трябва да бъде поне 1.");
            }

            var dishIds = requestedItems.Select(item => item.DishId).ToList();
            var dishes = await _context.Dishes
                .Where(d => dishIds.Contains(d.DishId))
                .ToDictionaryAsync(d => d.DishId);

            if (dishes.Count != dishIds.Count)
            {
                throw new ArgumentException("Поръчката съдържа невалидно ястие.");
            }

            return requestedItems
                .Select(item => new CustomerOrderItemViewModel
                {
                    DishId = item.DishId,
                    DishName = dishes[item.DishId].NameOfTheDish,
                    Quantity = item.Quantity,
                    Price = dishes[item.DishId].PriceOfTheDish
                })
                .ToList();
        }
    }

}
