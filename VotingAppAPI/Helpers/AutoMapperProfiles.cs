using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VotingAppAPI.Dtos;
using VotingAppAPI.Models;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Voter, VoterToReturn>();

        CreateMap<Candidate, CandidateToReturn>();

        CreateMap<IEnumerable<Candidate>, IList<CandidateToReturn>>();

        CreateMap<IEnumerable<Voter>, IList<VoterToReturn>>();

        CreateMap<Election, ElectionToReturnDto>();
    }
}