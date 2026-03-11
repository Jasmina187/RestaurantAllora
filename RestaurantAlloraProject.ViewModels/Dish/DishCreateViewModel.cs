using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantAlloraProject.ViewModels.Dish
{
    public class DishCreateViewModel
    {
        [Required(ErrorMessage = "Името на ястието е задължително.")]
        [StringLength(20,MinimumLength = 5,ErrorMessage = "Името на ястието трябва да бъде между 5 и 20 символа.")]
        public string NameOfTheDish { get; set; } = null!;

        [Required(ErrorMessage = "Описанието на ястието е задължително.")]
        [StringLength(50, MinimumLength = 5,ErrorMessage = "Описанието на ястието трябва да бъде между 5 и 50 символа.")]
        public string DescriptionOfTheDish { get; set; } = null!;

        [Required(ErrorMessage = "Цената на ястието е задължителна.")]
        [Range(0.01,50.00,ErrorMessage ="Цената трябва да бъде между 0.01 и 50")]
        public decimal PriceOfTheDish { get; set; }

        [Required]
        public string CategoryOfTheDish { get; set; } = null!;

        [Required(ErrorMessage = "URL е задължителeн.")]
        [Url(ErrorMessage = "Невалиден URL адрес. ")]
        public string ImageUrl { get; set; } = null!;

        public List<Guid> SelectedAllergens { get; set; } = new();
        public List<Guid> SelectedAllergenIds { get; set; } = new();
        public Microsoft.AspNetCore.Mvc.Rendering.MultiSelectList? Allergens { get; set; } 
    }
}
