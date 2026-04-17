using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProjectData.Entities;

namespace RestaurantAlloraProjectTests;

public class AllergenServiceTests
{
    [Fact]
    public async Task GetAllAllergensAsync_ReturnsAllergensAsSelectModels()
    {
        await using var context = TestDataFactory.CreateContext();
        context.Allergens.AddRange(
            new Allergen { AllergenId = Guid.NewGuid(), AllergenName = "Глутен" },
            new Allergen { AllergenId = Guid.NewGuid(), AllergenName = "Яйца" });
        await context.SaveChangesAsync();

        var service = new AllergenService(context);

        var result = (await service.GetAllAllergensAsync()).ToList();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, a => a.AllergenName == "Глутен");
        Assert.Contains(result, a => a.AllergenName == "Яйца");
    }
}
