using System.Collections.Generic;

namespace RestaurantAlloraProjectViewModels.Reservation
{
    public class ReservationManagementViewModel
    {
        public ReservationFilterViewModel Filter { get; set; } = new ReservationFilterViewModel();
        public List<ReservationIndexViewModel> Reservations { get; set; } = new List<ReservationIndexViewModel>();
        public List<string> Statuses { get; set; } = new List<string>();
    }
}
