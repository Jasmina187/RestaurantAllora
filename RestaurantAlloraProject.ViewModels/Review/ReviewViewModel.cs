using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectViewModels.Review
{
    public class ReviewViewModel
    {
        public Guid ReviewId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid DishId { get; set; }

        public string? DishName { get; set; }

        [Required(ErrorMessage = "Моля, изберете оценка.")]
        [Range(1, 5, ErrorMessage = "Оценката трябва да е между 1 и 5.")]
        public int Rating { get; set; }

        [MaxLength(2000, ErrorMessage = "Коментарът не може да е по-дълъг от 2000 символа.")]
        public string? Comment { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
