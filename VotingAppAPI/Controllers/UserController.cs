using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VotingAppAPI.Repositories;
using VotingAppAPI.Dtos;
using VotingAppAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace VotingAppAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserRepository _userRepo;

        public UserController(ILogger<WeatherForecastController> logger, IUserRepository userRepo)
        {
            this._logger = logger;
            this._userRepo = userRepo;
        }

        [HttpGet("voter/{id}")]
        public async Task<IActionResult> GetVoter([FromRoute] int id)
        {
            return Ok(await _userRepo.GetVoter(id));
        }

        [HttpGet("candidate/{id}")]
        public async Task<IActionResult> GetCandidate([FromRoute] int id)
        {
            return Ok(await _userRepo.GetCandidate(id));
        }

        [HttpGet("getAllVoters")]
        public async Task<IActionResult> GetAllVoters()
        {
            return Ok(await _userRepo.GetAllVoters());
        }
        
        [HttpGet("getAllCandidates")]
        public async Task<IActionResult> GetAllCandidates()
        {
            return Ok(await _userRepo.GetAllCandidates());
        }

        [HttpGet("getVotes/{id}")]
        public async Task<IActionResult> GetVotes([FromRoute] int id)
        {
            var votes = await _userRepo.GetElectionVotes(id);

            System.Console.WriteLine("we out of this repo");

            if(votes != null)
            {
                return Ok(votes);
            }

            System.Console.WriteLine("shakla null");
            return BadRequest("Failed getting votes");
        }

        [HttpPost("addVote")]
        public async Task<IActionResult> AddVote([FromBody] VoteToSendDto newVote)
        {
            System.Console.WriteLine("candidate id: " + newVote.CandidateId);
            System.Console.WriteLine("election id: " + newVote.ElectionId);
            System.Console.WriteLine("voter id: " + newVote.VoterId);

            //Vote voteToAdd = new Vote(newVote.ElectionId, newVote.VoterId, newVote.CandidateId);

           // System.Console.WriteLine(voteToAdd);

            //System.Console.WriteLine("candidate id: " + voteToAdd.CandidateId);
           // System.Console.WriteLine("election id: " + voteToAdd.ElectionId);
            //System.Console.WriteLine("voter id: " + voteToAdd.VoterId);

            await _userRepo.AddVote(newVote);
           // {
             //   return Ok("Added Vote!");
           // }

            return Ok("Failed to add votes");
        }
    }
}
