using RestaurantAlloraProjectData.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantAlloraProjectViewModels.Reservation
{
    public class ReservationCreateViewModel
    {
        public Guid TableId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfGuests { get; set; }
        public List<SelectListItem> Tables { get; set; } = new List<SelectListItem>();

    }
}
