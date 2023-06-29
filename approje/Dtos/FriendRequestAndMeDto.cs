using App.Entities.Models;

namespace approje.Dtos
{
    public class FriendRequestAndMeDto
    {
        public  CustomIdentityUser OwnCustomIdentityUser { get; set; }
        public string MeName { get; set; }
        public FriendRequestAndMeDto(CustomIdentityUser user, string Name)
        { 
            OwnCustomIdentityUser = user;
            MeName = Name;
        }
    }
}
