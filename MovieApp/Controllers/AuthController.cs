using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApp.Dtos;
using MovieApp.Helpers;
using MovieApp.Models;


namespace MovieApp.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        private RoleManager<Role> _roleManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto newUser)
        {
            System.Console.WriteLine("Registering new user");
            System.Console.WriteLine("Username: " + newUser.UserName);
            System.Console.WriteLine("Password: " + newUser.Password);
            
            if(!newUser.isValid())
            {
                return BadRequest("Missing fields");
            }

            var res = await _userManager.CreateAsync(new User(newUser.UserName),newUser.Password);

            string resp = string.Empty;

            if(res.Succeeded)
            {
                resp += "Successfully created user" + "\n";
                

                var user = await _userManager.FindByNameAsync(newUser.UserName);
                var rres = await _userManager.AddToRoleAsync(user, "Member");

                if(rres.Succeeded)
                {
                    resp += "Successfully added 'Member' role to new user" + "\n";
                    return Ok(resp);
                }
                else
                {
                    resp += "Could not add 'Member' role to new user" + "\n";
                    return BadRequest(resp);
                }
            }
            else
            {
                return BadRequest(res.GetErrorString());
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            if(!userForLogin.isValid())
            {
                return BadRequest("Missing fields");
            }

            var user = await _userManager.FindByNameAsync(userForLogin.UserName);

            if(user == null)
            {
                return Unauthorized("Invalid credentials");
            }
            
            var res = await _signInManager.CheckPasswordSignInAsync(user, userForLogin.Password, false);

            if(res.Succeeded)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user); 
                return Ok(new 
                {
                    token = JWTTokenHandler.GenerateJWT(user.Id, user.UserName, roles)
                });
            }
            else
            {
                return Unauthorized(res.ToString());
            }
        }
    }
}
