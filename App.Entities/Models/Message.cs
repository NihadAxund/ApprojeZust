using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Entities.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public string ReceiverId { get; set; }
        public string SenderId { get; set; }
        public virtual Chat Chat { get; set; }
        public bool HasSeen { get; set; } = false;
        public Message() { }
        public Message(string content, DateTime dateTime, string receiverId, string senderId, Chat chat, bool hasSeen)
        {
            Content = content;
            DateTime = dateTime;
            ReceiverId = receiverId;
            SenderId = senderId;
            Chat = chat;
            HasSeen = hasSeen;
        }
    }
}
