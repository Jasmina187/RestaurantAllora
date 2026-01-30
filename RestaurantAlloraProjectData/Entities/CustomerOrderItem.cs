using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class CustomerOrderItem
    {
        [Key]
        public Guid CustomerOrderId { get; set; }

        public Guid CustomerId { get; set; }
        public CustomerProfile Customer { get; set; } = null!;

        public Guid EmployeeId { get; set; }
        public EmployeeProfile Employee { get; set; } = null!;

        public Guid DishId { get; set; }
        public Dish Dish { get; set; } = null!;

        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Обработва се";

        public bool IsPickup { get; set; }
    }
}
