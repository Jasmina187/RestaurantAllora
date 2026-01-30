using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class CustomerFavorite
    {
        public Guid CustomerId { get; set; }
        public CustomerProfile Customer { get; set; } = null!;

        public Guid DishId { get; set; }
        public Dish Dish { get; set; } = null!;

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
    }
}
