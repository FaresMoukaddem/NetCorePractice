using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VotingAppAPI.Models;
using VotingAppAPI.Repositories;

namespace VotingAppAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ElectionController : ControllerBase
    {
        private readonly IElectionRepository _elecRepo;

        private readonly IUserRepository _userRepo;

        public ElectionController(IElectionRepository elecRepo, IUserRepository userRepo)
        {
            this._elecRepo = elecRepo;
            this._userRepo = userRepo;
        }

        [HttpPost("CreateElection")]
        public async Task<IActionResult> CreateElection(ElectionForCreationDto electionToCreate)
        {
            System.Console.WriteLine("-----------------------------------------");
            System.Console.WriteLine("Creating new election");

            System.Console.WriteLine(electionToCreate.Name);

            foreach(int id in electionToCreate.Candidates)
                System.Console.WriteLine(id);

            System.Console.WriteLine("-----------------------------------------");

            var newElection = await _elecRepo.CreateElection(electionToCreate);

            if(newElection != null)
            {
                return Ok(newElection);
            }

            return BadRequest();
        }

        [HttpGet("GetElection/{id}")]
        public async Task<IActionResult> GetElection([FromRoute] int id)
        {
            var election = await _userRepo.GetElection(id);

            if(election != null)
            {
                return Ok(election);
            }

            return BadRequest();
        }

        [HttpGet("ElectionActive/{id}")]
        public async Task<IActionResult> GetElectionActive([FromRoute] int id)
        {
            return Ok(await _userRepo.IsElectionActive(id));
        }
    }
}