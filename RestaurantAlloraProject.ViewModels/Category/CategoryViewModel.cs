using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAlloraProjectViewModels.Category
{
    public class CategoryViewModel
    {
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Името на категорията е задължително.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Името трябва да бъде между 2 и 100 символа.")]
        public string Name { get; set; } = string.Empty;
    }
}
