using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantAlloraProject.Core.Contracts;
using RestaurantAlloraProjectViewModels.Review;
using System.Security.Claims;

namespace RestaurantAlloraProjectWeb.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Add(Guid dishId)
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

            return View("Create", model);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
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
            return RedirectToAction("ClientMenu", "Dish");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")] 
        public IActionResult Index()
        {
            
            var emptyList = new List<ReviewViewModel>();
            return View(emptyList);
        }
    }
}
