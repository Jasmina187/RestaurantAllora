using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectViewModels.CustomerOrderItem;
using RestaurantAlloraProjectViewModels.Order;
using System.Security.Claims;

namespace RestaurantAlloraProjectWeb.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private const int OrderPageSize = 10;

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                return RedirectToAction(nameof(Manage));
            }

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdString))
            {
                return View(new OrderListViewModel());
            }
            var userId = Guid.Parse(userIdString);
            var orders = await _orderService.GetCustomerOrdersPageAsync(userId, page, OrderPageSize);

            return View(orders);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Manage(int page = 1)
        {
            var orders = await _orderService.GetAllOrdersPageAsync(page, OrderPageSize);

            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new OrderViewModel
            {
                OrderDate = DateTime.Now,
                Status = "Обработва се",
                FulfillmentType = "Вземане на място"
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderViewModel model)
        {
            try
            {
                var checkoutModel = await _orderService.PrepareCheckoutAsync(model);
                ModelState.Clear();
                return View("Create", checkoutModel);
            }
            catch (ArgumentException ex)
            {
                TempData["OrderError"] = ex.Message;
                return RedirectToAction("ClientMenu", "Dish");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderViewModel model)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            model.CustomerId = userId;
            OrderViewModel checkoutModel;

            try
            {
                checkoutModel = await _orderService.PrepareCheckoutAsync(model);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

            checkoutModel.CustomerId = userId;
            checkoutModel.FulfillmentType = model.FulfillmentType;
            checkoutModel.CustomerFullName = model.CustomerFullName;
            checkoutModel.CustomerPhone = model.CustomerPhone;
            checkoutModel.DeliveryAddress = model.DeliveryAddress;
            checkoutModel.Notes = model.Notes;

            if (!ModelState.IsValid)
            {
                return View(checkoutModel);
            }

            try
            {
                var orderId = await _orderService.CreateOrderAsync(checkoutModel);
                TempData["OrderSuccess"] = "Поръчката е приета.";
                return RedirectToAction(nameof(Details), new { id = orderId });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(checkoutModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            OrderViewModel? order;

            if (User.IsInRole("Admin") || User.IsInRole("Employee"))
            {
                order = await _orderService.GetOrderDetailsAsync(id);
            }
            else
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (string.IsNullOrEmpty(userIdString))
                {
                    return RedirectToAction("LogIn", "User");
                }

                order = await _orderService.GetCustomerOrderDetailsAsync(id, Guid.Parse(userIdString));
            }

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.OrderStatuses = new SelectList(_orderService.GetOrderStatuses(), order.Status);

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> UpdateStatus(Guid id, string status)
        {
            try
            {
                await _orderService.UpdateStatusAsync(id, status);
                TempData["OrderStatusSuccess"] = "Статусът на поръчката е обновен.";
            }
            catch (ArgumentException ex)
            {
                TempData["OrderStatusError"] = ex.Message;
            }

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitOrder([FromBody] List<CustomerOrderItemViewModel> cartItems)
        {
            return BadRequest("Моля финализирайте поръчката през checkout страницата.");
        }
    }
}
