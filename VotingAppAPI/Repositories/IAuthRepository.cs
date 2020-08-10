using System.Threading.Tasks;
using System.Collections.Generic;
using VotingAppAPI.Dtos;
using VotingAppAPI.Models;

namespace VotingAppAPI.Repositories
{
    public interface IAuthRepository
    {
        Task<VoterToReturn> RegisterVoter(VoterForRegisterDto voter);

        Task<CandidateToReturn> RegisterCandidate(CandidateForRegisterDto candidate);

        Task<int> Login(UserForLoginDto user);

        Task<bool> UserNameExists(string username, bool voter);
    }
}