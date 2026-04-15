using Microsoft.AspNetCore.Mvc;
using RestaurantAlloraProject.Core.Contracts;

namespace RestaurantAlloraProjectWeb.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Customer")]
    public class CustomerOrderItemController : Controller
    {
        private readonly ICustomerOrderItemService _itemService;
        public CustomerOrderItemController(ICustomerOrderItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(Guid id, int quantity)
        {
            if (quantity < 1) return BadRequest();

            await _itemService.UpdateQuantityAsync(id, quantity);
            return RedirectToAction("Index", "Order"); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _itemService.RemoveItemAsync(id);
            return RedirectToAction("Index", "Order");
        }
    }
}
