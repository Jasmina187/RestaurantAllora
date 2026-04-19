using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectTests;

public class CustomerOrderItemServiceTests
{
    [Fact]
    public async Task UpdateQuantityAsync_ChangesExistingItemQuantity()
    {
        await using var context = TestDataFactory.CreateContext();
        var item = new CustomerOrderItem { Id = Guid.NewGuid(), Quantity = 1, Price = 9m };
        context.CustomerOrderItems.Add(item);
        await context.SaveChangesAsync();
        var service = new CustomerOrderItemService(context);

        await service.UpdateQuantityAsync(item.Id, 4);

        Assert.Equal(4, item.Quantity);
    }

    [Fact]
    public async Task UpdateQuantityAsync_IgnoresMissingItem()
    {
        await using var context = TestDataFactory.CreateContext();
        var service = new CustomerOrderItemService(context);

        await service.UpdateQuantityAsync(Guid.NewGuid(), 4);

        Assert.Empty(context.CustomerOrderItems);
    }

    [Fact]
    public async Task RemoveItemAsync_RemovesExistingItem()
    {
        await using var context = TestDataFactory.CreateContext();
        var item = new CustomerOrderItem { Id = Guid.NewGuid(), Quantity = 1, Price = 9m };
        context.CustomerOrderItems.Add(item);
        await context.SaveChangesAsync();
        var service = new CustomerOrderItemService(context);

        await service.RemoveItemAsync(item.Id);

        Assert.False(await context.CustomerOrderItems.AnyAsync());
    }

    [Fact]
    public async Task RemoveItemAsync_IgnoresMissingItem()
    {
        await using var context = TestDataFactory.CreateContext();
        var service = new CustomerOrderItemService(context);

        await service.RemoveItemAsync(Guid.NewGuid());

        Assert.Empty(context.CustomerOrderItems);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsItemViewModelOrNull()
    {
        await using var context = TestDataFactory.CreateContext();
        var item = new CustomerOrderItem
        {
            Id = Guid.NewGuid(),
            DishId = Guid.NewGuid(),
            OrderId = Guid.NewGuid(),
            Quantity = 2,
            Price = 5m
        };
        context.CustomerOrderItems.Add(item);
        await context.SaveChangesAsync();
        var service = new CustomerOrderItemService(context);

        var found = await service.GetByIdAsync(item.Id);
        var missing = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Equal(item.DishId, found?.DishId);
        Assert.Null(missing);
    }
}
