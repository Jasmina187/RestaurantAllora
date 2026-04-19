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
    public async Task GetAllReviewsAsync_ReturnsReviewsWithDishNamesOrderedByDate()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory();
        var dish = TestDataFactory.CreateDish(category, "Чийзкейк");
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        context.Reviews.AddRange(
            new Review { ReviewId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), DishId = dish.DishId, Dish = dish, Rating = 3, CreatedOn = DateTime.UtcNow.AddDays(-1) },
            new Review { ReviewId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), DishId = dish.DishId, Dish = dish, Rating = 5, CreatedOn = DateTime.UtcNow });
        await context.SaveChangesAsync();
        var service = new ReviewService(context);

        var reviews = (await service.GetAllReviewsAsync()).ToList();

        Assert.Equal(5, reviews[0].Rating);
        Assert.All(reviews, r => Assert.Equal("Чийзкейк", r.DishName));
    }

    [Fact]
    public async Task GetAllReviewsPageAsync_ClampsPageAndReturnsMetadata()
    {
        await using var context = TestDataFactory.CreateContext();
        var category = TestDataFactory.CreateCategory();
        var dish = TestDataFactory.CreateDish(category, "Чийзкейк");
        context.Categories.Add(category);
        context.Dishes.Add(dish);
        context.Reviews.AddRange(
            new Review { ReviewId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), DishId = dish.DishId, Dish = dish, Rating = 3 },
            new Review { ReviewId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), DishId = dish.DishId, Dish = dish, Rating = 5 });
        await context.SaveChangesAsync();
        var service = new ReviewService(context);

        var page = await service.GetAllReviewsPageAsync(page: 10, pageSize: 1);

        Assert.Equal(2, page.TotalReviews);
        Assert.Equal(2, page.CurrentPage);
        Assert.Single(page.Reviews);
    }

    [Fact]
    public async Task GetDishReviewsAsync_ReturnsOnlyDishReviews()
    {
        await using var context = TestDataFactory.CreateContext();
        var dishId = Guid.NewGuid();
        context.Reviews.AddRange(
            new Review { ReviewId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), DishId = dishId, Rating = 5 },
            new Review { ReviewId = Guid.NewGuid(), CustomerId = Guid.NewGuid(), DishId = Guid.NewGuid(), Rating = 1 });
        await context.SaveChangesAsync();
        var service = new ReviewService(context);

        var review = Assert.Single(await service.GetDishReviewsAsync(dishId));

        Assert.Equal(5, review.Rating);
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
