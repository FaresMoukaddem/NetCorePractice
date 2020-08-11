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

        public ElectionController(IElectionRepository elecRepo)
        {
            this._elecRepo = elecRepo;
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
        public async Task<IActionResult> GetElection([FromRoute ]int id)
        {
            var election = await _elecRepo.GetElection(id);

            if(election != null)
            {
                return Ok(election);
            }

            return BadRequest();
        }

    }
}