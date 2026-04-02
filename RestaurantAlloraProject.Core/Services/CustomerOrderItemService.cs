using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectViewModels.CustomerOrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Services
{
    public class CustomerOrderItemService : ICustomerOrderItemService
    {
        private readonly RestaurantAlloraProjectContext _context;
        public CustomerOrderItemService(RestaurantAlloraProjectContext context)
        {
            _context = context;
        }
        public async Task UpdateQuantityAsync(Guid id, int newQuantity)
        {
            var item = await _context.CustomerOrderItems.FindAsync(id);
            if (item != null)
            {
                item.Quantity = newQuantity;
                await _context.SaveChangesAsync();
            }
        }
        public async Task RemoveItemAsync(Guid id)
        {
            var item = await _context.CustomerOrderItems.FindAsync(id);
            if (item != null)
            {
                _context.CustomerOrderItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<CustomerOrderItemViewModel?> GetByIdAsync(Guid id)
        {
            return await _context.CustomerOrderItems
                .Where(oi => oi.Id == id)
                .Select(oi => new CustomerOrderItemViewModel
                {
                    Id = oi.Id,
                    DishId = oi.DishId,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    OrderId = oi.OrderId
                })
                .FirstOrDefaultAsync();
        }
    }
}

