using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProjectViewModels.Category;

namespace RestaurantAlloraProjectTests;

public class CategoryServiceTests
{
    [Fact]
    public async Task CreateAsync_TrimsNameAndSavesCategory()
    {
        await using var context = TestDataFactory.CreateContext();
        var service = new CategoryService(context);

        await service.CreateAsync(new CategoryViewModel { Name = "  Нови предложения  " });

        var category = await context.Categories.SingleAsync();
        Assert.Equal("Нови предложения", category.Name);
    }

    [Fact]
    public async Task UpdateAsync_RenamesCategoryAndItsDishes()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory("Салати");
        var dish = TestDataFactory.CreateDish(category, "Фатуш");
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        await service.UpdateAsync(new CategoryViewModel
        {
            CategoryId = category.CategoryId,
            Name = "Свежи салати"
        });

        Assert.Equal("Свежи салати", category.Name);
        Assert.Equal("Свежи салати", dish.CategoryOfTheDish);
    }

    [Fact]
    public async Task DeleteAsync_ThrowsWhenCategoryHasDishes()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory();
        context.Categories.Add(category);
        context.Dishes.Add(TestDataFactory.CreateDish(category));
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteAsync(category.CategoryId));
    }
}
