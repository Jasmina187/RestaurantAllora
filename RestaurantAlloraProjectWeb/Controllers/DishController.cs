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
    public class DishController : Controller
    {
        private readonly IDishService dishService;
        public DishController(IDishService _dishService)
        {
            dishService = _dishService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var dishes = await dishService.GetAllAsync();
            return View(dishes);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = dishService.GetCategories();
            var vm = await dishService.GetCreateAsync();
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(DishCreateViewModel model)
        {
            ViewBag.Categories = dishService.GetCategories(model.CategoryOfTheDish);
            if (!ModelState.IsValid)
            {
                var fixedVm = await dishService.GetCreateAsync();
                model.Allergens = fixedVm.Allergens;
                return View(model);
            }

            await dishService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await dishService.GetEditAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.Categories = dishService.GetCategories(model.CategoryOfTheDish);
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, DishViewModel model)
        {
            ViewBag.Categories = dishService.GetCategories(model.CategoryOfTheDish);

            if (!ModelState.IsValid)
            {
                var fixedVm = await dishService.GetEditAsync(id);
                if (fixedVm != null)
                {
                    model.Allergens = fixedVm.Allergens;
                }
                return View(model);
            }
            await dishService.UpdateAsync(id, model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await dishService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
