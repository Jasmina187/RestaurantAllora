using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProject.ViewModels.CustomerFavorite;
using RestaurantAlloraProjectData;
using RestaurantAlloraProjectData.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAlloraProjectWeb.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerFavoriteController : Controller
    {
        private readonly ICustomerFavoriteService _favoriteService;
        private readonly UserManager<User> _userManager;

        public CustomerFavoriteController(
            ICustomerFavoriteService favoriteService,
            UserManager<User> userManager)
        {
            _favoriteService = favoriteService;
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

            var favorites = await _favoriteService.GetFavoritesAsync(userId);
            return View(favorites);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleFavorite(Guid dishId)
        {
            var userIdString = _userManager.GetUserId(User);

            if (!Guid.TryParse(userIdString, out Guid userId))
            {
                return Unauthorized();
            }

            await _favoriteService.ToggleFavoriteAsync(dishId, userId);

            var returnUrl = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
