using App.Entities.Models;

namespace approje.Dtos
{
    public class MessagesUsersDto
    {
        public Chat chat { get; set; }
        public CustomIdentityUser user { get; set; }

        public MessagesUsersDto(Chat chat, CustomIdentityUser ownuser)
        {
            this.chat = chat;
            user = ownuser;
            
        }
    }
}
