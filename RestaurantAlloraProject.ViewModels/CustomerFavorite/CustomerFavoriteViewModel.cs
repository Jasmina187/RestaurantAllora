using System;

namespace RestaurantAlloraProject.ViewModels.FavoriteDish
{
    public class FavoriteDishViewModel
    {
        public Guid DishId { get; set; }
        public string DishName { get; set; } = null!;
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = null!;
        public DateTime AddedOn { get; set; }
    }
}
