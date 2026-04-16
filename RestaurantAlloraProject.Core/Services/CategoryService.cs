using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using RestaurantAlloraProjectViewModels.Category;

namespace RestaurantAlloraProject.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly RestaurantAlloraProjectContext _context;

        public CategoryService(RestaurantAlloraProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .Select(c => new CategoryViewModel
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<CategoryViewModel?> GetByIdAsync(Guid id)
        {
            return await _context.Categories
                .Where(c => c.CategoryId == id)
                .Select(c => new CategoryViewModel
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(CategoryViewModel model)
        {
            var name = model.Name.Trim();

            if (await CategoryNameExistsAsync(name))
            {
                throw new ArgumentException("Вече има категория с това име.");
            }

            _context.Categories.Add(new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = name
            });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryViewModel model)
        {
            var category = await _context.Categories.FindAsync(model.CategoryId);

            if (category == null)
            {
                throw new InvalidOperationException("Категорията не е намерена.");
            }

            var newName = model.Name.Trim();

            if (await CategoryNameExistsAsync(newName, model.CategoryId))
            {
                throw new ArgumentException("Вече има категория с това име.");
            }

            var oldName = category.Name;
            category.Name = newName;

            var dishesInCategory = await _context.Dishes
                .Where(d => d.CategoryOfTheDish == oldName)
                .ToListAsync();

            foreach (var dish in dishesInCategory)
            {
                dish.CategoryOfTheDish = newName;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return;
            }

            var isUsed = await _context.Dishes
                .AnyAsync(d => d.CategoryOfTheDish == category.Name);

            if (isUsed)
            {
                throw new InvalidOperationException("Категорията се използва от ястия и не може да бъде изтрита.");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<string>> GetCategoryNamesAsync()
        {
            return await _context.Categories
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .ToListAsync();
        }

        private async Task<bool> CategoryNameExistsAsync(string name, Guid? ignoredCategoryId = null)
        {
            return await _context.Categories
                .AnyAsync(c => c.Name == name && (!ignoredCategoryId.HasValue || c.CategoryId != ignoredCategoryId.Value));
        }
    }
}
