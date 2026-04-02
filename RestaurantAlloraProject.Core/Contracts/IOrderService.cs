using RestaurantAlloraProjectViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Contracts
{
    public interface IOrderService
    {
        Task<Guid> CreateOrderAsync(OrderViewModel model);
        Task<IEnumerable<OrderViewModel>> GetCustomerOrdersAsync(Guid customerId);
    }
}
