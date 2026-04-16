using System;

namespace RestaurantAlloraProjectViewModels.Reservation
{
    public class ReservationFilterViewModel
    {
        public string? Status { get; set; }
        public DateTime? Date { get; set; }
        public string? SearchTerm { get; set; }
        public int? TableNumber { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
