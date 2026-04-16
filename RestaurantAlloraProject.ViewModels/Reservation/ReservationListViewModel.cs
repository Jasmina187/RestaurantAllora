using System;
using System.Collections.Generic;

namespace RestaurantAlloraProjectViewModels.Reservation
{
    public class ReservationListViewModel
    {
        public List<ReservationIndexViewModel> Reservations { get; set; } = new List<ReservationIndexViewModel>();
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalReservations { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalReservations / (double)PageSize);
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}
