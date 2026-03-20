using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Reservation;

namespace RestaurantAlloraProjectWeb.Controllers
{
    [Authorize(Roles = "Admin,Customer")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly UserManager<User> _userManager;
        public ReservationController(IReservationService reservationService, UserManager<User> userManager)
        {
            _reservationService = reservationService;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var data = await _reservationService.GetUserReservationsAsync(userId);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new ReservationCreateViewModel
            {
                ReservationDate = DateTime.Now.AddHours(1)
            };
            await _reservationService.FillTablesAsync(vm);
            return View(vm);
        }

        [HttpPost]
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
        public async Task<IActionResult> Pending()
        {
            var data = await _reservationService.GetPendingReservationsAsync();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> Approve(Guid id)
        {
            var employeeId = Guid.Parse(_userManager.GetUserId(User)!);
            await _reservationService.ApproveReservationAsync(id, employeeId);
            return RedirectToAction(nameof(Pending));
        }

        [HttpPost]
        public async Task<IActionResult> Reject(Guid id)
        {
            var employeeId = Guid.Parse(_userManager.GetUserId(User)!);
            await _reservationService.RejectReservationAsync(id, employeeId);
            return RedirectToAction(nameof(Pending));
        }
    }

}



