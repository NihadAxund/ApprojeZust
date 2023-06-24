namespace approje.Dtos
{
    public class OwnUserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsFriend { get; set; }
        public OwnUserDto() { }
        public OwnUserDto(string id, string name, string email, bool ısFried)
        {
            Id = id;
            Name = name; Email = email;
            IsFriend = ısFried;
        }
    }
}
