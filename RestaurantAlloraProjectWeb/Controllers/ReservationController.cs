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
            {
                ModelState.AddModelError(nameof(vm.NumberOfGuests), "Моля, въведи валиден брой гости.");
            }          
            if (vm.ReservationDate < DateTime.Now)
            {
                ModelState.AddModelError(nameof(vm.ReservationDate), "Не може да резервирате за минало време.");
            }
            var reservationStart = vm.ReservationDate;
            var reservationEnd = vm.ReservationDate.AddHours(3);
            if (reservationStart.Hour < 8)
            {
                ModelState.AddModelError(nameof(vm.ReservationDate), "Ресторантът отваря в 08:00 ч.");
            }
            else if (reservationStart.Hour > 21 || (reservationStart.Hour == 21 && reservationStart.Minute > 0))
            {
                ModelState.AddModelError(nameof(vm.ReservationDate), "Резервацията е за 3 часа и надхвърля часа на затваряне (00:00 ч.). Най-късният час за резервация е 21:00 ч.");
            }
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var potentialTables = await _restaurantAlloraProjectContext.Tables
                .Where(t => t.CapacityOfTheTable >= vm.NumberOfGuests)
                .ToListAsync();
            Table? availableTable = null;
            foreach (var table in potentialTables)
            {
                var isOccupied = await _restaurantAlloraProjectContext.Reservations
                    .AnyAsync(r => r.TableId == table.TableId &&
                                   r.Status != "Отказана" &&
                                   reservationStart < r.ReservationDate.AddHours(3) &&
                                   reservationEnd > r.ReservationDate);

                if (!isOccupied)
                {
                    availableTable = table;
                    break; 
                }
            }
            if (availableTable == null)
            {
                ModelState.AddModelError(string.Empty, "За съжаление няма свободна маса за избраното време и брой гости.");
                return View(vm);
            }
            var userId = Guid.Parse(_userManager.GetUserId(User)!);
            var customerProfileExists = await _restaurantAlloraProjectContext.Set<CustomerProfile>()
                .AnyAsync(cp => cp.UserId == userId);

            if (!customerProfileExists)
            {
                
                var newCustomerProfile = new CustomerProfile
                {
                    UserId = userId
                };
                _restaurantAlloraProjectContext.Add(newCustomerProfile);
                await _restaurantAlloraProjectContext.SaveChangesAsync(); 
            }
            var reservation = new Reservation
            {
                ReservationId = Guid.NewGuid(),
                TableId = availableTable.TableId,
                CustomerId = userId, 
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



