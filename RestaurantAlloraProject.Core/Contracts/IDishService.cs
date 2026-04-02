using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantAlloraProject.ViewModels.Dish;
using RestaurantAlloraProjectViewModels.Dish;
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
        Task<DishEditViewModel?> GetByIdAsync(Guid id);
        Task CreateAsync(DishCreateViewModel model);
        Task UpdateAsync(DishEditViewModel model);
        Task DeleteAsync(Guid id);
        SelectList GetCategoriesSelectList(string? selected = null);
    }
}
