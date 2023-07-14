using App.Core.Abstraction;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Entities.Models
{
    public class CustomIdentityUser:IdentityUser,IEntity
    {
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<FriendRequest> FriendRequests { get; set; }
        public string? ImageUrl { get; set; } = "https://w7.pngwing.com/pngs/744/940/png-transparent-anonym-avatar-default-head-person-unknown-user-user-pictures-icon.png";
        public bool IsFriend { get; set; } = false;
        public bool HasRequestPending { get; set; } = false;
        public DateTime DisConnectTime { get; set; } = DateTime.Now;
        public List<Chat> Chats { get; set; }
        public string ConnectTime { get; set; } = "";
        public List<Post> Posts { get; set; }
        public CustomIdentityUser()
        {
            Friends = new List<Friend>();
            FriendRequests = new List<FriendRequest>();
            Chats = new();
            Posts = new();
        }


    }
}
