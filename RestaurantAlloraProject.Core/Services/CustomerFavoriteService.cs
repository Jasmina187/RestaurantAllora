using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProject.ViewModels.CustomerFavorite;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantAlloraProject.Core.Services
{
    public class CustomerFavoriteService : ICustomerFavoriteService
    {
        private readonly RestaurantAlloraProjectContext _context;
        public CustomerFavoriteService(RestaurantAlloraProjectContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CustomerFavoriteViewModel>> GetFavoritesAsync(Guid userId)
        {
            return await _context.CustomerFavorites
                .Include(cf => cf.Dish)
                .Where(cf => cf.CustomerId == userId)
                .Select(cf => new CustomerFavoriteViewModel
                {
                    DishId = cf.DishId,
                    DishName = cf.Dish.NameOfTheDish,
                    Price = cf.Dish.PriceOfTheDish,
                    ImageUrl = cf.Dish.ImageUrl,
                    AddedOn = cf.AddedOn
                })
                .ToListAsync();
        }
        public async Task ToggleFavoriteAsync(Guid dishId, Guid userId)
        {
            var existingFavorite = await _context.CustomerFavorites
                .FirstOrDefaultAsync(cf => cf.CustomerId == userId && cf.DishId == dishId);
            if (existingFavorite != null)
            {
                _context.CustomerFavorites.Remove(existingFavorite);
            }
            else
            {
                var profileExists = await _context.Set<CustomerProfile>().AnyAsync(cp => cp.UserId == userId);
                if (!profileExists)
                {
                    _context.Add(new CustomerProfile { UserId = userId });
                }
                var newFavorite = new CustomerFavorite
                {
                    CustomerId = userId,
                    DishId = dishId,
                    AddedOn = DateTime.UtcNow
                };
                _context.CustomerFavorites.Add(newFavorite);
            }
            await _context.SaveChangesAsync();
        }
    }
}
