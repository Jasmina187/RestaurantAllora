using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectViewModels.Dashboard;

namespace RestaurantAlloraProjectWeb.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class DashboardController : Controller
    {
        private static readonly string[] ActiveOrderStatuses =
        {
            "Обработва се",
            "Приета",
            "Приготвя се"
        };

        private readonly RestaurantAlloraProjectContext _context;

        public DashboardController(RestaurantAlloraProjectContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var now = DateTime.Now;

            var ordersToday = _context.Orders
                .AsNoTracking()
                .Where(order => order.OrderDate >= today && order.OrderDate < tomorrow);

            var ordersTodayCount = await ordersToday.CountAsync();
            var revenueToday = await ordersToday
                .Where(order => order.Status != "Отказана")
                .SumAsync(order => (decimal?)order.TotalAmount) ?? 0m;

            var pendingReservations = await _context.Reservations
                .AsNoTracking()
                .CountAsync(reservation => reservation.Status == "Очаква одобрение");

            var activeOrders = await _context.Orders
                .AsNoTracking()
                .CountAsync(order => ActiveOrderStatuses.Contains(order.Status));

            var totalDishes = await _context.Dishes.AsNoTracking().CountAsync();
            var totalUsers = await _context.Users.AsNoTracking().CountAsync();

            var recentOrdersRaw = await _context.Orders
                .AsNoTracking()
                .OrderByDescending(order => order.OrderDate)
                .Take(5)
                .Select(order => new
                {
                    order.OrderId,
                    order.CustomerFullName,
                    order.OrderDate,
                    order.Status,
                    order.TotalAmount
                })
                .ToListAsync();

            var upcomingReservations = await _context.Reservations
                .AsNoTracking()
                .Include(reservation => reservation.Table)
                .Include(reservation => reservation.Customer)
                    .ThenInclude(customer => customer.User)
                .Where(reservation => reservation.ReservationDate >= now && reservation.Status != "Отказана")
                .OrderBy(reservation => reservation.ReservationDate)
                .Take(5)
                .Select(reservation => new DashboardUpcomingReservationViewModel
                {
                    CustomerName = reservation.Customer.User.UserName ?? reservation.Customer.User.Email ?? "Клиент",
                    ReservationDate = reservation.ReservationDate,
                    TableNumber = reservation.Table.TableNumber,
                    NumberOfGuests = reservation.NumberOfGuests,
                    Status = reservation.Status
                })
                .ToListAsync();

            var popularDishes = await _context.CustomerOrderItems
                .AsNoTracking()
                .GroupBy(item => new { item.DishId, item.Dish.NameOfTheDish })
                .Select(group => new DashboardPopularDishViewModel
                {
                    DishName = group.Key.NameOfTheDish,
                    Quantity = group.Sum(item => item.Quantity),
                    Revenue = group.Sum(item => item.Price * item.Quantity)
                })
                .OrderByDescending(item => item.Quantity)
                .Take(5)
                .ToListAsync();

            var orderStatuses = await _context.Orders
                .AsNoTracking()
                .GroupBy(order => order.Status)
                .Select(group => new DashboardStatusCountViewModel
                {
                    Status = group.Key,
                    Count = group.Count()
                })
                .OrderByDescending(status => status.Count)
                .ToListAsync();

            var model = new AdminDashboardViewModel
            {
                Metrics = new List<DashboardMetricViewModel>
                {
                    new()
                    {
                        Label = "Поръчки днес",
                        Value = ordersTodayCount.ToString(),
                        Hint = "Онлайн поръчки за текущия ден",
                        Accent = "gold"
                    },
                    new()
                    {
                        Label = "Оборот днес",
                        Value = $"{revenueToday:0.00} €",
                        Hint = "Без отказани поръчки",
                        Accent = "green"
                    },
                    new()
                    {
                        Label = "Чакащи резервации",
                        Value = pendingReservations.ToString(),
                        Hint = "Очакват действие от екипа",
                        Accent = "wine"
                    },
                    new()
                    {
                        Label = "Активни поръчки",
                        Value = activeOrders.ToString(),
                        Hint = "Обработват се в момента",
                        Accent = "olive"
                    },
                    new()
                    {
                        Label = "Ястия в менюто",
                        Value = totalDishes.ToString(),
                        Hint = "Публикувани предложения",
                        Accent = "gold"
                    },
                    new()
                    {
                        Label = "Потребители",
                        Value = totalUsers.ToString(),
                        Hint = "Регистрирани профили",
                        Accent = "olive"
                    }
                },
                RecentOrders = recentOrdersRaw.Select(order => new DashboardRecentOrderViewModel
                {
                    ShortId = order.OrderId.ToString()[..8],
                    CustomerName = order.CustomerFullName,
                    OrderDate = order.OrderDate,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount
                }).ToList(),
                UpcomingReservations = upcomingReservations,
                PopularDishes = popularDishes,
                OrderStatuses = orderStatuses
            };

            return View(model);
        }
    }
}
