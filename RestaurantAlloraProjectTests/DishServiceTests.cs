using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProject.ViewModels.Dish;
using RestaurantAlloraProjectViewModels.Dish;

namespace RestaurantAlloraProjectTests;

public class DishServiceTests
{
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
}
