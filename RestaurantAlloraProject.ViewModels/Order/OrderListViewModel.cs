using System;
using System.Collections.Generic;

namespace RestaurantAlloraProjectViewModels.Order
{
    public class OrderListViewModel
    {
        public IEnumerable<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalOrders { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalOrders / (double)PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
