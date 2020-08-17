using System.Collections.Generic;
using System.Threading.Tasks;
using VotingAppAPI.Dtos;
using VotingAppAPI.Models;

namespace VotingAppAPI.Repositories
{
    public interface IElectionRepository
    {
        Task<ElectionToReturnDto> CreateElection(ElectionForCreationDto electionToCreate);

        Task<bool> AddCandidateToElection(int electionId, int candidateId);

        Task<Election> ConcludeElection(int electionId);
    }
}