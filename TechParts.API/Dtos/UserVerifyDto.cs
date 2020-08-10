namespace TechParts.API.Dtos
{
    public class UserVerifyDto
    {
        public string username { get; set; }

        public string password { get; set; }

        public int code { get; set; }
    }
}