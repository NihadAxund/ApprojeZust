using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace App.Entities.Models
{
    [Index(nameof(CompositeId), IsUnique = true)]
    public class FriendRequest
    {
        public int Id { get; set; }
        [Required]
        public string CompositeId { get; set; }
        public string Content { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string SenderId { get; set; } = null!;
        public virtual CustomIdentityUser CustomIdentityUser { get; set; }
        public string ReceiverId { get; set; } = null!;
        public string ReceiverName { get; set; } = null!;
        public FriendRequest() { }
        public FriendRequest(string content,string status,string senderId,CustomIdentityUser user,string receiverId,string receiverName)
        {
            CompositeId = senderId +"|"+ receiverId;
            Content = content; Status = status; SenderId = senderId; 
            CustomIdentityUser = user; ReceiverId = receiverId; ReceiverName = receiverName;
        }
    }
}
