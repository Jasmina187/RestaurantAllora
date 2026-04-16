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
using RestaurantAlloraProjectWeb.Contracts;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DishController : Controller
    {
        private readonly IDishService _dishService;
        private readonly IAllergenService _allergenService;
        private readonly IImageService _imageService;
        private readonly ICategoryService _categoryService;

        public DishController(
            IDishService dishService,
            IAllergenService allergenService,
            IImageService imageService,
            ICategoryService categoryService)
        {
            _dishService = dishService;
            _allergenService = allergenService;
            _imageService = imageService;
            _categoryService = categoryService;
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
            ViewBag.Categories = new SelectList(await _categoryService.GetCategoryNamesAsync());
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DishCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await TryUploadDishImageAsync(
                    vm.Picture,
                    vm.NameOfTheDish,
                    imageUrl => vm.ImageUrl = imageUrl,
                    nameof(DishCreateViewModel.Picture));
            }

            if (string.IsNullOrWhiteSpace(vm.ImageUrl) && (vm.Picture == null || vm.Picture.Length == 0))
            {
                ModelState.AddModelError(nameof(DishCreateViewModel.ImageUrl), "Качи снимка или въведи URL.");
            }

            if (!ModelState.IsValid)
            {
                var allergens = await _allergenService.GetAllAllergensAsync();

                ViewBag.Allergens = new MultiSelectList(allergens, "Id", "AllergenName");
                ViewBag.Categories = new SelectList(await _categoryService.GetCategoryNamesAsync(), vm.CategoryOfTheDish);
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
            ViewBag.Categories = new SelectList(await _categoryService.GetCategoryNamesAsync(), vm.CategoryOfTheDish);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DishEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await TryUploadDishImageAsync(
                    vm.Picture,
                    vm.NameOfTheDish,
                    imageUrl => vm.ImageUrl = imageUrl,
                    nameof(DishEditViewModel.Picture));
            }

            if (string.IsNullOrWhiteSpace(vm.ImageUrl) && (vm.Picture == null || vm.Picture.Length == 0))
            {
                ModelState.AddModelError(nameof(DishEditViewModel.ImageUrl), "Качи снимка или въведи URL.");
            }

            if (!ModelState.IsValid)
            {
                var allergens = await _allergenService.GetAllAllergensAsync();

                ViewBag.Allergens = new MultiSelectList(allergens, "Id", "AllergenName", vm.SelectedAllergenIds);
                ViewBag.Categories = new SelectList(await _categoryService.GetCategoryNamesAsync(), vm.CategoryOfTheDish);
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

        private async Task TryUploadDishImageAsync(
            IFormFile? picture,
            string dishName,
            Action<string> setImageUrl,
            string modelStateKey)
        {
            if (picture == null || picture.Length == 0)
            {
                return;
            }

            try
            {
                var imageName = $"{dishName}-{Guid.NewGuid():N}";
                var imageUrl = await _imageService.UploadImageAsync(picture, imageName);

                if (string.IsNullOrWhiteSpace(imageUrl))
                {
                    ModelState.AddModelError(modelStateKey, "Снимката не беше качена успешно.");
                    return;
                }

                setImageUrl(imageUrl);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(modelStateKey, ex.Message);
            }
        }
    }
}
