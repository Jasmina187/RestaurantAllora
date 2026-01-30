using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class Review
    {
        [Key]
        public Guid ReviewId { get; set; }

        public Guid CustomerId { get; set; }
        public CustomerProfile Customer { get; set; } = null!;

        public Guid DishId { get; set; }
        public Dish Dish { get; set; } = null!;

        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(2000)]
        public string? Comment { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
