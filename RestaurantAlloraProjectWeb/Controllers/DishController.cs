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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dishes = await _dishService.GetAllAsync();
            return View(dishes);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ClientMenu()
        {
            var dishes = await _dishService.GetAllAsync();
            return View(dishes);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var allergens = await _allergenService.GetAllAllergensAsync();

            var vm = new DishCreateViewModel
            {
            };

            ViewBag.Allergens = new MultiSelectList(allergens, "Id", "AllergenName");
            ViewBag.Categories = new SelectList(_dishService.GetCategories());
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DishCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var allergens = await _allergenService.GetAllAllergensAsync();

                ViewBag.Allergens = new MultiSelectList(allergens, "Id", "AllergenName");
                ViewBag.Categories = new SelectList(_dishService.GetCategories(), vm.CategoryOfTheDish);
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

            ViewBag.Allergens = new MultiSelectList(allergens, "Id", "AllergenName", vm.SelectedAllergenIds);
            ViewBag.Categories = new SelectList(_dishService.GetCategories(), vm.CategoryOfTheDish);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DishEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var allergens = await _allergenService.GetAllAllergensAsync();

                ViewBag.Allergens = new MultiSelectList(allergens, "Id", "AllergenName", vm.SelectedAllergenIds);
                ViewBag.Categories = new SelectList(_dishService.GetCategories(), vm.CategoryOfTheDish);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _dishService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
