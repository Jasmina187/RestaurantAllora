using System;
using System.Collections.Generic;

namespace RestaurantAlloraProjectViewModels.Review
{
    public class ReviewListViewModel
    {
        public IEnumerable<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalReviews { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalReviews / (double)PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
