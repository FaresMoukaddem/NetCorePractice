using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VotingAppAPI.Repositories;
using VotingAppAPI.Dtos;
using VotingAppAPI.Models;

namespace VotingAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IAuthRepository _authRepo;

        public AuthController(ILogger<WeatherForecastController> logger, IAuthRepository authRepo)
        {
            this._logger = logger;
            this._authRepo = authRepo;
        }

        [HttpPost("RegisterVoter")]
        public async Task<IActionResult> RegisterVoter(VoterForRegisterDto newVoter)
        {
            if(await _authRepo.UserNameExists(newVoter.username, true))
            {
                if(await _authRepo.RegisterVoter(newVoter) != null)
                {
                    return Ok("Sucess");
                }
            }
            
            return BadRequest();
        }

        [HttpPost("RegisterCandidate")]
        public async Task<IActionResult> RegisterCandidate(CandidateForRegisterDto newCandidate)
        {
            if(await _authRepo.UserNameExists(newCandidate.username, false))
            {
                if(await _authRepo.RegisterCandidate(newCandidate) != null)
                {
                    return Ok("Sucess");
                }
            }
            
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto user)
        {
            System.Console.WriteLine("yes login");
            
            var id = await _authRepo.Login(user);
            
            System.Console.WriteLine("the id returned is: " + id);

            if(id > 0)
            {
                System.Console.WriteLine("jwt should be returned now");
                return Ok(new 
                {
                    token = Helpers.TokenHandler.GenerateJWT(id, user.username, user.isVoter)
                });
            }
            
            return BadRequest("Failed to login");
        }
    }
}
