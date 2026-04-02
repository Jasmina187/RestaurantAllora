using Microsoft.AspNetCore.Mvc;
using RestaurantAlloraProject.Core.Contracts;

namespace RestaurantAlloraProjectWeb.Controllers
{
    public class CustomerOrderItemController : Controller
    {
        private readonly ICustomerOrderItemService _itemService;
        public CustomerOrderItemController(ICustomerOrderItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(Guid id, int quantity)
        {
            if (quantity < 1) return BadRequest();

            await _itemService.UpdateQuantityAsync(id, quantity);
            return RedirectToAction("Review", "Order"); 
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _itemService.RemoveItemAsync(id);
            return RedirectToAction("Review", "Order");
        }
    }
}
