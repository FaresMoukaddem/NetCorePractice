using VotingAppAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VotingAppAPI.Repositories
{
    public interface IUserRepository
    {
        Task<VoterToReturn> GetVoter(int id);

        Task<CandidateToReturn> GetCandidate(int id);

        Task<IEnumerable<VoterToReturn>> GetAllVoters();

        Task<IEnumerable<CandidateToReturn>> GetAllCandidates();
    }
}