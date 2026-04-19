using Microsoft.AspNetCore.Mvc;
using RestaurantAlloraProjectWeb.Models;
using System.Diagnostics;

namespace RestaurantAlloraProjectWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult StatusCodePage(int code)
        {
            Response.StatusCode = code;
            ViewData["StatusCode"] = code;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
