namespace MovieApp.Dtos
{
    public class UserForLoginDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool isValid()
        {
            return UserName != null && Password != null;
        }
    }
}