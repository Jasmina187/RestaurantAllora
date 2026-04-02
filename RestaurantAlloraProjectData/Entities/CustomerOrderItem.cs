using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectData.Entities
{
    public class CustomerOrderItem
    {
        [Key]
        public Guid Id { get; set; } 
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; } = null!;
        public Guid DishId { get; set; }
        public Dish Dish { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid? EmployeeId { get; set; }
        public EmployeeProfile? Employee { get; set; }
    }
}
