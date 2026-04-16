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

        [Required(ErrorMessage = "Изберете доставка или вземане на място.")]
        [MaxLength(30)]
        public string FulfillmentType { get; set; } = "Вземане на място";

        [Required(ErrorMessage = "Името е задължително.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Името трябва да бъде между 3 и 100 символа.")]
        public string CustomerFullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефонът е задължителен.")]
        [Phone(ErrorMessage = "Моля въведете валиден телефон.")]
        [StringLength(30, ErrorMessage = "Телефонът не може да бъде по-дълъг от 30 символа.")]
        public string CustomerPhone { get; set; } = string.Empty;

        [StringLength(300, ErrorMessage = "Адресът не може да бъде по-дълъг от 300 символа.")]
        public string? DeliveryAddress { get; set; }

        [StringLength(500, ErrorMessage = "Бележката не може да бъде по-дълга от 500 символа.")]
        public string? Notes { get; set; }

        [Required]
        public Guid CustomerId { get; set; }
        public string? CustomerUserName { get; set; }
        public string? CustomerEmail { get; set; }
        public ICollection<CustomerOrderItemViewModel> CustomerOrderItems { get; set; } = new List<CustomerOrderItemViewModel>();
    }
}
