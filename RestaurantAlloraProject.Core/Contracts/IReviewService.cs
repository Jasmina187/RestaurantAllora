using RestaurantAlloraProjectViewModels.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Contracts
{
    public interface IReviewService
    {
        Task AddReviewAsync(ReviewViewModel model);
        Task<IEnumerable<ReviewViewModel>> GetDishReviewsAsync(Guid dishId);
    }
}
