using System.Threading.Tasks;
using TechParts.API.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using TechParts.API.Dtos;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace TechParts.API.Data
{
    public class UserRepositroy : IUserRepositroy
    {
        private DataContext _context { get; }
        public IWebHostEnvironment _hostEnv { get; }

        public UserRepositroy(DataContext context, IWebHostEnvironment _hostEnv)
        {
            this._context = context;
            this._hostEnv = _hostEnv;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUser(string username)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.username == username);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _context.users.ToListAsync();
            return users;
        }

        public async Task<bool> CreateUserPart(int id, int partId, bool isWishList)
        {
            await _context.userParts.AddAsync(new UserPart(id,partId,isWishList));

            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }

        /* returns list of all user parts
        public async Task<IEnumerable<Part>> GetWishlist(int id)
        {
            return await _context.userParts.Where(up => up.UserId == id && up.IsWishlist).ToListAsync();
        }
        */

        // returns a list of only the Part field of user parts
        public async Task<IEnumerable<Part>> GetWishlist(int id)
        {
            return await _context.userParts.Where(up => up.UserId == id && up.IsWishlist).Select(p => p.Part).ToListAsync();
        }

        // Returns a list of a custom dto we made that we set the values of here
        public async Task<IEnumerable<UserPartToReturnDto>> GetCart(int id)
        {
            return await _context.userParts.Where(up => up.UserId == id && !up.IsWishlist)
            .Select(p => new UserPartToReturnDto()
            {
                Id = p.PartId,
                Description = p.Part.Description,
                Name = p.Part.Name,
                CountAvailable = p.Part.CountAvailable,
                Count = p.Count

            }).ToListAsync();
        }

        // Returns a list of a new custom object we create in the method
        /*
        public async Task<IEnumerable<object>> GetCart(int id)
        {
            return await _context.userParts.Where(up => up.UserId == id && !up.IsWishlist)
            .Select(p => new
            {
                Id = p.PartId,
                Description = p.Part.Description,
                Name = p.Part.Name,
                CountAvailable = p.Part.CountAvailable,
                Count = p.Count

            }).ToListAsync();
        }
        */

        public async Task<UserPart> GetWishlistItem(int id)
        {
            return await _context.userParts.FirstOrDefaultAsync(up => up.Id == id && up.IsWishlist == true);
        }

        public async Task<UserPart> GetCartItem(int id)
        {
            return await _context.userParts.FirstOrDefaultAsync(up => up.Id == id && up.IsWishlist == false);
        }

        public async Task<UserPart> GetWishlistItem(int userId, int partId)
        {
            return await _context.userParts.FirstOrDefaultAsync(up => up.UserId == userId && up.PartId == partId && up.IsWishlist == true);
        }

        public async Task<UserPart> GetCartItem(int userId, int partId)
        {
            return await _context.userParts.FirstOrDefaultAsync(up => up.UserId == userId && up.PartId == partId && up.IsWishlist == false);
        }

        public async Task<bool> WishlistItemExists(int id, int partId)
        {
            var item = await _context.userParts.FirstOrDefaultAsync(up => up.UserId == id && up.PartId == partId && up.IsWishlist == true);

            return item != null;
        }

        public async Task<bool> CartItemExists(int id, int partId)
        {
            var item = await _context.userParts.FirstOrDefaultAsync(up => up.UserId == id && up.PartId == partId && up.IsWishlist == false);

            return item != null;
        }

        public async Task<bool> SaveDatabase()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddLikes(int id, int recieverId)
        {
            Like newLike = new Like(id, recieverId);

            Like like = await GetLike(id,recieverId);

            if (like != null)
            {
                return false;
            }

            await _context.AddAsync<Like>(newLike);

            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<Like> GetLike(int id)
        {
            return await _context.likes.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Like> GetLike(int likerId, int likeeId)
        {
            return await _context.likes.FirstOrDefaultAsync(l => l.LikerId == likerId && l.LikeeId == likeeId);
        }

        public async Task<IEnumerable<SimpleUserToReturnDto>> GetLikers(int id)
        {
            return await _context.likes.Where(l => l.LikeeId == id).Select(l => new SimpleUserToReturnDto()
            {
                Id = l.Liker.Id,
                username = l.Liker.username,
                mainPhotoLink = l.Liker.mainPhotoLink
            }).ToListAsync();
        }

        public async Task<IEnumerable<SimpleUserToReturnDto>> GetLikees(int id)
        {
            return await _context.likes.Where(l => l.LikerId == id).Select(l => new SimpleUserToReturnDto()
            {
                Id = l.Likee.Id,
                username = l.Likee.username,
                mainPhotoLink = l.Likee.mainPhotoLink
            }).ToListAsync();
        }

        public async Task<bool> AddPhotoRecord(int id, IFormFile file)
        {
            int count = await _context.photos.CountAsync(p => p.UserId == id);

            string fName = id.ToString() + "_" + count.ToString() + Helpers.HelperMethods.GetExtension(file.ContentType);

            if(count == 0)
            {
                var user = await _context.users.FirstOrDefaultAsync(u => u.Id == id);

                user.mainPhotoLink = Helpers.HelperMethods.GetImagePath(fName);

                if(await _context.SaveChangesAsync() <= 0)
                    return false;
            }

            string path = Path.Combine(_hostEnv.ContentRootPath, "Images/" + fName);
            
            try{
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch(System.Exception e)
            {
                System.Console.WriteLine("--------------------------------------");
                System.Console.WriteLine("Image saving failed");
                System.Console.WriteLine(e.Message);
                System.Console.WriteLine("--------------------------------------");
                return false;
            }

            Photo newPhoto = new Photo(id, fName);

            await _context.AddAsync<Photo>(newPhoto);

            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> SetMainPhoto(int id, int photoId)
        {
            User user = await _context.users.FirstOrDefaultAsync(u => u.Id == id);

            Photo photo = await _context.photos.FirstOrDefaultAsync(p => p.Id == photoId);

            if (user == null) System.Console.WriteLine("Couldn't find uer with id " + id);
            if (photo == null) System.Console.WriteLine("Couldn't find photo with id " + photoId);

            user.mainPhotoLink = Helpers.HelperMethods.GetImagePath(photo.Name);

            if(await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }
    }
}