using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string ReceiverId { get; set; }
        public virtual CustomIdentityUser Receiver { get; set; }
        public string SenderId { get; set; }
        public virtual List<Message> Messages { get; set; }
        public Chat()
        {
            Messages = new List<Message>();
        }
        public Chat(string receiverId, string senderId,CustomIdentityUser receiver)
        {
            ReceiverId = receiverId; SenderId = senderId;
            Messages = new List<Message>(); Receiver = receiver;
        }

    }
}
