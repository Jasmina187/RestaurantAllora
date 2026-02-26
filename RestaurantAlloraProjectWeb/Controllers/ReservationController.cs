using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Reservation;

namespace RestaurantAlloraProjectWeb.Controllers
{
    public class ReservationController : Controller
    {
        private readonly RestaurantAlloraProjectContext _restaurantAlloraProjectContext;
        private readonly UserManager<User> _userManager;
        public ReservationController(RestaurantAlloraProjectContext restaurantAlloraProjectContext,UserManager<User> userManager)
        {
            _restaurantAlloraProjectContext = restaurantAlloraProjectContext;
            _userManager = userManager;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Customer")]
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse(_userManager.GetUserId(User)!);

            var data = await _restaurantAlloraProjectContext.Reservations
                .Include(r => r.Table)
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Employee).ThenInclude(e => e.User)
                .Where(r => r.CustomerId == userId)
                .OrderByDescending(r => r.ReservationDate)
                .Select(r => new ReservationIndexViewModel
                {
                    ReservationId = r.ReservationId,
                    TableNumber = r.Table.TableNumber,
                    CapacityOfTheTable = r.Table.CapacityOfTheTable,
                    Status = r.Status,
                    ReservationDate = r.ReservationDate,
                    NumberOfGuests = r.NumberOfGuests,
                    StatusOfTheTable = r.Table.StatusOfTheTable,
                    CustomerName = r.Customer.User.UserName,
                    EmployeeName = r.Employee != null ? r.Employee.User.UserName : null
                })
                .ToListAsync();

            return View(data);
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new ReservationCreateViewModel
            {
                ReservationDate = DateTime.Now.AddHours(1)
            };

            await FillTables(vm);
            return View(vm);
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpPost]

        public async Task<IActionResult> Create(ReservationCreateViewModel vm)
        {
            if (vm.NumberOfGuests <= 0)
                ModelState.AddModelError(nameof(vm.NumberOfGuests), "Моля, въведи валиден брой гости.");

            var table = await _restaurantAlloraProjectContext.Tables.FirstOrDefaultAsync(t => t.TableId == vm.TableId);
            if (table == null)
                ModelState.AddModelError(nameof(vm.TableId), "Моля, избери маса.");

            if (table != null && vm.NumberOfGuests > table.CapacityOfTheTable)
                ModelState.AddModelError(nameof(vm.NumberOfGuests), "Броят гости надвишава капацитета на масата.");


            if (table != null)
            {
                var taken = await _restaurantAlloraProjectContext.Reservations.AnyAsync(r =>
                    r.TableId == vm.TableId &&
                    r.ReservationDate == vm.ReservationDate &&
                    r.Status != "Отказана");

                if (taken)
                    ModelState.AddModelError("", "Има вече резервация за тази маса на избрания час.");
            }

            if (!ModelState.IsValid)
            {
                await FillTables(vm);
                return View(vm);
            }

            var userId = Guid.Parse(_userManager.GetUserId(User)!);

            var reservation = new Reservation
            {
                ReservationId = Guid.NewGuid(),
                TableId = vm.TableId,
                CustomerId = userId,
                EmployeeId = null,
                ReservationDate = vm.ReservationDate,
                NumberOfGuests = vm.NumberOfGuests,
                Status = "Очаква одобрение"
            };

            _restaurantAlloraProjectContext.Reservations.Add(reservation);
            await _restaurantAlloraProjectContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpGet]
        public async Task<IActionResult> Pending()
        {
            var data = await _restaurantAlloraProjectContext.Reservations
                .AsNoTracking()
                .Include(r => r.Table)
                .Include(r => r.Customer).ThenInclude(c => c.User)
                .Include(r => r.Employee).ThenInclude(e => e.User)
                .Where(r => r.Status == "Очаква одобрение")
                .OrderBy(r => r.ReservationDate)
                .Select(r => new ReservationIndexViewModel
                {
                    ReservationId = r.ReservationId,
                    TableNumber = r.Table.TableNumber,
                    CapacityOfTheTable = r.Table.CapacityOfTheTable,
                    Status = r.Status,
                    ReservationDate = r.ReservationDate,
                    NumberOfGuests = r.NumberOfGuests,
                    StatusOfTheTable = r.Table.StatusOfTheTable,
                    CustomerName = r.Customer.User.UserName,
                    EmployeeName = r.Employee != null ? r.Employee.User.UserName : null
                })
                .ToListAsync();

            return View(data);
        }

        [Authorize(Roles = "Admin,Customer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(Guid id)
        {
            var reservation = await _restaurantAlloraProjectContext.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
            if (reservation == null) return NotFound();

            reservation.Status = "Одобрена";
            reservation.EmployeeId = Guid.Parse(_userManager.GetUserId(User)!);

            await _restaurantAlloraProjectContext.SaveChangesAsync();
            return RedirectToAction(nameof(Pending));
        }
        [Authorize(Roles = "Admin,Customer")]
        [HttpPost]

        public async Task<IActionResult> Reject(Guid id)
        {
            var reservation = await _restaurantAlloraProjectContext.Reservations.FirstOrDefaultAsync(r => r.ReservationId == id);
            if (reservation == null) return NotFound();

            reservation.Status = "Отказана";
            reservation.EmployeeId = Guid.Parse(_userManager.GetUserId(User)!);

            await _restaurantAlloraProjectContext.SaveChangesAsync();
            return RedirectToAction(nameof(Pending));
        }

        private async Task FillTables(ReservationCreateViewModel vm)
        {
            var tables = await _restaurantAlloraProjectContext.Tables
                .AsNoTracking()
                .OrderBy(t => t.TableNumber)
                .ToListAsync();

            vm.Tables = tables.Select(t => new SelectListItem
            {
                Value = t.TableId.ToString(),
                Text = $"Маса {t.TableNumber} ({t.CapacityOfTheTable} места)"
            }).ToList();
        }

    }


}



