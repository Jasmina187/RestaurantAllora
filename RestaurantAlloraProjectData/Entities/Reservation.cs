using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class Reservation
    {

        [Key]
        public Guid ReservationId { get; set; }

        public Guid TableId { get; set; }
        public Table Table { get; set; } = null!;

        public Guid CustomerId { get; set; }
        public CustomerProfile Customer { get; set; } = null!;

        public Guid? EmployeeId { get; set; }
        public EmployeeProfile? Employee { get; set; }

        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Очаква одобрение";
    }
}
    