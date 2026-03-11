using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.ViewModels.User
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Потребителското име трябва да бъде между 5 и 20 символа.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Имейл адресът е задължителен.")]
        [EmailAddress(ErrorMessage = "Моля въведете валиден имейл адрес.")]
        [StringLength(60, MinimumLength = 10, ErrorMessage = "Имейлът трябва да бъде между 10 и 60 символа.")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Паролата е задължителна.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Паролата трябва да бъде между 5 и 20 символа.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Моля потвърдете паролата.")]
        [Compare(nameof(Password), ErrorMessage = "Паролите не съвпадат.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Ролята е задължителна.")]
        public string Role { get; set; }
    }
        
}
