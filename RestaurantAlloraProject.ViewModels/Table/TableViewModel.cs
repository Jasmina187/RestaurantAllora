using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectViewModels.Table
{
    public class TableViewModel
    {
        public Guid TableId { get; set; }
        public int TableNumber { get; set; }
        public int CapacityOfTheTable { get; set; }
        public string StatusOfTheTable { get; set; }

    }
}
