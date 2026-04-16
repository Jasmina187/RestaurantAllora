using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Reservation;

namespace RestaurantAlloraProjectWeb.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private const int ReservationPageSize = 10;

        private readonly IReservationService _reservationService;
        private readonly UserManager<User> _userManager;

        public ReservationController(IReservationService reservationService, UserManager<User> userManager)
        {
            _reservationService = reservationService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var data = await _reservationService.GetUserReservationsPageAsync(userId, page, ReservationPageSize);

            return View(data);
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(Guid? tableId)
        {
            var vm = new ReservationCreateViewModel
            {
                TableId = tableId ?? Guid.Empty,
                ReservationDate = DateTime.Now.AddHours(1)
            };

            await _reservationService.FillTablesAsync(vm);

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservationCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await _reservationService.FillTablesAsync(vm);
                return View(vm);
            }

            try
            {
                var userId = Guid.Parse(_userManager.GetUserId(User)!);
                await _reservationService.CreateReservationAsync(vm, userId);

                TempData["ReservationSuccess"] = "Резервацията е изпратена и очаква одобрение.";
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await _reservationService.FillTablesAsync(vm);
                return View(vm);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Pending([FromQuery] ReservationFilterViewModel filter)
        {
            filter.Page = Math.Max(1, filter.Page);
            filter.PageSize = ReservationPageSize;

            var model = await _reservationService.GetReservationsForManagementAsync(filter);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(Guid id)
        {
            try
            {
                var employeeId = Guid.Parse(_userManager.GetUserId(User)!);
                await _reservationService.ApproveReservationAsync(id, employeeId);
                TempData["ReservationSuccess"] = "Резервацията е одобрена.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ReservationError"] = ex.Message;
            }

            return RedirectToAction(nameof(Pending));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(Guid id)
        {
            try
            {
                var employeeId = Guid.Parse(_userManager.GetUserId(User)!);
                await _reservationService.RejectReservationAsync(id, employeeId);
                TempData["ReservationSuccess"] = "Резервацията е отказана.";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ReservationError"] = ex.Message;
            }

            return RedirectToAction(nameof(Pending));
        }
    }
}
