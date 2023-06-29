using App.Entities.Models;
using approje.Dtos;
using approje.Hubs;
using approje.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace approje.Controllers
{
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;
        private CustomIdentityUser _user { get; set; }
        private UserManager<CustomIdentityUser> _userManager { get; set; }
        private RoleManager<CustomIdentityRole> _roleManager { get; set; }
        private SignInManager<CustomIdentityUser> _signInManager { get; set; }
        private CustomIdentityDbContext _context;
        IHttpContextAccessor _httpContextAccessor;
        private readonly IHubContext<ChatHub> _hubContext;
        public  UserViewModel _userViewModel { get; set; }
        public HomeController(UserManager<CustomIdentityUser> userManager,
                              RoleManager<CustomIdentityRole> roleManager,
                              SignInManager<CustomIdentityUser> signInManager,
                              CustomIdentityDbContext context,
                              IHttpContextAccessor httpContextAccessor,
                              IHubContext<ChatHub> hubContext)
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
                ViewDataLoading();
            base.OnActionExecuting(context);
        }

        [Authorize]
        public async Task<IActionResult> groups()
        {
            
            return PartialView("groups");
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
        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return RedirectToAction("Login");
        }


        [Authorize]
        private void ViewDataLoading()
        {

            if(_userViewModel == null)
            {
                var _userName =  _httpContextAccessor.HttpContext.User.Identity.Name;
                var user =  _userManager.Users.Include(u => u.FriendRequests).FirstOrDefaultAsync(u => u.UserName == _userName).Result;

                _user = user;
              
                if(user != null)
                {
                    _userViewModel = new UserViewModel(user.Id, user.UserName, user.Email,user.FriendRequests.ToList());
                    ViewData["User"] = _userViewModel;
                   // ViewBag.User = _userViewModel;
                }
                else  RedirectToAction("login");
            }
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
            lock (ChatHub.UsersAndId.Values)
            {
                var list = ChatHub.UsersAndId.Values.ToList();
                try
                {
                    if (list.Count > 0)
                        list.Remove(ChatHub.UsersAndId[_userViewModel.Id]);
                    else return Ok();
                    return Ok(list);

                }
                catch
                {
                    if (list.Count > 0) return Ok(list);
                    return Ok();
                }
            }
        }

        [Authorize]
        public async Task<IActionResult> userProfile(string id)
        {

           
            var ownuser = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            
            var list = _context.FriendRequests.Where(f => f.ReceiverId == id&& f.SenderId==_user.Id).ToList();
            ownuser.FriendRequests = list;
            var FriendRequest = ownuser.FriendRequests.FirstOrDefault(f=> f.SenderId==_user.Id);
            OwnUserDto vm = new OwnUserDto(ownuser.Id, ownuser.UserName, ownuser.Email,FriendRequest!=null);
            if (vm != null)
                return View(vm);
            
            return Ok();
        }

        [Authorize]
        public async Task<IActionResult> GetAllMeFriendRequest()
        {
            var me = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var list = me.FriendRequests.ToList();
            List<FriendRequestAndMeDto> list2 = new List<FriendRequestAndMeDto>();
            foreach (var f in list)
                list2.Add(new(f.CustomIdentityUser, f.ReceiverName));
            if(list2.Count > 0) return Ok(list2);
            return Ok();
        }



        [Authorize]
        public async Task<JsonResult> SendFollow(string id)
        {
            var OwnUser = await _context.Users.Include(f=>f.FriendRequests).FirstOrDefaultAsync(u => u.Id == id);
            var list = _context.FriendRequests.Where(f=>f.ReceiverId==id);
            OwnUser.FriendRequests = list.ToList();
            if (OwnUser != null)
            {
                OwnUser.FriendRequests.Add(new FriendRequest($"{_userViewModel.Name} Send friend request at {DateTime.Now.ToShortDateString()}",
                    "Request", _user.Id, OwnUser, id,_user.UserName));

                await _userManager.UpdateAsync(OwnUser);

                return new JsonResult(OwnUser);
            }
            else
            {
                return new JsonResult("Error");
            }
        }

      
        [Authorize]
        public async Task<JsonResult> CancelFollow(string id)
        {
            var OwnUser = await _userManager.Users.Include(a=>a.FriendRequests).FirstOrDefaultAsync(u => u.Id == id);

            if (OwnUser == null)
                return new JsonResult("Null");

            var collection = _context.FriendRequests.Where(f => f.SenderId == _user.Id&&f.ReceiverId==id);
            if (collection.Any())
            {
                foreach (var item in collection.ToList())
                {
                    _context.FriendRequests.Remove(item);
                    OwnUser.FriendRequests.Remove(item);
                }

                await _context.SaveChangesAsync();
            }

            return new JsonResult("Done");
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
                    Email = model.Email,
                   
                
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