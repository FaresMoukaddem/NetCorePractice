using System.Linq;
using AutoMapper;
using TechParts.API.Dtos;
using TechParts.API.Models;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<User, UserToReturnDto>();

        CreateMap<Part, PartSimpleDto>();
    }
}