using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectViewModels.Order;
using System.Security.Claims;

namespace RestaurantAlloraProjectWeb.Controllers
{
    
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return View(new List<OrderViewModel>());
            }

            var userId = Guid.Parse(userIdString);
            var orders = await _orderService.GetCustomerOrdersAsync(userId);

            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new OrderViewModel
            {
                OrderDate = DateTime.Now,
                Status = "Обработва се"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _orderService.CreateOrderAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var orders = await _orderService.GetCustomerOrdersAsync(Guid.Parse(userIdString));
            var order = orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}

