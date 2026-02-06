using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class Dish
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
        [Required]
        public string ImageUrl { get; set; }
        public ICollection<DishAllergen> DishAllergens { get; set; } = new List<DishAllergen>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<CustomerOrderItem> OrderItems { get; set; } = new List<CustomerOrderItem>();
        public ICollection<CustomerFavorite> FavoritedBy { get; set; } = new List<CustomerFavorite>();
    }
}
