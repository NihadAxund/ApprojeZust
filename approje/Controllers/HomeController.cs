using approje.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;

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
        [Route("/home/help-and-support")]
        public IActionResult help_and_support()
        {
            return View("help-and-support");
        }
        
        [Route("/home/live-chat")]
        public IActionResult live_chat()
        {
            // Gerekli işlemler
            return View("live-chat");
        }
        public IActionResult marketplace()
        {
            return View();
        }
        public IActionResult messages()
        {
            return View();
        }
        [Route("/home/my-profile")]
        public IActionResult my_profile()
        {
            return View("my-profile");
        }

        public IActionResult notifications()
        {
            return View();
        }
        public IActionResult register() => View();

        public IActionResult setting()
        {
            return View();
        }

        public IActionResult video()
        {
            return View();
        }

        public IActionResult weather()
        {
            return View();
        }

        [Route("/home/forgot-password")]
        public IActionResult forgot_password() 
        {
            return View();
        }

        public IActionResult login()
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