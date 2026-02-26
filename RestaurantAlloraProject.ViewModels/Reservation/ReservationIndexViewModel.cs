using RestaurantAlloraProjectData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectViewModels.Reservation
{
    public  class ReservationIndexViewModel
    {
        public Guid ReservationId { get; set; }  
        public int TableNumber { get; set; }
        public int CapacityOfTheTable { get; set; }
        public string Status { get; set; } = "Очаква одобрение";
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string StatusOfTheTable { get; set; } = "Заета";
        public string? CustomerName { get; set; }

        public string? EmployeeName { get; set; }

       

       
        
    }
}
