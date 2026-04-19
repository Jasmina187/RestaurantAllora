using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.ViewModels.User
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Потребителското име трябва да бъде между 3 и 50 символа.")]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Паролата трябва да бъде поне 6 символа.")]
        [Display(Name = "Парола")]
        public string Password { get; set; } = string.Empty;
    }
}
