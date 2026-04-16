using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Table;

namespace RestaurantAlloraProjectWeb.Controllers
{
    public class TableController : Controller
    {
        private readonly ITableService _tableService;
        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tables = await _tableService.GetAllAsync();
            return View(tables);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet]
        public async Task<IActionResult> ChooseTable(int? capacity)
        {
            var tables = await _tableService.GetAllAsync();
            ViewBag.SelectedCapacity = capacity;
            return View(tables);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new TableViewModel());
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TableViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                await _tableService.CreateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(vm.TableNumber), ex.Message);
                return View(vm);
            }
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var vm = await _tableService.GetByIdAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TableViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            try
            {
                await _tableService.UpdateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(nameof(vm.TableNumber), ex.Message);
                return View(vm);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin,Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _tableService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
