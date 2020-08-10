using System.Collections.Generic;
using System.Threading.Tasks;
using TechParts.API.Models;
using TechParts.API.Dtos;
using Microsoft.AspNetCore.Http;

namespace TechParts.API.Data
{
    public interface IUserRepositroy
    {
        Task<User> GetUser(int id);

        Task<User> GetUser(string username);

        Task<IEnumerable<User>> GetAllUsers();
    
        Task<bool> CreateUserPart(int id, int partId, bool isWishList);

        Task<IEnumerable<Part>> GetWishlist(int id);

        Task<IEnumerable<UserPartToReturnDto>> GetCart(int id);

        Task<UserPart> GetWishlistItem(int id);

        Task<UserPart> GetCartItem(int id);

        Task<UserPart> GetWishlistItem(int userId, int partId);

        Task<UserPart> GetCartItem(int userId, int partId);

        Task<bool> WishlistItemExists(int id, int partId);

        Task<bool> CartItemExists(int id, int partId);
        
        Task <bool> SaveDatabase();

        Task<bool> AddLikes(int id, int recieverId);

        Task<Like> GetLike(int id);

        Task<Like> GetLike(int likerId, int likeeId);

        Task<IEnumerable<SimpleUserToReturnDto>> GetLikers(int id);

        Task<IEnumerable<SimpleUserToReturnDto>> GetLikees(int id);

        Task<bool> AddPhotoRecord(int id, IFormFile file);

        Task<bool> SetMainPhoto(int id, int photoId);
    }
}