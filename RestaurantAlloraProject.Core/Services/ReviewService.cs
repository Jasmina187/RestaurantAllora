using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Services
{
    public class ReviewService: IReviewService
    {
        private readonly RestaurantAlloraProjectContext _context;
        public ReviewService(RestaurantAlloraProjectContext context)
        {
            _context = context;
        }
        public async Task AddReviewAsync(ReviewViewModel model)
        {
            var profileExists = await _context.Set<CustomerProfile>()
                .AnyAsync(cp => cp.UserId == model.CustomerId);

            if (!profileExists)
            {
                _context.Add(new CustomerProfile { UserId = model.CustomerId });
                await _context.SaveChangesAsync();
            }

            var existingReview = await _context.Reviews
                .FirstOrDefaultAsync(r => r.CustomerId == model.CustomerId && r.DishId == model.DishId);

            if (existingReview != null)
            {
                existingReview.Rating = model.Rating;
                existingReview.Comment = model.Comment;
                existingReview.CreatedOn = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                return;
            }

            var review = new Review
            {
                ReviewId = Guid.NewGuid(),
                CustomerId = model.CustomerId,
                DishId = model.DishId,
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedOn = DateTime.UtcNow
            };

            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReviewViewModel>> GetAllReviewsAsync()
        {
            return await _context.Reviews
                .Include(r => r.Dish)
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new ReviewViewModel
                {
                    ReviewId = r.ReviewId,
                    CustomerId = r.CustomerId,
                    DishId = r.DishId,
                    DishName = r.Dish.NameOfTheDish,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedOn = r.CreatedOn
                })
                .ToListAsync();
        }

        public async Task<ReviewListViewModel> GetAllReviewsPageAsync(int page, int pageSize)
        {
            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);

            var totalReviews = await _context.Reviews.CountAsync();
            var totalPages = (int)Math.Ceiling(totalReviews / (double)pageSize);

            if (totalPages > 0 && page > totalPages)
            {
                page = totalPages;
            }

            var reviews = await _context.Reviews
                .Include(r => r.Dish)
                .OrderByDescending(r => r.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(r => new ReviewViewModel
                {
                    ReviewId = r.ReviewId,
                    CustomerId = r.CustomerId,
                    DishId = r.DishId,
                    DishName = r.Dish.NameOfTheDish,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedOn = r.CreatedOn
                })
                .ToListAsync();

            return new ReviewListViewModel
            {
                Reviews = reviews,
                CurrentPage = page,
                PageSize = pageSize,
                TotalReviews = totalReviews
            };
        }

        public async Task<IEnumerable<ReviewViewModel>> GetDishReviewsAsync(Guid dishId)
        {
            return await _context.Reviews
                .Where(r => r.DishId == dishId)
                .Select(r => new ReviewViewModel
                {
                    ReviewId = r.ReviewId,
                    CustomerId = r.CustomerId,
                    DishId = r.DishId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedOn = r.CreatedOn
                }).ToListAsync();
        }
    }
}
