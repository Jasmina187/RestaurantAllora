using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectViewModels.CustomerOrderItem;
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
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            model.CustomerId = userId;

            try
            {
                await _orderService.CreateOrderAsync(model);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitOrder([FromBody] List<CustomerOrderItemViewModel> cartItems)
        {
            if (cartItems == null || !cartItems.Any())
            {
                return BadRequest("Количката е празна.");
            }
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }
            var newOrderId = Guid.NewGuid();
            foreach (var item in cartItems)
            {
                item.OrderId = newOrderId;
            }
            var order = new OrderViewModel
            {
                OrderId = newOrderId,
                OrderDate = DateTime.Now,
                Status = "Обработва се",
                CustomerId = userId,
                CustomerOrderItems = cartItems 
            };
            try
            {
                await _orderService.CreateOrderAsync(order);
                return Json(new { success = true });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

