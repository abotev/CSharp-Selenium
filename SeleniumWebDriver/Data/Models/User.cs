namespace SeleniumWebDriver.Data.Models
{
    public class User
    {
        public User(string email, string password, string name, string nickName)
        {
            this.Email = email;
            this.Password = password;
            this.Name = name;
            this.NickName = nickName;
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }
    }
}
