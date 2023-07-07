using App.Entities.Models;

namespace approje.Models
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<FriendRequest> FriendsRequest { get; set; } = new();
        public int FriendCount { get; set; }
        public string UserImagelink { get; set; } = "https://w7.pngwing.com/pngs/744/940/png-transparent-anonym-avatar-default-head-person-unknown-user-user-pictures-icon.png";
        public UserViewModel() { }

        //public UserViewModel(string id,string name, string email) 
        //{
        //    Id = id;
        //    Name = name; Email = email;        
        //}



        public UserViewModel(string id, string name, string email,List<FriendRequest> friends, int friendCount,string url)
        {
            Id = id; Name = name;
            Email = email; FriendsRequest = friends;
            FriendCount = friendCount; UserImagelink = url;
        }
    }
}
