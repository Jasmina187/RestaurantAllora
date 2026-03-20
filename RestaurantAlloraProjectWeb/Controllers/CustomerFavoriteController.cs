using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.ViewModels.FavoriteDish;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectWeb.Controllers
{
    [Authorize]
    public class CustomerFavoritesController : Controller
    {
        private readonly RestaurantAlloraProjectContext _context;
        private readonly UserManager<User> _userManager;

        public CustomerFavoritesController(RestaurantAlloraProjectContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userIdString = _userManager.GetUserId(User);
            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized();
            }

            var favorites = await _context.CustomerFavorites
                .Include(cf => cf.Dish)
                .Where(cf => cf.CustomerId == userId)
                .Select(cf => new FavoriteDishViewModel
                {
                    DishId = cf.DishId,
                    DishName = cf.Dish.NameOfTheDish,
                    Price = cf.Dish.PriceOfTheDish,
                    ImageUrl = cf.Dish.ImageUrl,
                    AddedOn = cf.AddedOn
                })
                .ToListAsync();

            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFavorite(Guid dishId)
        {
            var userIdString = _userManager.GetUserId(User);
            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized();
            }

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

            var returnUrl = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index");
        }
    }
}
