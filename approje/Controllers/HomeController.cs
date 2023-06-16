using approje.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace approje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult groups()
        {
            return View();
        }
        public IActionResult friends()
        {
            return View();
        }
        public IActionResult favorite()
        {
            return View();
        }
        public IActionResult birthday()
        {
            return View();
        }

        public IActionResult events()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}