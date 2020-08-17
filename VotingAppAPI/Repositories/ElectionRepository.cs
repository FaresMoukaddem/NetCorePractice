using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VotingAppAPI.Data;
using VotingAppAPI.Models;
using VotingAppAPI.Dtos;

namespace VotingAppAPI.Repositories
{
    public class ElectionRepository : IElectionRepository
    {

        private readonly DataContext _context;

        private readonly IMapper _mapper;

        public ElectionRepository(DataContext context, IMapper mapper)
        {
            this._context = context;

            this._mapper = mapper;
        }

        public Task<bool> AddCandidateToElection(int electionId, int candidateId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Election> ConcludeElection(int electionId)
        {
            var election = await _context.elections.FirstOrDefaultAsync(e => e.Id == electionId);

            if(election == null)
            {
                return null;
            }

            election.IsOver = true;

            return null;
        }

        public async Task<ElectionToReturnDto> CreateElection(ElectionForCreationDto electionToCreate)
        {
            if (! await IsCandidateIdListValid(electionToCreate.Candidates))
            {
                return null;
            }

            var candidates = await _context.candidates.Where(c => electionToCreate.Candidates.Contains(c.Id)).ToListAsync();

            Election newElection = new Election(electionToCreate.Name);

            newElection.Candidates = candidates;

            await _context.elections.AddAsync(newElection);

            if(await _context.SaveChangesAsync() > 0)
            {
                return _mapper.Map<ElectionToReturnDto>(newElection);
            }

            return null;
        }

        //=============================================================================================================================
        //=============================================================================================================================
        public async Task<bool> IsCandidateIdListValid(IEnumerable<int> idList)  
        {
            var validIds = await _context.candidates.Select(x => x.Id).ToListAsync();
            return idList.All(x => validIds.Contains(x));
        }

    }
}