namespace approje.Dtos
{
    public class OwnUserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsFriendRequest { get; set; }
        public bool IsFriend { get; set; }
        public int FriendCount { get; set; }
        public string Imageurl { get; set; }
        public OwnUserDto() { }
        public OwnUserDto(string id, string name, string email, bool ısFriedrequest,bool isfriend,int friendcount,string imageulr)
        {
            Imageurl = imageulr;
            Id = id; Name = name; 
            Email = email; IsFriendRequest = ısFriedrequest;
            IsFriend = isfriend;
            FriendCount = friendcount;
        }
    }
}
