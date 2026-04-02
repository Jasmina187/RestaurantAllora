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
        public async Task<IEnumerable<ReviewViewModel>> GetDishReviewsAsync(Guid dishId)
        {
            return await _context.Reviews
                .Where(r => r.DishId == dishId)
                .Select(r => new ReviewViewModel
                {
                    ReviewId = r.ReviewId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedOn = r.CreatedOn
                }).ToListAsync();
        }
    }
}
