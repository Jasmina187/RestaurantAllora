using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.ViewModels.Dish
{
    public class DishViewModel
    {
        [Key]
        public Guid DishId { get; set; }

        [Required, MaxLength(100)]
        public string NameOfTheDish { get; set; } = null!;

        [Required, MaxLength(200)]
        public string DescriptionOfTheDish { get; set; } = null!;

        [Required]
        public decimal PriceOfTheDish { get; set; }

        [Required, MaxLength(100)]
        public string CategoryOfTheDish { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public List<string> AllergenNames { get; set; } = new();
        public List<Guid> SelectedAllergens { get; set; } = new();
        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Allergens { get; set; }
    }
}
