using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class Table
    {
        [Key]
        public Guid TableId { get; set; }

        [Required]
        public int TableNumber { get; set; }

        [Required]
        public int CapacityOfTheTable { get; set; }

        [Required, MaxLength(100)]
        public string StatusOfTheTable { get; set; } = "Свободна";
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
