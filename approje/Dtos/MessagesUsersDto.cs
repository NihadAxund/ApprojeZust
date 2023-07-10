using App.Entities.Models;

namespace approje.Dtos
{
    public class MessagesUsersDto
    {
        public Chat chat { get; set; }
        public CustomIdentityUser user { get; set; }
        public CustomIdentityUser ownuser { get; set; }
        public string MeId { get; set; }

        public MessagesUsersDto(Chat chat, CustomIdentityUser meuser, CustomIdentityUser ownu, string meId)
        {
            this.chat = chat;
            user = meuser;
            ownuser = ownu;
            MeId = meId;
        }
    }
}
