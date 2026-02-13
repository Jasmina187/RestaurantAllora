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
        Task<DishCreateViewModel> GetCreateAsync();
        Task CreateAsync(DishCreateViewModel model);
        Task<DishViewModel?> GetEditAsync(Guid id);
        Task UpdateAsync(Guid id, DishViewModel model);
        Task DeleteAsync(Guid id);
        SelectList GetCategories(string? selected = null);     
    }
}
