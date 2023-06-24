using App.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Security.Claims;

namespace approje.Hubs
{
    public class ChatHub: Hub
    {
        private UserManager<CustomIdentityUser> _Usermanegeer { get; set; }
        private IHttpContextAccessor _HttpContextAccessor { get; set; }
        public static Dictionary<string, CustomIdentityUser> UsersAndId = new Dictionary<string, CustomIdentityUser>();
        private static Dictionary<string, CustomIdentityUser> Disconnect_User = new Dictionary<string, CustomIdentityUser>();

        public ChatHub(UserManager<CustomIdentityUser> usermanageer,IHttpContextAccessor httpContextAccessor) 
        {
            _Usermanegeer = usermanageer;
            _HttpContextAccessor = httpContextAccessor;

        }
        public override async Task OnConnectedAsync()
        {
            var user = await _Usermanegeer.GetUserAsync(_HttpContextAccessor.HttpContext.User);
            if (UsersAndId==null||!(UsersAndId.ContainsKey(user.Id)))
            {
                UsersAndId.Add(user.Id, user);
                await Clients.Others.SendAsync("Connect", user.UserName, user.Id);
                
            }
            else if (Disconnect_User.ContainsKey(user.Id))
            {
                Disconnect_User.Remove(user.Id);
            }


            // return base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await _Usermanegeer.GetUserAsync(_HttpContextAccessor.HttpContext.User);
            if (user != null)
            {
                    Disconnect_User.Add(user.Id, user);
                await Task.Delay(TimeSpan.FromSeconds(5));
                UsersAndId.Remove(user.Id);
                    if(Disconnect_User.ContainsKey(user.Id))
                        await Clients.Others.SendAsync("Disconnect", user.Id);
            }
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
