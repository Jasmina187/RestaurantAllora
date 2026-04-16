using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAlloraProjectViewModels.Dish
{
    public class DishEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Името на ястието е задължително.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Името на ястието трябва да бъде между 5 и 50 символа.")]
        public string NameOfTheDish { get; set; } = null!;

        [Required(ErrorMessage = "Описанието на ястието е задължително.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Описанието на ястието трябва да бъде между 5 и 100 символа.")]
        public string DescriptionOfTheDish { get; set; } = null!;

        [Required(ErrorMessage = "Цената на ястието е задължителна.")]
        [Range(0.01, 50.00, ErrorMessage = "Цената трябва да бъде между 0.01 и 50.")]
        public decimal PriceOfTheDish { get; set; }

        [Required(ErrorMessage = "Категорията е задължителна.")]
        public string CategoryOfTheDish { get; set; } = null!;

        [Display(Name = "Снимка")]
        public IFormFile? Picture { get; set; }

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        public List<Guid> SelectedAllergenIds { get; set; } = new();
    }
}
