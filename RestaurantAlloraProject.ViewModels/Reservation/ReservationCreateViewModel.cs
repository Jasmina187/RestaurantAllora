using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAlloraProjectViewModels.Reservation
{
    public class ReservationCreateViewModel
    {
        public const int MaxAdvanceReservationDays = 90;
        public const int MaxGuestCount = 10;

        public Guid TableId { get; set; }

        [Required(ErrorMessage = "Датата на резервацията е задължителна.")]
        [DataType(DataType.DateTime)]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Броят гости е задължителен.")]
        [Range(1, MaxGuestCount, ErrorMessage = "Броят гости трябва да бъде между 1 и 10.")]
        public int NumberOfGuests { get; set; }

        public List<SelectOptionViewModel> Tables { get; set; } = new List<SelectOptionViewModel>();
    }
}
