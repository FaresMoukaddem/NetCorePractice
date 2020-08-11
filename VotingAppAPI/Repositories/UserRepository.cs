

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
    }
}
