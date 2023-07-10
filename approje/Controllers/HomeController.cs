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
using System.Web;

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
        public async Task<IActionResult> friends()
        {
            List<CustomIdentityUser> ramruser = new List<CustomIdentityUser>();
            var Task1 = Task.Run(async () =>
            {
                foreach (var item in _user.Friends)
                    ramruser.Add(await _userManager.Users.Include(f=>f.Friends).FirstOrDefaultAsync(a => a.Id == item.YourFriendId));
            });
            Task.WaitAll(Task1);
            return View(ramruser);
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
                var user =  _userManager.Users.Include(u => u.FriendRequests).Include(u=>u.Friends).FirstOrDefaultAsync(u => u.UserName == _userName).Result;
                _user = user;
            
                if(user != null)
                {
                    _userViewModel = new UserViewModel(user.Id, user.UserName, user.Email,user.FriendRequests.ToList(),user.Friends.Count(),user.ImageUrl);
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
        public IActionResult errorview() => View();

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
        public async Task<IActionResult> MyChat(string id)
        {
            var ownuser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            var chat = await _context.Chats.Include(s=>s.Messages).FirstOrDefaultAsync(c => c.ReceiverId == id&&c.SenderId==_userViewModel.Id
            || c.ReceiverId==_userViewModel.Id&&c.SenderId==id);
            if (chat == null)
            {
                chat = new Chat(ownuser.Id,_userViewModel.Id,ownuser);
               await _context.Chats.AddAsync(chat);
               await _context.Chats.AddAsync(new Chat(_userViewModel.Id,ownuser.Id,_user));
                await _context.SaveChangesAsync();

            }
            else
            {
                var messages = chat.Messages.OrderBy(o => o.DateTime).ToList();
                chat.Messages = messages;
            }

            MessagesUsersDto dto = new(chat, _user,ownuser,_userViewModel.Id);

            return Ok(dto);
           
        }

        [HttpPost(Name = "AddMessage")]
        public async Task<IActionResult> AddMessage(MessageModel model)
        {
            try
            {
                var chat = await _context.Chats.FirstOrDefaultAsync(i => i.ReceiverId == model.ReceiverId&&i.SenderId==model.SenderId);
                var chat2 = await _context.Chats.FirstOrDefaultAsync(i => i.ReceiverId == model.SenderId && i.SenderId == model.ReceiverId);
                if (chat != null&&chat2!=null)
                {
                    var message = new Message
                    {
                        Chat = chat,
                        Content = model.Content,
                        DateTime = DateTime.Now,
                        HasSeen = false,
                        ReceiverId = model.ReceiverId,
                        SenderId = model.SenderId
                    };
                    var message2 = new Message
                    {
                        Chat = chat2,
                        Content = model.Content,
                        DateTime = DateTime.Now,
                        HasSeen = false,
                        ReceiverId = model.ReceiverId,
                        SenderId = model.SenderId
                    };
                    await _context.Messages.AddAsync(message);
                    await _context.Messages.AddAsync(message2);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [Route("/home/live-chat")]
        public IActionResult live_chat()
        {
            lock (ChatHub.UsersAndId.Values)
            {
                var list = ChatHub.UsersAndId.Values.Select(s => s[0]).Where(s=>s.Id!=_userViewModel.Id).ToList();

                List<ChatUserDto> chatUserDtos = new List<ChatUserDto>();
                foreach (var item in list)
                    chatUserDtos.Add(new(item));
                if(chatUserDtos.Count > 0)
                    ViewData["live_chat_message_user"] = chatUserDtos[0];
                ViewData["me"] = new ChatUserDto(_user);
                return View("live-chat", chatUserDtos);
                //return View(chatUserDtos);
            }
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
                var list = ChatHub.UsersAndId.Values.Select(s => s[0]).ToList();
                
                try
                {
                    if (list.Count > 0)
                        list.Remove(ChatHub.UsersAndId[_userViewModel.Id][0]);
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
            var ownuser = await _userManager.Users.Include(u => u.FriendRequests).Include(s=>s.Friends).FirstOrDefaultAsync(u => u.Id == id);
            var friendslist = _context.Friends.Where(a => a.MyId == id);
            ownuser.Friends = friendslist.ToList();
            var list = _context.FriendRequests.Where(f => f.ReceiverId == id&& f.SenderId==_user.Id).ToList();
            ownuser.FriendRequests = list;
            var FriendRequest = ownuser.FriendRequests.FirstOrDefault(f=> f.SenderId==_user.Id);
            if (friendslist.Count() > 0)
            {
                var data = friendslist.FirstOrDefault(f => f.MyId == id && f.YourFriendId == _user.Id);
            }
            var IsFriend = ownuser.Friends.FirstOrDefault(f=>f.MyId==id&&f.YourFriendId==_user.Id);
            OwnUserDto vm = new OwnUserDto(ownuser.Id, ownuser.UserName, ownuser.Email,FriendRequest!=null, IsFriend != null,ownuser.Friends.Count(),ownuser.ImageUrl);
            if (vm != null)
                return View(vm);
            
            return Ok();
        }

        [Authorize]
        public async Task<IActionResult> GetAllMeFriendRequest()
        {
            var me = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var list = _user.FriendRequests.ToList();
            List<FriendRequestAndMeDto> list2 = new List<FriendRequestAndMeDto>();
            foreach (var f in list)
                list2.Add(new(f.CustomIdentityUser, f.ReceiverName,f.SenderId));
            if(list2.Count > 0) return Ok(list2);
            return Ok();
        }

		[Authorize]
        public async Task<IActionResult> AddFriends(string id)
        {
			var OwnUser = await _context.Users.Include(f => f.FriendRequests).Include(f =>f.Friends).FirstOrDefaultAsync(u => u.Id == id);
            //_context.Friends.Add(new Friend(OwnUser.Id, _user.Id));
            //_context.Friends.Add(new Friend(_user.Id, OwnUser.Id));
            OwnUser.Friends.Add(new Friend(OwnUser.Id, _user.Id));
            _user.Friends.Add(new Friend(_user.Id, OwnUser.Id));
            await _userManager.UpdateAsync(OwnUser);
            await _userManager.UpdateAsync(_user);

            var removelist = _context.FriendRequests.Where(f => f.ReceiverId == id && f.SenderId == _user.Id || f.ReceiverId == _user.Id && f.SenderId == id);
            foreach (var f in removelist)
                _context.FriendRequests.Remove(f);
            await _context.SaveChangesAsync();
            var data = _context.Friends.ToList();

			return Ok();
        }

		[Authorize]
		public async Task<JsonResult> DeleteFriend(string id)
		{
            var val = _context.Friends.Where(f=>f.YourFriendId==id&&f.MyId==_user.Id|| f.MyId == id && f.YourFriendId == _user.Id);
            foreach (var f in val)
                _context.Friends.Remove(f);
            await _context.SaveChangesAsync();
			return new("Done");
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
        public async Task<JsonResult> NewProfileImage(string id)
        {
            if (id != null)
            {
                string decodedLink = HttpUtility.UrlDecode(id);
                _user.ImageUrl = decodedLink;
                await _userManager.UpdateAsync(_user);
                return new("Done");

            }
            return new("Null");
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

        public async Task<JsonResult> deleteRequest(string id)
        {
            var OwnUser = await _userManager.Users.Include(a => a.FriendRequests).FirstOrDefaultAsync(u => u.Id == id);

            if (OwnUser == null)
                return new JsonResult("Null");

            var collection = _context.FriendRequests.Where(f => f.SenderId == id && f.ReceiverId == _user.Id);
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