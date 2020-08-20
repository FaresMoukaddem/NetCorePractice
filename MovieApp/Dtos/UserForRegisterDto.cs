namespace MovieApp.Dtos
{
    public class UserForRegisterDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool isValid()
        {
            return UserName != null && Password != null;
        }
    }
}