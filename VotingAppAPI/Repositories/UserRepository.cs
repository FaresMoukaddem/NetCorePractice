using  VotingAppAPI.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VotingAppAPI.Data;
using VotingAppAPI.Dtos;
using AutoMapper;
using System.Collections.Generic;

namespace VotingAppAPI.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;

        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            this._context = context;

            this._mapper = mapper;
        }

        public async Task<VoterToReturn> GetVoter(int id)
        {
            return _mapper.Map<VoterToReturn>(await _context.voters.FirstOrDefaultAsync(v => v.Id == id));
        }

        public async Task<CandidateToReturn> GetCandidate(int id)
        {
            return _mapper.Map<CandidateToReturn>(await _context.candidates.FirstOrDefaultAsync(c => c.Id == id));
        }

        public async Task<IEnumerable<VoterToReturn>> GetAllVoters()
        {
            return _mapper.Map<IEnumerable<VoterToReturn>>(await _context.voters.ToListAsync());
        }

        public async Task<IEnumerable<CandidateToReturn>> GetAllCandidates()
        {
            return _mapper.Map<IEnumerable<CandidateToReturn>>(await _context.candidates.ToListAsync());
        }

                public async Task<ElectionToReturnDto> GetElection(int electionId)
        {
            return _mapper.Map<ElectionToReturnDto>(await _context.elections.FirstOrDefaultAsync(e => e.Id == electionId));
        }

        public async Task<bool> IsElectionActive(int electionId)
        {
            var election = await _context.elections.AsNoTracking().FirstOrDefaultAsync(e => e.Id == electionId);

            if(election != null)
            {
                return election.IsOver;
            }

            return false;
        }

        public async Task<IEnumerable<Vote>> GetElectionVotes(int electionId)
        {
            var election = await _context.elections.FirstOrDefaultAsync(e => e.Id == electionId);

            if(election != null)
            {
                System.Console.WriteLine("ELECtion not NuLL");
                return election.Votes;
            }

            System.Console.WriteLine("eLEcTiOn nULL!");

            return election.Votes;
        }

        public async Task<bool> AddVote(VoteToSendDto newVote)
        {
            System.Console.WriteLine("in repo func with electionid: " + newVote.ElectionId);
            var election = await _context.elections.FirstOrDefaultAsync(e => e.Id == newVote.ElectionId);
            System.Console.WriteLine("got election");
           // if(election != null)
           // {
            //    System.Console.WriteLine("election aint null");
           //     return false;
           // }

            System.Console.WriteLine("election is null!");

            //Vote voteToAdd = new Vote(newVote.ElectionId, newVote.VoterId, newVote.CandidateId);

            //_context.votes.Add(voteToAdd);

            return true;
        }
    }
}
