using VotingAppAPI.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotingAppAPI.Models;

namespace VotingAppAPI.Repositories
{
    public interface IUserRepository
    {
        Task<VoterToReturn> GetVoter(int id);

        Task<CandidateToReturn> GetCandidate(int id);

        Task<IEnumerable<VoterToReturn>> GetAllVoters();

        Task<IEnumerable<CandidateToReturn>> GetAllCandidates();

        Task<bool> IsElectionActive(int electionId);

        Task<ElectionToReturnDto> GetElection(int electionId);

        Task<IEnumerable<Vote>> GetElectionVotes(int electionId);

        Task<bool> AddVote(VoteToSendDto newVote);
    }
}