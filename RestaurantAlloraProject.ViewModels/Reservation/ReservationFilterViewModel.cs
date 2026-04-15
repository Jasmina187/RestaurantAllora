using System;

namespace RestaurantAlloraProjectViewModels.Reservation
{
    public class ReservationFilterViewModel
    {
        public string? Status { get; set; }
        public DateTime? Date { get; set; }
        public string? SearchTerm { get; set; }
        public int? TableNumber { get; set; }
    }
}
