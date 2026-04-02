using RestaurantAlloraProjectViewModels.CustomerOrderItem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectViewModels.Order
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Общата сума е задължителна.")]
        [Range(0.01, 10000.00, ErrorMessage = "Сумата трябва да е положително число.")]
        public decimal TotalAmount { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Обработва се";
        [Required]
        public Guid CustomerId { get; set; }
        public ICollection<CustomerOrderItemViewModel> CustomerOrderItems { get; set; } = new List<CustomerOrderItemViewModel>();
    }
}
