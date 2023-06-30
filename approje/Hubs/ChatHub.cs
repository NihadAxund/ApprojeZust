using App.Entities.Models;
using approje.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;

namespace approje.Hubs
{
    public class ChatHub: Hub
    {
        private UserManager<CustomIdentityUser> _Usermanegeer { get; set; }
        private IHttpContextAccessor _HttpContextAccessor { get; set; }
        public static Dictionary<string, List<CustomIdentityUser>> UsersAndId = new Dictionary<string, List<CustomIdentityUser>>();
        private static Dictionary<string, CustomIdentityUser> Disconnect_User = new Dictionary<string, CustomIdentityUser>();

        public ChatHub(UserManager<CustomIdentityUser> usermanageer, IHttpContextAccessor httpContextAccessor)
        {
            _Usermanegeer = usermanageer;
            _HttpContextAccessor = httpContextAccessor;

        }
        public override async Task OnConnectedAsync()
        {
            var user = await _Usermanegeer.GetUserAsync(_HttpContextAccessor.HttpContext.User);
            if (UsersAndId.Count==0||!(UsersAndId.ContainsKey(user.Id)))
            {
                UsersAndId.Add(user.Id, new());
                UsersAndId[user.Id].Add(user);
                await Clients.Others.SendAsync("Connect", user.UserName, user.Id);
            }
            else if (Disconnect_User.ContainsKey(user.Id))
            {
                Disconnect_User.Remove(user.Id);
  
            }
            else
            {
                UsersAndId[user.Id].Add(user);

			}


            // return base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await _Usermanegeer.GetUserAsync(_HttpContextAccessor.HttpContext.User);
            if (user != null)
            {
                Disconnect_User.Add(user.Id, user);
                await Task.Delay(TimeSpan.FromSeconds(3));
                if (Disconnect_User.ContainsKey(user.Id))
                {
                    if (UsersAndId[user.Id].Count > 0)
                    {
                        UsersAndId[user.Id].RemoveAt(0);
                        Disconnect_User.Remove(user.Id);
                    }
                    if (UsersAndId[user.Id].Count <= 0) { 
                           UsersAndId.Remove(user.Id);
                           await Clients.Others.SendAsync("Disconnect", user.Id);               
                    }
                    
                    
                }
            }
        }

        public async Task SendNotification(string Ownid, NotificationEnum notificationEnum = NotificationEnum.FriendRequest)
        {
            await Clients.User(Ownid).SendAsync("Notification",Ownid, notificationEnum);

        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
