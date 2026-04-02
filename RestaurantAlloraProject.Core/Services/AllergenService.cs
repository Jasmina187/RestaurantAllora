using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectViewModels.Allergen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Services
{
    public class AllergenService : IAllergenService
    {
        private readonly RestaurantAlloraProjectContext _context;
        public AllergenService(RestaurantAlloraProjectContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<SelectAllergenViewModel>> GetAllAllergensAsync()
        {
            return await _context.Allergens.Select(a => new SelectAllergenViewModel
            {
                Id = a.AllergenId,
                AllergenName = a.AllergenName,
            }).ToListAsync();
        }
    }
}
