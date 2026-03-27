using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectViewModels.Allergen
{
    public class SelectAllergenViewModel
    {
        public Guid Id { get; set; }

        public string AllergenName { get; set; } = null!;
    }
}
