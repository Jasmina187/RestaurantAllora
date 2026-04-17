using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectViewModels.Review;
using System.Security.Claims;

namespace RestaurantAlloraProjectWeb.Controllers
{
    public class ReviewController : Controller
    {
        private const int ReviewPageSize = 10;

        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Add(Guid dishId, string? returnUrl = null)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            var model = new ReviewViewModel
            {
                DishId = dishId,
                CustomerId = userId
            };

            ViewData["ReturnUrl"] = GetSafeReturnUrl(returnUrl);

            return View("Create", model);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ReviewViewModel model, string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = GetSafeReturnUrl(returnUrl);
                return View("Create", model); 
            }
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                return Unauthorized();
            }

            model.CustomerId = userId;

            await _reviewService.AddReviewAsync(model);

            TempData["Success"] = "Благодарим ви за оценката!";
            var safeReturnUrl = GetSafeReturnUrl(returnUrl);
            return LocalRedirect(safeReturnUrl);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Index(int page = 1)
        {
            var reviews = await _reviewService.GetAllReviewsPageAsync(page, ReviewPageSize);
            return View(reviews);
        }

        private string GetSafeReturnUrl(string? returnUrl)
        {
            return !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)
                ? returnUrl
                : Url.Action("ClientMenu", "Dish")!;
        }
    }
}
