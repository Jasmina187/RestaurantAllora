using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.CustomerOrderItem;
using RestaurantAlloraProjectViewModels.Order;

namespace RestaurantAlloraProjectTests;

public class OrderServiceTests
{
    [Fact]
    public async Task PrepareCheckoutAsync_CombinesDuplicateItemsAndUsesCurrentDishPrice()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory();
        var dish = TestDataFactory.CreateDish(category, "Фатуш", 7.49m);
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        var service = new OrderService(context);

        var result = await service.PrepareCheckoutAsync(new OrderViewModel
        {
            CustomerOrderItems = new List<CustomerOrderItemViewModel>
            {
                new() { DishId = dish.DishId, Quantity = 1, Price = 1m },
                new() { DishId = dish.DishId, Quantity = 2, Price = 1m }
            }
        });

        var item = Assert.Single(result.CustomerOrderItems);
        Assert.Equal(3, item.Quantity);
        Assert.Equal(7.49m, item.Price);
        Assert.Equal(22.47m, result.TotalAmount);
    }

    [Fact]
    public async Task PrepareCheckoutAsync_ThrowsForEmptyInvalidQuantityOrUnknownDish()
    {
        await using var context = TestDataFactory.CreateContext();
        var service = new OrderService(context);

        await Assert.ThrowsAsync<ArgumentException>(() => service.PrepareCheckoutAsync(new OrderViewModel()));

        await Assert.ThrowsAsync<ArgumentException>(() => service.PrepareCheckoutAsync(new OrderViewModel
        {
            CustomerOrderItems = new List<CustomerOrderItemViewModel>
            {
                new() { DishId = Guid.NewGuid(), Quantity = 0 }
            }
        }));

        await Assert.ThrowsAsync<ArgumentException>(() => service.PrepareCheckoutAsync(new OrderViewModel
        {
            CustomerOrderItems = new List<CustomerOrderItemViewModel>
            {
                new() { DishId = Guid.Empty, Quantity = 1 }
            }
        }));

        await Assert.ThrowsAsync<ArgumentException>(() => service.PrepareCheckoutAsync(new OrderViewModel
        {
            CustomerOrderItems = new List<CustomerOrderItemViewModel>
            {
                new() { DishId = Guid.NewGuid(), Quantity = 1 }
            }
        }));
    }

    [Fact]
    public async Task CreateOrderAsync_SavesOrderAndCustomerProfile()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory();
        var dish = TestDataFactory.CreateDish(category, "Фатуш", 7.49m);
        var customerId = Guid.NewGuid();
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        var service = new OrderService(context);

        var orderId = await service.CreateOrderAsync(new OrderViewModel
        {
            CustomerId = customerId,
            CustomerFullName = "Стоян Стоянов",
            CustomerPhone = "0888123456",
            FulfillmentType = "Вземане на място",
            CustomerOrderItems = new List<CustomerOrderItemViewModel>
            {
                new() { DishId = dish.DishId, Quantity = 2 }
            }
        });

        var order = await context.Orders.Include(o => o.CustomerOrderItems).SingleAsync(o => o.OrderId == orderId);
        Assert.Equal(14.98m, order.TotalAmount);
        Assert.Equal("Обработва се", order.Status);
        Assert.True(await context.Set<CustomerProfile>().AnyAsync(cp => cp.UserId == customerId));
        Assert.Single(order.CustomerOrderItems);
    }

    [Fact]
    public async Task CreateOrderAsync_ValidatesRequiredFields()
    {
        await using var context = TestDataFactory.CreateContext();
        var service = new OrderService(context);
        var validItems = new List<CustomerOrderItemViewModel> { new() { DishId = Guid.NewGuid(), Quantity = 1 } };

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateOrderAsync(new OrderViewModel
        {
            CustomerId = Guid.Empty,
            CustomerOrderItems = validItems
        }));

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateOrderAsync(new OrderViewModel
        {
            CustomerId = Guid.NewGuid()
        }));

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateOrderAsync(new OrderViewModel
        {
            CustomerId = Guid.NewGuid(),
            CustomerOrderItems = validItems,
            FulfillmentType = "Друго",
            CustomerFullName = "Име",
            CustomerPhone = "123"
        }));

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateOrderAsync(new OrderViewModel
        {
            CustomerId = Guid.NewGuid(),
            CustomerOrderItems = validItems,
            FulfillmentType = "Вземане на място",
            CustomerPhone = "123"
        }));

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateOrderAsync(new OrderViewModel
        {
            CustomerId = Guid.NewGuid(),
            CustomerOrderItems = validItems,
            FulfillmentType = "Вземане на място",
            CustomerFullName = "Име"
        }));

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateOrderAsync(new OrderViewModel
        {
            CustomerId = Guid.NewGuid(),
            CustomerOrderItems = validItems,
            FulfillmentType = "Доставка",
            CustomerFullName = "Име",
            CustomerPhone = "123"
        }));
    }

    [Fact]
    public async Task GetCustomerOrdersAndPages_ReturnCustomerOrdersOnly()
    {
        await using var context = TestDataFactory.CreateContext();
        var customerId = Guid.NewGuid();
        var otherCustomerId = Guid.NewGuid();
        context.Orders.AddRange(
            CreateOrder(customerId, total: 10m, date: DateTime.UtcNow.AddMinutes(-1)),
            CreateOrder(customerId, total: 20m, date: DateTime.UtcNow),
            CreateOrder(otherCustomerId, total: 30m, date: DateTime.UtcNow.AddMinutes(1)));
        await context.SaveChangesAsync();
        var service = new OrderService(context);

        var all = (await service.GetCustomerOrdersAsync(customerId)).ToList();
        var page = await service.GetCustomerOrdersPageAsync(customerId, page: 5, pageSize: 1);

        Assert.Equal(2, all.Count);
        Assert.Equal(2, page.TotalOrders);
        Assert.Equal(2, page.CurrentPage);
        Assert.Single(page.Orders);
    }

    [Fact]
    public async Task GetCustomerOrderDetailsAsync_ReturnsDetailsOnlyForOwner()
    {
        await using var context = TestDataFactory.CreateContext();
        var customerId = Guid.NewGuid();
        var category = TestDataFactory.CreateCategory();
        var dish = TestDataFactory.CreateDish(category, "Филе риба", 9.71m);
        var order = CreateOrder(customerId, total: 9.71m, date: DateTime.UtcNow);
        order.CustomerOrderItems.Add(new CustomerOrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = order.OrderId,
            Order = order,
            DishId = dish.DishId,
            Dish = dish,
            Quantity = 1,
            Price = 9.71m
        });
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        var service = new OrderService(context);

        var details = await service.GetCustomerOrderDetailsAsync(order.OrderId, customerId);
        var forbidden = await service.GetCustomerOrderDetailsAsync(order.OrderId, Guid.NewGuid());

        Assert.Equal("Филе риба", Assert.Single(details!.CustomerOrderItems).DishName);
        Assert.Null(forbidden);
    }

    [Fact]
    public async Task GetAllOrdersMethods_ReturnAdminOrderData()
    {
        await using var context = TestDataFactory.CreateContext();
        var customerId = Guid.NewGuid();
        var user = TestDataFactory.CreateUser(customerId, "stoyan", "stoyan@example.com");
        var profile = new CustomerProfile { UserId = customerId, User = user };
        var order = CreateOrder(customerId, total: 12m, date: DateTime.UtcNow);
        order.Customer = profile;
        context.Users.Add(user);
        context.Set<CustomerProfile>().Add(profile);
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        var service = new OrderService(context);

        var all = Assert.Single(await service.GetAllOrdersAsync());
        var page = await service.GetAllOrdersPageAsync(page: 10, pageSize: 1);

        Assert.Equal("stoyan", all.CustomerUserName);
        Assert.Equal(1, page.CurrentPage);
        Assert.Single(page.Orders);
    }

    [Fact]
    public async Task GetOrderDetailsAsync_ReturnsOrderWithItems()
    {
        await using var context = TestDataFactory.CreateContext();
        var customerId = Guid.NewGuid();
        var user = TestDataFactory.CreateUser(customerId);
        var profile = new CustomerProfile { UserId = customerId, User = user };
        var category = TestDataFactory.CreateCategory();
        var dish = TestDataFactory.CreateDish(category, "Нудли", 8.69m);
        var order = CreateOrder(customerId, total: 8.69m, date: DateTime.UtcNow);
        order.Customer = profile;
        order.CustomerOrderItems.Add(new CustomerOrderItem
        {
            Id = Guid.NewGuid(),
            OrderId = order.OrderId,
            Order = order,
            DishId = dish.DishId,
            Dish = dish,
            Quantity = 1,
            Price = 8.69m
        });
        context.Users.Add(user);
        context.Set<CustomerProfile>().Add(profile);
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        var service = new OrderService(context);

        var details = await service.GetOrderDetailsAsync(order.OrderId);

        Assert.Equal("Нудли", Assert.Single(details!.CustomerOrderItems).DishName);
    }

    [Fact]
    public async Task UpdateStatusAsync_RejectsUnknownStatus()
    {
        await using var context = TestDataFactory.CreateContext();
        var service = new OrderService(context);

        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateStatusAsync(Guid.NewGuid(), "Неизвестен"));
    }

    [Fact]
    public async Task UpdateStatusAsync_UpdatesValidStatusAndThrowsForMissingOrder()
    {
        await using var context = TestDataFactory.CreateContext();
        var order = CreateOrder(Guid.NewGuid(), total: 10m, date: DateTime.UtcNow);
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        var service = new OrderService(context);

        await service.UpdateStatusAsync(order.OrderId, "Готова");

        Assert.Equal("Готова", order.Status);
        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateStatusAsync(Guid.NewGuid(), "Готова"));
    }

    [Fact]
    public void GetOrderStatuses_ReturnsAllowedStatuses()
    {
        var service = new OrderService(TestDataFactory.CreateContext());

        var statuses = service.GetOrderStatuses().ToList();

        Assert.Contains("Обработва се", statuses);
        Assert.Contains("Отказана", statuses);
    }

    private static Order CreateOrder(Guid customerId, decimal total, DateTime date)
    {
        return new Order
        {
            OrderId = Guid.NewGuid(),
            CustomerId = customerId,
            CustomerFullName = "Стоян Стоянов",
            CustomerPhone = "0888123456",
            FulfillmentType = "Вземане на място",
            OrderDate = date,
            TotalAmount = total,
            Status = "Обработва се"
        };
    }
}
