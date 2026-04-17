using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectTests;

public class CustomerFavoriteServiceTests
{
    [Fact]
    public async Task ToggleFavoriteAsync_AddsFavoriteAndCreatesCustomerProfile()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory();
        var dish = TestDataFactory.CreateDish(category, "Пуешки стек");
        var customerId = Guid.NewGuid();
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        var service = new CustomerFavoriteService(context);

        await service.ToggleFavoriteAsync(dish.DishId, customerId);

        Assert.True(await context.Set<CustomerProfile>().AnyAsync(cp => cp.UserId == customerId));
        Assert.True(await context.CustomerFavorites.AnyAsync(cf => cf.CustomerId == customerId && cf.DishId == dish.DishId));
    }

    [Fact]
    public async Task ToggleFavoriteAsync_RemovesExistingFavorite()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory();
        var dish = TestDataFactory.CreateDish(category, "Пуешки стек");
        var customerId = Guid.NewGuid();
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        var service = new CustomerFavoriteService(context);

        await service.ToggleFavoriteAsync(dish.DishId, customerId);
        await service.ToggleFavoriteAsync(dish.DishId, customerId);

        Assert.Empty(context.CustomerFavorites);
    }
}
