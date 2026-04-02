using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectViewModels.CustomerOrderItem
{
    public class CustomerOrderItemViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid DishId { get; set; }

        public string DishName { get; set; } = null!;

        [Required(ErrorMessage = "Моля, въведете количество.")]
        [Range(1, 100, ErrorMessage = "Количеството трябва да е между 1 и 100.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, 2000.00, ErrorMessage = "Цената трябва да е положително число.")]
        public decimal Price { get; set; }

        public Guid? EmployeeId { get; set; }
    }
}
