using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAlloraProjectViewModels.Reservation
{
    public class ReservationIndexViewModel
    {
        public Guid ReservationId { get; set; }

        [Required(ErrorMessage = "Номерът на масата е задължителен.")]
        [Range(1, 100, ErrorMessage = "Номерът на масата трябва да бъде между 1 и 100.")]
        public int TableNumber { get; set; }

        [Required(ErrorMessage = "Капацитетът на масата е задължителен.")]
        [Range(1, 20, ErrorMessage = "Капацитетът на масата трябва да бъде между 1 и 20 човека.")]
        public int CapacityOfTheTable { get; set; }

        public string Status { get; set; } = "Очаква одобрение";

        [Required(ErrorMessage = "Датата на резервацията е задължителна.")]
        [DataType(DataType.DateTime)]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Броят гости е задължителен.")]
        [Range(1, 20, ErrorMessage = "Броят гости трябва да бъде между 1 и 20.")]
        public int NumberOfGuests { get; set; }

        public string StatusOfTheTable { get; set; } = "Свободна";

        [StringLength(100, ErrorMessage = "Името на клиента не може да бъде по-дълго от 100 символа.")]
        public string? CustomerName { get; set; }

        [StringLength(100, ErrorMessage = "Името на служителя не може да бъде по-дълго от 100 символа.")]
        public string? EmployeeName { get; set; }

        public bool CanApprove => Status == "Очаква одобрение";

        public bool CanReject => Status == "Очаква одобрение" || Status == "Одобрена";
    }
}
