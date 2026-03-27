using RestaurantAlloraProjectViewModels.Allergen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Contracts
{
    public interface IAllergenService
    {
        Task<IEnumerable<SelectAllergenViewModel>> GetAllAllergensAsync();
    }
}
