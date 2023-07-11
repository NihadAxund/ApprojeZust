namespace approje.Dtos
{
    public class ContactFriendDto
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Imageurl { get; set; }

        public ContactFriendDto(string? id, string? userName, string? imageurl)
        {
            Id = id;
            UserName = userName;
            Imageurl = imageurl;
        }
    }
}
