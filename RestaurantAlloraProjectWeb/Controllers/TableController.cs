using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Table;

namespace RestaurantAlloraProjectWeb.Controllers
{
    public class TableController : Controller
    {
        private readonly RestaurantAlloraProjectContext _restaurantAlloraProjectContext;
        public TableController(RestaurantAlloraProjectContext restaurantAlloraProjectContext)
        {
            _restaurantAlloraProjectContext = restaurantAlloraProjectContext;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var tables = await _restaurantAlloraProjectContext.Tables
                .OrderBy(t => t.TableNumber)
                .Select(t => new TableViewModel
                {
                    TableId = t.TableId,
                    TableNumber = t.TableNumber,
                    CapacityOfTheTable = t.CapacityOfTheTable,
                    StatusOfTheTable = t.StatusOfTheTable
                })
                .ToListAsync();
            return View(tables);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new TableViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(TableViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var table = new Table
            {
                TableId = Guid.NewGuid(),
                TableNumber = vm.TableNumber,
                CapacityOfTheTable = vm.CapacityOfTheTable,
                StatusOfTheTable = string.IsNullOrWhiteSpace(vm.StatusOfTheTable) ? "Свободна" : vm.StatusOfTheTable
            };
            _restaurantAlloraProjectContext.Tables.Add(table);
            await _restaurantAlloraProjectContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var table = await _restaurantAlloraProjectContext.Tables.FirstOrDefaultAsync(t => t.TableId == id);
            if (table == null) return NotFound();
            var vm = new TableViewModel
            {
                TableId = table.TableId,
                TableNumber = table.TableNumber,
                CapacityOfTheTable = table.CapacityOfTheTable,
                StatusOfTheTable = table.StatusOfTheTable
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(TableViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var table = await _restaurantAlloraProjectContext.Tables.FirstOrDefaultAsync(t => t.TableId == vm.TableId);
            if (table == null) return NotFound();

            table.TableNumber = vm.TableNumber;
            table.CapacityOfTheTable = vm.CapacityOfTheTable;
            table.StatusOfTheTable = vm.StatusOfTheTable;

            await _restaurantAlloraProjectContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(Guid id)
        {
            var table = await _restaurantAlloraProjectContext.Tables.FindAsync(id);
            if (table == null) return NotFound();

            _restaurantAlloraProjectContext.Tables.Remove(table);
            await _restaurantAlloraProjectContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
