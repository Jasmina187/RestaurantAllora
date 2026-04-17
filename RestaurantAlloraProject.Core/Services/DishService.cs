using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProject.ViewModels.Dish;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Dish;

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
                .AsNoTracking()
                .Include(d => d.Category)
                .Include(d => d.DishAllergens)
                    .ThenInclude(da => da.Allergen)
                .Select(d => new DishViewModel
                {
                    DishId = d.DishId,
                    NameOfTheDish = d.NameOfTheDish,
                    DescriptionOfTheDish = d.DescriptionOfTheDish,
                    PriceOfTheDish = d.PriceOfTheDish,
                    CategoryOfTheDish = d.Category.Name,
                    ImageUrl = d.ImageUrl,
                    AllergenNames = d.DishAllergens.Select(x => x.Allergen.AllergenName).ToList()
                })
                .ToListAsync();
        }

        public async Task<DishEditViewModel?> GetByIdAsync(Guid id)
        {
            var dish = await _context.Dishes
                .AsNoTracking()
                .Include(d => d.Category)
                .Include(d => d.DishAllergens)
                .FirstOrDefaultAsync(d => d.DishId == id);

            if (dish == null)
            {
                return null;
            }

            return new DishEditViewModel
            {
                Id = dish.DishId,
                NameOfTheDish = dish.NameOfTheDish,
                DescriptionOfTheDish = dish.DescriptionOfTheDish,
                PriceOfTheDish = dish.PriceOfTheDish,
                CategoryOfTheDish = dish.Category.Name,
                ImageUrl = dish.ImageUrl,
                SelectedAllergenIds = dish.DishAllergens.Select(x => x.AllergenId).ToList()
            };
        }

        public async Task CreateAsync(DishCreateViewModel model)
        {
            var category = await GetCategoryByNameAsync(model.CategoryOfTheDish);

            var dish = new Dish
            {
                DishId = Guid.NewGuid(),
                NameOfTheDish = model.NameOfTheDish,
                DescriptionOfTheDish = model.DescriptionOfTheDish,
                PriceOfTheDish = model.PriceOfTheDish,
                CategoryId = category.CategoryId,
                CategoryOfTheDish = category.Name,
                ImageUrl = model.ImageUrl!,
                DishAllergens = new List<DishAllergen>()
            };

            var selectedAllergens = await _context.Allergens
                .Where(a => model.SelectedAllergenIds.Contains(a.AllergenId))
                .ToListAsync();

            foreach (var allergen in selectedAllergens.Distinct())
            {
                dish.DishAllergens.Add(new DishAllergen { Dish = dish, Allergen = allergen });
            }

            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DishEditViewModel model)
        {
            var dish = await _context.Dishes
                .Include(d => d.DishAllergens)
                .FirstOrDefaultAsync(d => d.DishId == model.Id);

            if (dish == null)
            {
                throw new InvalidOperationException("Ястието не е намерено.");
            }

            var category = await GetCategoryByNameAsync(model.CategoryOfTheDish);

            dish.NameOfTheDish = model.NameOfTheDish;
            dish.DescriptionOfTheDish = model.DescriptionOfTheDish;
            dish.PriceOfTheDish = model.PriceOfTheDish;
            dish.CategoryId = category.CategoryId;
            dish.CategoryOfTheDish = category.Name;
            dish.ImageUrl = model.ImageUrl!;
            dish.DishAllergens.Clear();

            var selectedAllergens = await _context.Allergens
                .Where(a => model.SelectedAllergenIds.Contains(a.AllergenId))
                .ToListAsync();

            foreach (var allergen in selectedAllergens.Distinct())
            {
                dish.DishAllergens.Add(new DishAllergen { Dish = dish, Allergen = allergen });
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

        public IEnumerable<string> GetCategories()
        {
            return new List<string> { "Салати", "Основни ястия", "Десерти", "Напитки" };
        }

        private async Task<Category> GetCategoryByNameAsync(string categoryName)
        {
            var normalizedName = categoryName.Trim();

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == normalizedName);

            if (category == null)
            {
                throw new ArgumentException("Избраната категория не съществува.");
            }

            return category;
        }
    }
}
