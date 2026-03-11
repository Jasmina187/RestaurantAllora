using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectViewModels.Table
{
    public class TableViewModel
    {
        public Guid TableId { get; set; }
        [Required(ErrorMessage = "Номерът на масата е задължителен.")]
        [Range(1, 100, ErrorMessage = "Номерът на масата трябва да бъде между 1 и 100.")]
        public int TableNumber { get; set; }

        [Required(ErrorMessage = "Капацитетът на масата е задължителен.")]
        [Range(1, 20, ErrorMessage = "Капацитетът на масата трябва да бъде между 1 и 20 човека.")]
        public int CapacityOfTheTable { get; set; }

        [Required(ErrorMessage = "Статусът на масата е задължителен.")]
        [StringLength(20, ErrorMessage = "Статусът не може да бъде по-дълъг от 20 символа.")]
        public string StatusOfTheTable { get; set; }

    }
}
