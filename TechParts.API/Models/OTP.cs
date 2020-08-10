using System.Security.Cryptography;

namespace TechParts.API.Models
{
    public class OTP
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public int Code { get; set; }

        public OTP()
        {
            string otpString = string.Empty;

            for(int i = 0; i < 5; i++)
            {
                otpString += RandomNumberGenerator.GetInt32(1,10).ToString();
            }

            this.Code = int.Parse(otpString);
        }
    }
}