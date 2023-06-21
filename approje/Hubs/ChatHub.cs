using App.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace approje.Hubs
{
    public class ChatHub: Hub
    {
        private UserManager<CustomIdentityUser> _Usermanegeer { get; set; }
        private IHttpContextAccessor _HttpContextAccessor { get; set; }
        private static Dictionary<string, string> UsersAndId = new Dictionary<string, string>();

        public ChatHub(UserManager<CustomIdentityUser> usermanageer,IHttpContextAccessor httpContextAccessor) 
        {
            _Usermanegeer = usermanageer;
            _HttpContextAccessor = httpContextAccessor;

        }
        public override async Task OnConnectedAsync()
        {
            var user = await _Usermanegeer.GetUserAsync(_HttpContextAccessor.HttpContext.User);
          
            await Clients.Others.SendAsync("Connect",user.Id.ToString());

            return base.OnConnectedAsync();
        }

        public async Task Join(string User_Name)
        {
            UsersAndId.Add(User_Name, Context.ConnectionId);
        }

        public async Task SendProgramData()
        {

        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
