using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAlloraProject.ViewModels.User
{
    public class UserEditViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Потребителското име е задължително.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Потребителското име трябва да бъде между 3 и 100 символа.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Моля въведете валиден имейл.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ролята е задължителна.")]
        public string Role { get; set; } = string.Empty;
    }
}
