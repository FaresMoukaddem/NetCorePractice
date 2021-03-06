

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace VotingAppAPI.Helpers
{
    public class TokenHandler
    {

        private static readonly IConfiguration _config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json", optional: false)
            .Build();

        public static string GenerateJWT(int id, string username, bool isVoter)
        {
            // User specific info for the token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Name, isVoter ? "1" : "0")
            };

            // Key to sign the token (we get the key from the config dependency)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            // We hash the key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // We specify basic token info along with SigningCredentials
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            // We create a token handler thats going to create the token for us
            var tokenHandler = new JwtSecurityTokenHandler();

            // We create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}