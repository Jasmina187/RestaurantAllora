using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class Allergen
    {
        [Key]
        public Guid AllergenId { get; set; }

        [Required, MaxLength(100)]
        public string AllergenName { get; set; } = null!;


        public ICollection<DishAllergen> DishAllergens { get; set; } = new List<DishAllergen>();
    }
}
