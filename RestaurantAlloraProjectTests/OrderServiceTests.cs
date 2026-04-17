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
    public async Task UpdateStatusAsync_RejectsUnknownStatus()
    {
        await using var context = TestDataFactory.CreateContext();
        var service = new OrderService(context);

        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateStatusAsync(Guid.NewGuid(), "Неизвестен"));
    }
}
