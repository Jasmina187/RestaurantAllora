using System.ComponentModel.DataAnnotations;

namespace RestaurantAlloraProject.ViewModels.User
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Имейл адресът е задължителен.")]
        [EmailAddress(ErrorMessage = "Моля въведете валиден имейл адрес.")]
        public string EmailAddress { get; set; } = string.Empty;
    }
}
