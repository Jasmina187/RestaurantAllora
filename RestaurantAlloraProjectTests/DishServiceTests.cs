using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProject.ViewModels.Dish;
using RestaurantAlloraProjectViewModels.Dish;

namespace RestaurantAlloraProjectTests;

public class DishServiceTests
{
    [Fact]
    public async Task GetAllAsync_ReturnsDishesWithCategoryAndAllergens()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory("Десерти");
        var allergen = TestDataFactory.CreateAllergen("Мляко");
        var dish = TestDataFactory.CreateDish(category, "Чийзкейк", 5.62m);
        dish.DishAllergens.Add(new() { Dish = dish, Allergen = allergen });
        context.Categories.Add(category);
        context.Allergens.Add(allergen);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        var service = new DishService(context);

        var result = Assert.Single(await service.GetAllAsync());

        Assert.Equal("Десерти", result.CategoryOfTheDish);
        Assert.Contains("Мляко", result.AllergenNames);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsDishEditModelOrNull()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory("Салати");
        var allergen = TestDataFactory.CreateAllergen("Ядки");
        var dish = TestDataFactory.CreateDish(category, "Истанбул");
        dish.DishAllergens.Add(new() { Dish = dish, Allergen = allergen });
        context.Categories.Add(category);
        context.Allergens.Add(allergen);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        var service = new DishService(context);

        var found = await service.GetByIdAsync(dish.DishId);
        var missing = await service.GetByIdAsync(Guid.NewGuid());

        Assert.Equal("Истанбул", found?.NameOfTheDish);
        Assert.Contains(allergen.AllergenId, found!.SelectedAllergenIds);
        Assert.Null(missing);
    }

    [Fact]
    public async Task CreateAsync_SavesDishWithSelectedAllergens()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory("Основни ястия");
        var allergen = TestDataFactory.CreateAllergen("Соя");
        context.Categories.Add(category);
        context.Allergens.Add(allergen);
        await context.SaveChangesAsync();
        var service = new DishService(context);

        await service.CreateAsync(new DishCreateViewModel
        {
            NameOfTheDish = "Пиле пад капрао",
            DescriptionOfTheDish = "Описание за пад капрао",
            PriceOfTheDish = 12.50m,
            CategoryOfTheDish = "Основни ястия",
            ImageUrl = "/img/chicken.png",
            SelectedAllergenIds = new List<Guid> { allergen.AllergenId }
        });

        var dish = await context.Dishes.Include(d => d.DishAllergens).SingleAsync();
        Assert.Equal(category.CategoryId, dish.CategoryId);
        Assert.Contains(dish.DishAllergens, da => da.AllergenId == allergen.AllergenId);
    }

    [Fact]
    public async Task CreateAsync_ThrowsWhenCategoryDoesNotExist()
    {
        await using var context = TestDataFactory.CreateContext();
        var service = new DishService(context);

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(new DishCreateViewModel
        {
            NameOfTheDish = "Ново ястие",
            DescriptionOfTheDish = "Описание",
            PriceOfTheDish = 9m,
            CategoryOfTheDish = "Липсваща",
            ImageUrl = "/img/test.png"
        }));
    }

    [Fact]
    public async Task UpdateAsync_ReplacesDishAllergens()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory("Салати");
        var oldAllergen = TestDataFactory.CreateAllergen("Ядки");
        var newAllergen = TestDataFactory.CreateAllergen("Мляко");
        var dish = TestDataFactory.CreateDish(category, "Истанбул");
        dish.DishAllergens.Add(new() { Dish = dish, Allergen = oldAllergen });
        context.Categories.Add(category);
        context.Allergens.AddRange(oldAllergen, newAllergen);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        var service = new DishService(context);

        await service.UpdateAsync(new DishEditViewModel
        {
            Id = dish.DishId,
            NameOfTheDish = "Истанбул салата",
            DescriptionOfTheDish = "Ново описание",
            PriceOfTheDish = 11m,
            CategoryOfTheDish = "Салати",
            ImageUrl = "/img/istanbul.png",
            SelectedAllergenIds = new List<Guid> { newAllergen.AllergenId }
        });

        var updated = await context.Dishes.Include(d => d.DishAllergens).SingleAsync(d => d.DishId == dish.DishId);
        Assert.Equal("Истанбул салата", updated.NameOfTheDish);
        Assert.DoesNotContain(updated.DishAllergens, da => da.AllergenId == oldAllergen.AllergenId);
        Assert.Contains(updated.DishAllergens, da => da.AllergenId == newAllergen.AllergenId);
    }

    [Fact]
    public async Task UpdateAsync_ThrowsWhenDishOrCategoryIsMissing()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory("Салати");
        var dish = TestDataFactory.CreateDish(category, "Фатуш");
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        var service = new DishService(context);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.UpdateAsync(new DishEditViewModel
        {
            Id = Guid.NewGuid(),
            NameOfTheDish = "Липсва",
            DescriptionOfTheDish = "Описание",
            PriceOfTheDish = 9m,
            CategoryOfTheDish = "Салати",
            ImageUrl = "/img/test.png"
        }));

        await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateAsync(new DishEditViewModel
        {
            Id = dish.DishId,
            NameOfTheDish = "Фатуш",
            DescriptionOfTheDish = "Описание",
            PriceOfTheDish = 9m,
            CategoryOfTheDish = "Липсваща",
            ImageUrl = "/img/test.png"
        }));
    }

    [Fact]
    public async Task DeleteAsync_RemovesExistingDishAndIgnoresMissingDish()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory();
        var dish = TestDataFactory.CreateDish(category);
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        var service = new DishService(context);

        await service.DeleteAsync(Guid.NewGuid());
        await service.DeleteAsync(dish.DishId);

        Assert.Empty(context.Dishes);
    }

    [Fact]
    public void GetCategories_ReturnsStaticCategories()
    {
        var service = new DishService(TestDataFactory.CreateContext());

        var categories = service.GetCategories().ToList();

        Assert.Contains("Салати", categories);
        Assert.Contains("Напитки", categories);
    }
}
