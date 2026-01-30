using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class DishAllergen
    {
        public Guid DishId { get; set; }
        public Dish Dish { get; set; } = null!;
        public Guid AllergenId { get; set; }
        public Allergen Allergen { get; set; } = null!;
    }
}
