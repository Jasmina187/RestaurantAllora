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
        public IActionResult Add(Guid dishId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var model = new ReviewViewModel
            {
                DishId = dishId,
                CustomerId = string.IsNullOrEmpty(userIdString) ? Guid.Empty : Guid.Parse(userIdString)
            };

            return View(model);
        }      [HttpPost]
        public async Task<IActionResult> Add(ReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); 
            }
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.CustomerId = Guid.Parse(userIdString);

            await _reviewService.AddReviewAsync(model);

            TempData["Success"] = "Благодарим ви за оценката!";
            return RedirectToAction("Details", "Dish", new { id = model.DishId });
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