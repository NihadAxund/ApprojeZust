namespace approje.Models
{
    public  class UserViewModel
    {
        public  string Name { get; set; }
        public  string Email { get; set; }
        public UserViewModel() { }
        public UserViewModel(string name, string email) 
        {
            Name = name; Email = email;        
        }
    }
}
