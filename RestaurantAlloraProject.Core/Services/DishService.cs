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
        private readonly RestaurantAlloraProjectContext _context;
        public DishService(RestaurantAlloraProjectContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<DishViewModel>> GetAllAsync()
        {
            return await _context.Dishes
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
        public async Task<DishViewModel?> GetByIdAsync(Guid id)
        {
            var dish = await _context.Dishes
                .Include(d => d.DishAllergens)
                .FirstOrDefaultAsync(d => d.DishId == id);
            if (dish == null)
            {
                return null;
            }
             var selectedIds = dish.DishAllergens.Select(x => x.AllergenId).ToList();

            return new DishViewModel
            {
                DishId = dish.DishId,
                NameOfTheDish = dish.NameOfTheDish,
                DescriptionOfTheDish = dish.DescriptionOfTheDish,
                PriceOfTheDish = dish.PriceOfTheDish,
                CategoryOfTheDish = dish.CategoryOfTheDish,
                ImageUrl = dish.ImageUrl,
                SelectedAllergens = selectedIds
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
            var selectedAllergens = model.SelectedAllergens ?? new List<Guid>();
            foreach (var allergenId in selectedAllergens.Distinct())
            {
                dish.DishAllergens.Add(new DishAllergen { DishId = dish.DishId, AllergenId = allergenId });
            }
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(DishViewModel model)
        {
            var dish = await _context.Dishes
                .Include(d => d.DishAllergens)
                .FirstOrDefaultAsync(d => d.DishId == model.DishId);

            if (dish == null)
            {
                throw new InvalidOperationException("Ястието не е намерено.");
            }
            dish.NameOfTheDish = model.NameOfTheDish;
            dish.DescriptionOfTheDish = model.DescriptionOfTheDish;
            dish.PriceOfTheDish = model.PriceOfTheDish;
            dish.CategoryOfTheDish = model.CategoryOfTheDish;
            dish.ImageUrl = model.ImageUrl;
            dish.DishAllergens.Clear();
            var selectedAllergens = model.SelectedAllergens ?? new List<Guid>();
            foreach (var allergenId in selectedAllergens.Distinct())
            {
                dish.DishAllergens.Add(new DishAllergen { DishId = dish.DishId, AllergenId = allergenId });
            }

            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(Guid id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish != null)
            {
                _context.Dishes.Remove(dish);
                await _context.SaveChangesAsync();
            }
        }
        public async Task FillCreateDropdownsAsync(DishCreateViewModel vm)
        {
            var allergens = await _context.Allergens.OrderBy(a => a.AllergenName).ToListAsync();
            vm.Allergens = new MultiSelectList(allergens, "AllergenId", "AllergenName", vm.SelectedAllergens);
        }
        public async Task FillEditDropdownsAsync(DishViewModel vm)
        {
            var allergens = await _context.Allergens.OrderBy(a => a.AllergenName).ToListAsync();
            vm.Allergens = new MultiSelectList(allergens, "AllergenId", "AllergenName", vm.SelectedAllergens);
        }
        public SelectList GetCategoriesSelectList(string? selected = null)
        {
            var categories = new List<string> { "Салати", "Основни ястия", "Десерти", "Напитки" };
            return new SelectList(categories, selected);
        }
    }
}
