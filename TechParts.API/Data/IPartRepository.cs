using System.Threading.Tasks;
using TechParts.API.Models;
using System.Collections.Generic;
using TechParts.API.Helpers;

namespace TechParts.API.Data
{
    public interface IPartRepository
    {
        Task<Part> GetPart(int id);

        Task<IEnumerable<Part>> GetAllParts();

        Task <int> GetCountAvailable(int id);

        Task <bool> SaveDatabase();

        Task<IEnumerable<Part>> GetAllPartsPaged(PagedParams pagedParams);

        Task<int> GetNumberOfParts();
    }
}