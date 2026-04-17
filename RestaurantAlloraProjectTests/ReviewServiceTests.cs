using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Review;

namespace RestaurantAlloraProjectTests;

public class ReviewServiceTests
{
    [Fact]
    public async Task AddReviewAsync_CreatesReviewAndCustomerProfile()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory();
        var dish = TestDataFactory.CreateDish(category, "Пистачио чийзкейк");
        var customerId = Guid.NewGuid();
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        await context.SaveChangesAsync();
        var service = new ReviewService(context);

        await service.AddReviewAsync(new ReviewViewModel
        {
            CustomerId = customerId,
            DishId = dish.DishId,
            Rating = 5,
            Comment = "Чудесно."
        });

        var review = await context.Reviews.SingleAsync();
        Assert.Equal(5, review.Rating);
        Assert.True(await context.Set<CustomerProfile>().AnyAsync(cp => cp.UserId == customerId));
    }

    [Fact]
    public async Task AddReviewAsync_UpdatesExistingReviewForSameDishAndCustomer()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory();
        var dish = TestDataFactory.CreateDish(category, "Пистачио чийзкейк");
        var customerId = Guid.NewGuid();
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        context.Set<CustomerProfile>().Add(new CustomerProfile { UserId = customerId });
        context.Reviews.Add(new Review
        {
            ReviewId = Guid.NewGuid(),
            CustomerId = customerId,
            DishId = dish.DishId,
            Rating = 2,
            Comment = "Старо"
        });
        await context.SaveChangesAsync();
        var service = new ReviewService(context);

        await service.AddReviewAsync(new ReviewViewModel
        {
            CustomerId = customerId,
            DishId = dish.DishId,
            Rating = 4,
            Comment = "По-добре"
        });

        var review = await context.Reviews.SingleAsync();
        Assert.Equal(4, review.Rating);
        Assert.Equal("По-добре", review.Comment);
    }
}
