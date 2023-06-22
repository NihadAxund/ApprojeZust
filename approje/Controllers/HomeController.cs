using App.Entities.Models;
using approje.Hubs;
using approje.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace approje.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;
        private UserManager<CustomIdentityUser> _userManager;
        private RoleManager<CustomIdentityRole> _roleManager;
        private SignInManager<CustomIdentityUser> _signInManager;
        private CustomIdentityDbContext _context;
        IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<ChatHub> _hubContext;
         public  UserViewModel _userViewModel { get; set; }
        public HomeController(UserManager<CustomIdentityUser> userManager,
            RoleManager<CustomIdentityRole> roleManager,
            SignInManager<CustomIdentityUser> signInManager,
            CustomIdentityDbContext context, IHttpContextAccessor httpContextAccessor, IHubContext<ChatHub> hubContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _hubContext = hubContext;

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.Name!=null)
                ViewDataLoadaig();
            base.OnActionExecuting(context);
        }

        [Authorize]
        public IActionResult groups()
        {
            return View();
        }
        [Authorize]
        public IActionResult friends()
        {
            return View();
        }
        [Authorize]
        public IActionResult favorite()
        {
            return View();
        }
        [Authorize]
        public IActionResult birthday()
        {
            return View();
        }
        [Authorize]
        public IActionResult events()
        {
            return View();
        }

        [Authorize]

        private void ViewDataLoadaig()
        {

            if(_userViewModel == null)
            {
                var _userName = _httpContextAccessor.HttpContext.User.Identity.Name;
                var user = _context.Users.FirstOrDefault(f => f.UserName == _userName);
                _userViewModel = new UserViewModel(user.Id, user.UserName, user.Email);
            }
            ViewData["User"] = _userViewModel;


            
        }


        [Authorize]
        public IActionResult Index()
        {
            
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        [Route("/home/help-and-support")]
        public IActionResult help_and_support()
        {
            return View("help-and-support");
        }
        [Authorize]
        [Route("/home/live-chat")]
        public IActionResult live_chat()
        {
            // Gerekli işlemler
            return View("live-chat");
        }
        [Authorize]
        public IActionResult marketplace()
        {
            return View();
        }
        [Authorize]
        public IActionResult messages()
        {
            return View();
        }
        [Authorize]
        [Route("/home/my-profile")]
        public IActionResult my_profile()
        {
            return View("my-profile");
        }
        [Authorize]
        public IActionResult notifications()
        {
            
            return View();
        }
        [Authorize]

        public IActionResult settings()
        {
            return View();
        }
        [Authorize]
        public IActionResult video()
        {
            return View();
        }
        [Authorize]
        public IActionResult weather()
        {
            return View();
        }
        [Authorize]
        [Route("/home/forgot-password")]
        public IActionResult forgot_password()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> GetAllOnlineUsers()
        {
            var list = ChatHub.UsersAndId.Values.ToList();
            list.Remove(ChatHub.UsersAndId[_userViewModel.Id]);
                return Ok(list);
        }

        [Authorize]
        public async Task<IActionResult> userProfile(string id)
        {
            var user = _context.Users.FirstOrDefault(f => f.Id==id);
            UserViewModel vm = new UserViewModel(user.Id,user.UserName,user.Email);
            if (vm != null)
                return View(vm);
            
            return Ok();
        }





        //Accound
        public IActionResult register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                CustomIdentityUser user = new CustomIdentityUser
                {
                    UserName = model.Username,
                    Email = model.Email
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    if (!_roleManager.RoleExistsAsync("User").Result)
                    {
                        CustomIdentityRole role = new CustomIdentityRole
                        {
                            Name = "User"
                        };

                        IdentityResult roleResult = await _roleManager.CreateAsync(role);
                        if (!roleResult.Succeeded)
                        {
                            ModelState.AddModelError("", "We can not add the role");
                            return View(model);
                        }
                    }

                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("login", "Home");
                }
            }
            return View(model);
        }


        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Home");
                }
                ModelState.AddModelError("", "Invalid Login");
            }
            return View(model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}