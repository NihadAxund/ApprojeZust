using App.Entities.Models;

namespace approje.Dtos
{
    public class ChatUserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string User_img { get; set; }
        public ChatUserDto(CustomIdentityUser user)
        {
            Id = user.Id;
            Name = user.UserName;
            User_img = user.ImageUrl;

        }
    }
}
