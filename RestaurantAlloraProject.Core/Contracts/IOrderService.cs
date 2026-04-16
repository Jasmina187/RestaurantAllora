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
        Task<OrderViewModel> PrepareCheckoutAsync(OrderViewModel model);
        Task<Guid> CreateOrderAsync(OrderViewModel model);
        Task<IEnumerable<OrderViewModel>> GetCustomerOrdersAsync(Guid customerId);
        Task<OrderListViewModel> GetCustomerOrdersPageAsync(Guid customerId, int page, int pageSize);
        Task<OrderViewModel?> GetCustomerOrderDetailsAsync(Guid orderId, Guid customerId);
        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();
        Task<OrderListViewModel> GetAllOrdersPageAsync(int page, int pageSize);
        Task<OrderViewModel?> GetOrderDetailsAsync(Guid orderId);
        Task UpdateStatusAsync(Guid orderId, string status);
        IEnumerable<string> GetOrderStatuses();
    }
}
