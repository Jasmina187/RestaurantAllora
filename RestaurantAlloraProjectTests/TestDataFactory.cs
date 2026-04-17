using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectTests;

internal static class TestDataFactory
{
    public static RestaurantAlloraProjectContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<RestaurantAlloraProjectContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new RestaurantAlloraProjectContext(options);
    }

    public static Category CreateCategory(string name = "Салати")
    {
        return new Category
        {
            CategoryId = Guid.NewGuid(),
            Name = name
        };
    }

    public static Allergen CreateAllergen(string name = "Яйца и продукти от тях")
    {
        return new Allergen
        {
            AllergenId = Guid.NewGuid(),
            AllergenName = name
        };
    }

    public static Dish CreateDish(Category category, string name = "Тестово ястие", decimal price = 10m)
    {
        return new Dish
        {
            DishId = Guid.NewGuid(),
            NameOfTheDish = name,
            DescriptionOfTheDish = "Описание за тестово ястие",
            PriceOfTheDish = price,
            CategoryId = category.CategoryId,
            Category = category,
            CategoryOfTheDish = category.Name,
            ImageUrl = "/img/test.png"
        };
    }

    public static Table CreateTable(int number = 1, int capacity = 4, string status = "Свободна")
    {
        return new Table
        {
            TableId = Guid.NewGuid(),
            TableNumber = number,
            CapacityOfTheTable = capacity,
            StatusOfTheTable = status
        };
    }

    public static User CreateUser(Guid id, string userName = "test-user", string email = "test@example.com")
    {
        return new User
        {
            Id = id,
            UserName = userName,
            Email = email
        };
    }
}
