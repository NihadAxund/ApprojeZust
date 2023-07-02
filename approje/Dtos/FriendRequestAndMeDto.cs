using App.Entities.Models;

namespace approje.Dtos
{
    public class FriendRequestAndMeDto
    {
        public  CustomIdentityUser OwnCustomIdentityUser { get; set; }
        public string MeName { get; set; }
        public string Ownid { get; set; }
        public FriendRequestAndMeDto(CustomIdentityUser user, string Name,string ownid)
        { 
            OwnCustomIdentityUser = user;
            MeName = Name; Ownid = ownid;
        }
    }
}
