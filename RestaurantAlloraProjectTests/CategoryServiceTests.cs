using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProjectViewModels.Category;

namespace RestaurantAlloraProjectTests;

public class CategoryServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsCategoriesOrderedByName()
    {
        await using var context = TestDataFactory.CreateContext();
        context.Categories.AddRange(
            TestDataFactory.CreateCategory("Десерти"),
            TestDataFactory.CreateCategory("Салати"));
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        var result = (await service.GetAllAsync()).ToList();

        Assert.Equal(new[] { "Десерти", "Салати" }, result.Select(c => c.Name));
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCategoryOrNull()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory("Десерти");
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        var found = await service.GetByIdAsync(category.CategoryId);
        var missing = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Equal("Десерти", found?.Name);
        Assert.Null(missing);
    }

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
    public async Task CreateAsync_ThrowsWhenNameAlreadyExists()
    {
        await using var context = TestDataFactory.CreateContext();
        context.Categories.Add(TestDataFactory.CreateCategory("Салати"));
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(new CategoryViewModel { Name = "Салати" }));
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
    public async Task UpdateAsync_ThrowsWhenCategoryIsMissingOrNameIsDuplicate()
    {
        await using var context = TestDataFactory.CreateContext();
        var existing = TestDataFactory.CreateCategory("Салати");
        var other = TestDataFactory.CreateCategory("Десерти");
        context.Categories.AddRange(existing, other);
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.UpdateAsync(new CategoryViewModel
        {
            CategoryId = Guid.NewGuid(),
            Name = "Няма"
        }));

        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateAsync(new CategoryViewModel
        {
            CategoryId = other.CategoryId,
            Name = "Салати"
        }));
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

    [Fact]
    public async Task DeleteAsync_RemovesUnusedCategoryAndIgnoresMissingCategory()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory("Напитки");
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        await service.DeleteAsync(Guid.NewGuid());
        await service.DeleteAsync(category.CategoryId);

        Assert.Empty(context.Categories);
    }

    [Fact]
    public async Task GetCategoryNamesAsync_ReturnsOrderedNames()
    {
        await using var context = TestDataFactory.CreateContext();
        context.Categories.AddRange(
            TestDataFactory.CreateCategory("Салати"),
            TestDataFactory.CreateCategory("Десерти"));
        await context.SaveChangesAsync();
        var service = new CategoryService(context);

        var names = await service.GetCategoryNamesAsync();

        Assert.Equal(new[] { "Десерти", "Салати" }, names);
    }
}
