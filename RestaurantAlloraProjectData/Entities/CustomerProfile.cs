using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class CustomerProfile
    {
        [Key]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<CustomerOrderItem> Orders { get; set; } = new List<CustomerOrderItem>();
        public ICollection<CustomerFavorite> Favorites { get; set; } = new List<CustomerFavorite>();
    }
}
