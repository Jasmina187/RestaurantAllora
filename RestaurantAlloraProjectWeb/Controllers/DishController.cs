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
using RestaurantAlloraProjectViewModels.Dish;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DishController : Controller
    {
        private readonly IDishService _dishService;
        private readonly IAllergenService _allergenService;
        public DishController(IDishService dishService, IAllergenService allergenService)
        {
            _dishService = dishService;
            _allergenService = allergenService;
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
            var allergens = await _allergenService.GetAllAllergensAsync();
            var vm = new DishCreateViewModel();
            vm.Allergens = new MultiSelectList(allergens, "Id", "AllergenName");

            ViewBag.Categories = _dishService.GetCategoriesSelectList();
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Create(DishCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var allergens = await _allergenService.GetAllAllergensAsync();
                vm.Allergens = new MultiSelectList(allergens, "Id", "AllergenName");

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
            var allergens = await _allergenService.GetAllAllergensAsync();
            vm.Allergens = new MultiSelectList(allergens, "Id", "AllergenName");

            ViewBag.Categories = _dishService.GetCategoriesSelectList(vm.CategoryOfTheDish);
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DishEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var allergens = await _allergenService.GetAllAllergensAsync();
                vm.Allergens = new MultiSelectList(allergens, "Id", "AllergenName");

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
