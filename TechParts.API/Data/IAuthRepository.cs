using System.Threading.Tasks;
using System.Collections.Generic;
using TechParts.API.Dtos;
using TechParts.API.Models;

namespace TechParts.API.Data
{
    public interface IAutherRepositroy
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string username, string password);

        Task<bool> UserExists(string username);

        Task<bool> VerifyUser(UserVerifyDto userToVerify);

        Task<bool> VerifyUser(int id);
    }
}