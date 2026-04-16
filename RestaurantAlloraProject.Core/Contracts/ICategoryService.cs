using RestaurantAlloraProjectViewModels.Category;

namespace RestaurantAlloraProject.Core.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task<CategoryViewModel?> GetByIdAsync(Guid id);
        Task CreateAsync(CategoryViewModel model);
        Task UpdateAsync(CategoryViewModel model);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<string>> GetCategoryNamesAsync();
    }
}
