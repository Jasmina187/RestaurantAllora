using System.ComponentModel.DataAnnotations;

namespace RestaurantAlloraProject.ViewModels.User
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имейл адресът е задължителен.")]
        [EmailAddress(ErrorMessage = "Моля въведете валиден имейл адрес.")]
        public string EmailAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "Новата парола е задължителна.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Паролата трябва да бъде между 6 и 20 символа.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Моля потвърдете паролата.")]
        [Compare(nameof(Password), ErrorMessage = "Паролите не съвпадат.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
