using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAlloraProjectWeb.Models.Dish
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
        public List<string> AllergenNames { get; set; } = new();
        public List<Guid> SelectedAllergens { get; set; } = new();
        public MultiSelectList? Allergens { get; set; }
    }
}
