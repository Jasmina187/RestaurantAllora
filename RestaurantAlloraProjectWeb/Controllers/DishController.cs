using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProject.Core.Services;
using RestaurantAlloraProject.ViewModels.Dish;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectWeb.Controllers
{

    [Authorize(Roles = "Admin")]
    public class DishController : Controller
    {
        private readonly IDishService _dishService;
        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dishes = await _dishService.GetAllAsync();
            return View(dishes);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new DishCreateViewModel();
            await _dishService.FillCreateDropdownsAsync(vm);
            ViewBag.Categories = _dishService.GetCategoriesSelectList();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(DishCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await _dishService.FillCreateDropdownsAsync(vm);
                ViewBag.Categories = _dishService.GetCategoriesSelectList(vm.CategoryOfTheDish);
                return View(vm);
            }
            await _dishService.CreateAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var vm = await _dishService.GetByIdAsync(id);
            if (vm == null)
            {
                return NotFound();
            }

            await _dishService.FillEditDropdownsAsync(vm);
            ViewBag.Categories = _dishService.GetCategoriesSelectList(vm.CategoryOfTheDish);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DishViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                await _dishService.FillEditDropdownsAsync(vm);
                ViewBag.Categories = _dishService.GetCategoriesSelectList(vm.CategoryOfTheDish);
                return View(vm);
            }
            try
            {
                await _dishService.UpdateAsync(vm);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _dishService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
