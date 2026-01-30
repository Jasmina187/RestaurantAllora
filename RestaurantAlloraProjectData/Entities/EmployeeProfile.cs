using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class EmployeeProfile
    {
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Position { get; set; } = null!;
        public ICollection<CustomerOrderItem> HandledOrders { get; set; } = new List<CustomerOrderItem>();
        public ICollection<Reservation> HandledReservations { get; set; } = new List<Reservation>();
    }
}
