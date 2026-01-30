using System.ComponentModel.DataAnnotations;

namespace RestaurantAlloraProjectWeb.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(60, MinimumLength = 10)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; }
    }
}
