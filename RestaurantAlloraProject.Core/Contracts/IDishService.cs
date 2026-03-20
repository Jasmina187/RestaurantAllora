using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantAlloraProject.ViewModels.Dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Contracts
{
    public interface IDishService
    {
        Task<IEnumerable<DishViewModel>> GetAllAsync();
        Task<DishViewModel?> GetByIdAsync(Guid id);
        Task CreateAsync(DishCreateViewModel model);
        Task UpdateAsync(DishViewModel model);
        Task DeleteAsync(Guid id);
        Task FillCreateDropdownsAsync(DishCreateViewModel vm);
        Task FillEditDropdownsAsync(DishViewModel vm);
        SelectList GetCategoriesSelectList(string? selected = null);
    }
}
