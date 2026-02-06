using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectWeb.Models.Dish;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectWeb.Controllers
{
    public class DishController : Controller
    {
        private readonly RestaurantAlloraProjectContext _restaurantAlloraProjectContext;
        public DishController(RestaurantAlloraProjectContext restaurantAlloraProjectContext)
        {
            _restaurantAlloraProjectContext = restaurantAlloraProjectContext;
        }
        public async Task<IActionResult> Index()
        {
            var dishes = await _restaurantAlloraProjectContext.Dishes
                .Include(d => d.DishAllergens)
                    .ThenInclude(da => da.Allergen)
                .Select(d => new DishViewModel
                {
                    DishId = d.DishId,
                    NameOfTheDish = d.NameOfTheDish,
                    DescriptionOfTheDish = d.DescriptionOfTheDish,
                    PriceOfTheDish = d.PriceOfTheDish,
                    CategoryOfTheDish = d.CategoryOfTheDish,
                    ImageUrl = d.ImageUrl,
                    AllergenNames = d.DishAllergens
                        .Select(x => x.Allergen.AllergenName)
                        .ToList()
                })
                .ToListAsync();

            return View(dishes);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var allergens = await _restaurantAlloraProjectContext.Allergens.ToListAsync();
            var vm = new DishCreateViewModel()
            {
                Allergens = new MultiSelectList(allergens, "AllergenId", "AllergenName")
            };
            LoadCategories();
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(DishCreateViewModel model)
        {
            var allergensToAdd = new List<Guid>();

            LoadCategories(model.CategoryOfTheDish);

            if (!ModelState.IsValid)
            {
                var allergens = await _restaurantAlloraProjectContext.Allergens.ToListAsync();
                model.Allergens = new MultiSelectList(allergens, "AllergenId", "AllergenName");
                return View(model);
            }

            var dish = new Dish
            {
                NameOfTheDish = model.NameOfTheDish,
                DescriptionOfTheDish = model.DescriptionOfTheDish,
                PriceOfTheDish = model.PriceOfTheDish,
                CategoryOfTheDish = model.CategoryOfTheDish,
                ImageUrl = model.ImageUrl,
                DishAllergens = new List<DishAllergen>(),
            };

            foreach (var allergen in model.SelectedAllergens)
            {
                var allergenToAdd = await _restaurantAlloraProjectContext.Allergens.
                    FirstOrDefaultAsync(x => x.AllergenId == allergen);

                dish.DishAllergens.Add(new DishAllergen
                {
                    Dish = dish,
                    Allergen = allergenToAdd
                });
            }

            _restaurantAlloraProjectContext.Dishes.Add(dish);
            await _restaurantAlloraProjectContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var dish = await _restaurantAlloraProjectContext.Dishes
         .Include(d => d.DishAllergens)
         .FirstOrDefaultAsync(d => d.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }
            var allergens = await _restaurantAlloraProjectContext.Allergens.ToListAsync();
            var model = new DishViewModel
            {
                DishId = dish.DishId,
                NameOfTheDish = dish.NameOfTheDish,
                DescriptionOfTheDish = dish.DescriptionOfTheDish,
                PriceOfTheDish = dish.PriceOfTheDish,
                CategoryOfTheDish = dish.CategoryOfTheDish,
                ImageUrl = dish.ImageUrl,
                SelectedAllergens = dish.DishAllergens.Select(x => x.AllergenId).ToList(),
                Allergens = new MultiSelectList(allergens, "AllergenId", "AllergenName",
                                               dish.DishAllergens.Select(x => x.AllergenId))
            };
            LoadCategories(dish.CategoryOfTheDish);
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, DishViewModel model)
        {
            LoadCategories(model.CategoryOfTheDish);
            if (!ModelState.IsValid)
            {
                var allergens = await _restaurantAlloraProjectContext.Allergens.ToListAsync();
                model.Allergens = new MultiSelectList(allergens, "AllergenId", "AllergenName", model.SelectedAllergens);
                return View(model);
            }
            var dish = await _restaurantAlloraProjectContext.Dishes
                .Include(d => d.DishAllergens)
                .FirstOrDefaultAsync(d => d.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }
            dish.NameOfTheDish = model.NameOfTheDish;
            dish.DescriptionOfTheDish = model.DescriptionOfTheDish;
            dish.PriceOfTheDish = model.PriceOfTheDish;
            dish.CategoryOfTheDish = model.CategoryOfTheDish;
            dish.ImageUrl = model.ImageUrl;
            dish.DishAllergens.Clear();
            foreach (var allergenId in model.SelectedAllergens)
            {
                dish.DishAllergens.Add(new DishAllergen
                {
                    DishId = dish.DishId,
                    AllergenId = allergenId
                });
            }
            await _restaurantAlloraProjectContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dish = await _restaurantAlloraProjectContext.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            _restaurantAlloraProjectContext.Dishes.Remove(dish);
            await _restaurantAlloraProjectContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private void LoadCategories(string? selected = null)
        {
            ViewBag.Categories = new SelectList(new List<string>
        {
            "Салати",
            "Основни ястия",
            "Десерти",
            "Напитки"
        }, selected);
        }
    }
}
