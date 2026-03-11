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
        [Required(ErrorMessage = "Датата на резервацията е задължителна.")]
        [DataType(DataType.DateTime)]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Броят гости е задължителен.")]
        [Range(1, 20, ErrorMessage = "Броят гости трябва да бъде между 1 и 20.")]
        public int NumberOfGuests { get; set; }
        public List<SelectListItem> Tables { get; set; } = new List<SelectListItem>();

    }
}
