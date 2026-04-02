using RestaurantAlloraProjectViewModels.CustomerOrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Contracts
{
    public interface ICustomerOrderItemService
    {
        Task UpdateQuantityAsync(Guid id, int newQuantity);
        Task RemoveItemAsync(Guid id);
        Task<CustomerOrderItemViewModel?> GetByIdAsync(Guid id);
    }
}
