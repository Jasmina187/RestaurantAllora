using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectViewModels.Table
{
    public class TableViewModel
    {
        public Guid TableId { get; set; }
        [Required(ErrorMessage = "Номерът на масата е задължителен.")]
        [Range(1, 500, ErrorMessage = "Номерът на масата трябва да бъде между 1 и 500.")]
        public int TableNumber { get; set; }

        [Required(ErrorMessage = "Капацитетът на масата е задължителен.")]
        [Range(1, 10, ErrorMessage = "Капацитетът на масата трябва да бъде между 1 и 10 човека.")]
        public int CapacityOfTheTable { get; set; }
        [StringLength(20, ErrorMessage = "Статусът не може да бъде по-дълъг от 20 символа.")]
        public string? StatusOfTheTable { get; set; } = "Свободна";

        public DateTime? NextReservationStart { get; set; }

        public DateTime? NextReservationEnd => NextReservationStart?.AddHours(3);

        public List<DateTime> ActiveReservationStarts { get; set; } = new List<DateTime>();

        public List<DateTime> PendingReservationStarts { get; set; } = new List<DateTime>();
    }
}
