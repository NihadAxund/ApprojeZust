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
        public string? ImageUrl { get; set; }
        public bool IsFriend { get; set; } = false;
        public bool HasRequestPending { get; set; } = false;
        public DateTime DisConnectTime { get; set; } = DateTime.Now;
        public string ConnectTime { get; set; } = "";
        public CustomIdentityUser()
        {
            Friends = new List<Friend>();
            FriendRequests = new List<FriendRequest>();

        }


    }
}
