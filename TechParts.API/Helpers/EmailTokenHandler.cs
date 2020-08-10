using System;
using System.Linq;
using System.Security.Cryptography;

namespace TechParts.API.Helpers
{
    public class EmailTokenHandler
    {
        public static string GenerateEncryptedToken(int days)
        {
            if(days < 1) days = 1;

            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.AddDays(days).ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());

            System.Console.WriteLine("TOKEN CREATED: " + token);

            return Cryptor.EncryptString(token);
        }

        public static string GenerateLink(int id, int days)
        {   
            string link = "http://localhost:5000/auth/verifyemail?id=" + id + "&token=" + GenerateEncryptedToken(days);

            System.Console.WriteLine("LINK GENERATED: " + link);

            return link;
        }

        public static string GenerateHTML(string linkString)
        {
            return "<br/><br/><div style=\"<!--text-align:center;--> margin-top:50px\"><button style=\"background-color:green; width:200px; height:50px; border: none;\"> <a href=\""+linkString+"\" style=\"text-decoration: none; color:white; font-size:20px;\"><b>Verify Account</b></a> </button></div><br/>";
        }

        public static bool IsTokenValid(string decryptedToken)
        {
            System.Console.WriteLine("VALIDATING DECRYPTED TOKEN: " + decryptedToken);
            
            byte[] data = Convert.FromBase64String(decryptedToken);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));

            System.Console.WriteLine("Date from token: " + when);

            bool valid = DateTime.UtcNow < when;

            System.Console.WriteLine("TOKEN IS VALID: " + valid);

            return valid;
        }
    }
}