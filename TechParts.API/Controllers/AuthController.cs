using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TechParts.API.Models;
using TechParts.API.Dtos;
using TechParts.API.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using System.Text.RegularExpressions;
using NETCore.MailKit.Core;
using MimeKit;
using Microsoft.Extensions.DependencyInjection;
using MailKit.Net.Smtp;
using MimeKit.Text;
using TechParts.API.Helpers;
using Microsoft.AspNetCore.Identity;

namespace TechParts.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private IAutherRepositroy _authRepo { get; }
        
        private IConfiguration _config { get; }

        private IMapper _mapper { get; }

        private IUserRepositroy _userRpo { get; }
        
        public AuthController(IAutherRepositroy authRepo, IConfiguration config, IMapper mapper, IUserRepositroy userRepo)
        {
            this._authRepo = authRepo;
            this._config = config; 
            this._mapper = mapper;
            this._userRpo = userRepo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserSignupDto newUser)
        {
            
            System.Console.WriteLine("HELLO");

            try
            {
                if(newUser.email == null) throw new NullReferenceException();

                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(newUser.email);
            }
            catch (FormatException)
            {
                return BadRequest("Invalid Email");
            }
            catch (NullReferenceException)
            {
                return BadRequest("Email field required!");
            }
            
            System.Console.WriteLine("A new user is registering ");
            System.Console.WriteLine("Username: " + newUser.username + ", " + "password: " + newUser.password + ", email: " + newUser.email);

            if(await _authRepo.UserExists(newUser.username))
            {
                return BadRequest("This username already exists");
            }

            User user = new User
            {
                username = newUser.username,
                email = newUser.email
            };

            var createdUser = await _authRepo.Register(user, newUser.password);

            System.Console.WriteLine("User created sucessfully! Now returning user");

            EmailSender.SendEmail(newUser.email, "OTP Code", "Hello " + newUser.username
             + "<br/>Your verification code is: " + user.OTP.Code + 
             "<br/>and your verification link:" + EmailTokenHandler.GenerateHTML(EmailTokenHandler.GenerateLink(createdUser.Id, 1)) + "<br/>This link will expire in one day.",
              newUser.username);

            return CreatedAtRoute("GetUser", new { controller = "User", id = createdUser.Id }, _mapper.Map<UserToReturnDto>(createdUser));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserCredentialsDto userSigningIn)
        {
            var user = await _authRepo.Login(userSigningIn.username.ToLower(), userSigningIn.password);

            if (user == null)
            {
                return Unauthorized();
            }

            if (!user.IsVerified)
            {
                return BadRequest("User is not verified");
            }

            // User specific info for the token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, userSigningIn.username)
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

            // Return a new object that has the token
            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyUser([FromBody] UserVerifyDto userVDto)
        {  
            System.Console.WriteLine("VERIGYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");

            System.Console.WriteLine("verifying user " + userVDto.username);

            if (await _authRepo.VerifyUser(userVDto))
            {
                return Ok();
            }
            
            return BadRequest();
        }

        [HttpGet("verifyemail")]
        public async Task<IActionResult> VerifyEmail([FromQuery] int id, string token)
        {
            System.Console.WriteLine("EMAIL VERIFICATION!");
            System.Console.WriteLine("ID: " + id);
            token = token.Replace(" " , "+");
            System.Console.WriteLine("TOKEN: " + token);

            var valid = EmailTokenHandler.IsTokenValid(Cryptor.DecryptString(token));
            
            if(valid)
            {
                if(await _authRepo.VerifyUser(id))
                {
                    return Ok("Your account has been verified.");
                }
            }

            return BadRequest("Something went wrong...");
        }

    }
}