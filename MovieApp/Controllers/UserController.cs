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
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;

        private RoleManager<Role> _roleManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
        }

        [Authorize(Policy = "InsiderRole")]
        [HttpGet("ModTest")]
        public async Task<IActionResult> ModTest(UserForRegisterDto newUser)
        {
            return Ok("Hello mod");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("adminTest")]
        public async Task<IActionResult> AdminTest(UserForLoginDto userForLogin)
        {
            return Ok("Hello admin");
        }

        [Authorize(Roles = "Member")]
        [HttpGet("MemTest")]
        public async Task<IActionResult> MemTest(UserForLoginDto userForLogin)
        {
            return Ok("Hello mem");
        }
    }
}