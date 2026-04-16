using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Обработва се";
        public string FulfillmentType { get; set; } = "Вземане на място";
        public string CustomerFullName { get; set; } = null!;
        public string CustomerPhone { get; set; } = null!;
        public string? DeliveryAddress { get; set; }
        public string? Notes { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerProfile Customer { get; set; } = null!;
        public ICollection<CustomerOrderItem> CustomerOrderItems { get; set; } = new List<CustomerOrderItem>();
    }
}
