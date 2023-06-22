namespace approje.Models
{
    public  class UserViewModel
    {
        public string Id { get; set; }
        public  string Name { get; set; }
        public  string Email { get; set; }
        public UserViewModel() { }
        public UserViewModel(string id,string name, string email) 
        {
            Id = id;
            Name = name; Email = email;        
        }
    }
}
