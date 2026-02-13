using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProject.ViewModels.Dish;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantAlloraProjectContext _restaurantAlloraProjectContext;
        public DishService(RestaurantAlloraProjectContext restaurantAlloraProjectContext)
        {
            _restaurantAlloraProjectContext = restaurantAlloraProjectContext;
        }
        public async Task<IEnumerable<DishViewModel>> GetAllAsync()
        {
            return await _restaurantAlloraProjectContext.Dishes
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
                    AllergenNames = d.DishAllergens.Select(x => x.Allergen.AllergenName).ToList()
                })
                .ToListAsync();
        }
        public async Task<DishCreateViewModel> GetCreateAsync()
        {
            var allergens = await _restaurantAlloraProjectContext.Allergens
                .OrderBy(a => a.AllergenName)
                .ToListAsync();
            return new DishCreateViewModel
            {
                Allergens = new MultiSelectList(allergens, "AllergenId", "AllergenName")
            };
        }
        public async Task CreateAsync(DishCreateViewModel model)
        {
            var dish = new Dish
            {
                DishId = Guid.NewGuid(),
                NameOfTheDish = model.NameOfTheDish,
                DescriptionOfTheDish = model.DescriptionOfTheDish,
                PriceOfTheDish = model.PriceOfTheDish,
                CategoryOfTheDish = model.CategoryOfTheDish,
                ImageUrl = model.ImageUrl,
                DishAllergens = new List<DishAllergen>()
            };
            foreach (var allergenId in (model.SelectedAllergens ?? new List<Guid>()).Distinct())
            {
                dish.DishAllergens.Add(new DishAllergen
                {
                    DishId = dish.DishId,
                    AllergenId = allergenId
                });
            }
            await _restaurantAlloraProjectContext.Dishes.AddAsync(dish);
            await _restaurantAlloraProjectContext.SaveChangesAsync();
        }
        public async Task<DishViewModel?> GetEditAsync(Guid id)
        {
            var dish = await _restaurantAlloraProjectContext.Dishes
                .Include(d => d.DishAllergens)
                .FirstOrDefaultAsync(d => d.DishId == id);
            if (dish == null)
            {
                return null;
            }
            var allergens = await _restaurantAlloraProjectContext.Allergens
                .OrderBy(a => a.AllergenName)
                .ToListAsync();
            var selectedIds = dish.DishAllergens.Select(x => x.AllergenId).ToList();
            return new DishViewModel
            {
                DishId = dish.DishId,
                NameOfTheDish = dish.NameOfTheDish,
                DescriptionOfTheDish = dish.DescriptionOfTheDish,
                PriceOfTheDish = dish.PriceOfTheDish,
                CategoryOfTheDish = dish.CategoryOfTheDish,
                ImageUrl = dish.ImageUrl,

                SelectedAllergens = selectedIds,
                Allergens = new MultiSelectList(allergens, "AllergenId", "AllergenName", selectedIds)
            };
        }
        public async Task UpdateAsync(Guid id, DishViewModel model)
        {
            var dish = await _restaurantAlloraProjectContext.Dishes
                .Include(d => d.DishAllergens)
                .FirstOrDefaultAsync(d => d.DishId == id);
            if (dish == null)
            {
                return;
            }
            dish.NameOfTheDish = model.NameOfTheDish;
            dish.DescriptionOfTheDish = model.DescriptionOfTheDish;
            dish.PriceOfTheDish = model.PriceOfTheDish;
            dish.CategoryOfTheDish = model.CategoryOfTheDish;
            dish.ImageUrl = model.ImageUrl;
            dish.DishAllergens.Clear();
            foreach (var allergenId in (model.SelectedAllergens ?? new List<Guid>()).Distinct())
            {
                dish.DishAllergens.Add(new DishAllergen
                {
                    DishId = dish.DishId,
                    AllergenId = allergenId
                });
            }
            await _restaurantAlloraProjectContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid id)
        {
            var dish = await _restaurantAlloraProjectContext.Dishes.FindAsync(id);
            if (dish == null)
            {
                return;
            }
            _restaurantAlloraProjectContext.Dishes.Remove(dish);
            await _restaurantAlloraProjectContext.SaveChangesAsync();
        }
        public SelectList GetCategories(string? selected = null)
        {
            var categories = new List<string>
        {
            "Салати",
            "Основни ястия",
            "Десерти",
            "Напитки"
        };

            return new SelectList(categories, selected);
        }

    }
}
