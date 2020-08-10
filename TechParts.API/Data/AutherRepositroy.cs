using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechParts.API.Dtos;
using TechParts.API.Models;

namespace TechParts.API.Data
{
    public class AutherRepositroy : IAutherRepositroy
    {

        DataContext _context;

        public AutherRepositroy(DataContext context)
        {
            this._context = context;
        }

        public async Task<User> Login(string username, string password)
        {
            System.Console.WriteLine("logging in " + username);

            var user = await _context.users.FirstOrDefaultAsync(x => x.username == username);

            if(user == null)
            {
                return null;
            }

            if(!VerifyPasswordHash(password, user.passwordHash, user.passwordSalt))
            {
                return null;
            }

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.passwordHash = passwordHash;
            user.passwordSalt = passwordSalt;

            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.users.AnyAsync(x => x.username == username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }
        
        public async Task<bool> VerifyUser(UserVerifyDto userToVerify)
        {
            var user = await Login(userToVerify.username, userToVerify.password);

            if (user == null)
            {
                return false;
            }

            if (user.IsVerified)
            {
                return false;
            }

            System.Console.WriteLine("The code sent is " + userToVerify.code);

            System.Console.WriteLine("The code here is " + user.OTP.Code);

            if(user.OTP.Code == userToVerify.code)
            {
                user.IsVerified = true;

                if(await _context.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<bool> VerifyUser(int id)
        {
            var userFromRepo = await _context.users.FirstOrDefaultAsync(u => u.Id == id);

            if(userFromRepo == null)
            {
                return false;
            }

            if (userFromRepo.IsVerified)
            {
                return false;
            }

            userFromRepo.IsVerified = true;

            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }
    }
}