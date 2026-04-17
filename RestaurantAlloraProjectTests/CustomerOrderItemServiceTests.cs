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
}
