using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VotingAppAPI.Data;
using VotingAppAPI.Dtos;
using VotingAppAPI.Models;

namespace VotingAppAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<int> Login(UserForLoginDto user)
        {
            if(user.isVoter)
            {
                var userFromRepo = await _context.voters.FirstOrDefaultAsync(u => u.Username == user.username);

                if(userFromRepo == null) return -1;

                return BCrypt.Net.BCrypt.Verify(user.password, userFromRepo.PasswordHash) ? userFromRepo.Id : -1;
            }
            else
            {
                var userFromRepo = await _context.candidates.FirstOrDefaultAsync(u => u.Username == user.username);

                if(userFromRepo == null) return -1;

                return BCrypt.Net.BCrypt.Verify(user.password, userFromRepo.PasswordHash) ? userFromRepo.Id : -1;
            }
        }

        public async Task<CandidateToReturn> RegisterCandidate(CandidateForRegisterDto candidate)
        {
            System.Console.WriteLine("Registering candidate: ");
            System.Console.WriteLine("Username = " + candidate.username);
            System.Console.WriteLine("Password = " + candidate.password);

            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(candidate.password,salt);

            System.Console.WriteLine("Salt = " + salt);
            System.Console.WriteLine("Hashed password = " + hashedPassword);

            System.Console.WriteLine(BCrypt.Net.BCrypt.Verify(candidate.password, hashedPassword));

            Candidate candidateToStore = new Candidate
            {
                Username = candidate.username,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
            };

            await _context.candidates.AddAsync(candidateToStore);

            if(await _context.SaveChangesAsync() > 0)
            {
                return new CandidateToReturn
                {
                    username = candidate.username,
                    Statement = candidate.statement
                };
            }

            else return null;
        }

        public async Task<VoterToReturn> RegisterVoter(VoterForRegisterDto voter)
        {
            System.Console.WriteLine("Registering voter: ");
            System.Console.WriteLine("Username = " + voter.username);
            System.Console.WriteLine("Password = " + voter.password);

            var salt = BCrypt.Net.BCrypt.GenerateSalt();

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(voter.password,salt);

            System.Console.WriteLine("Salt = " + salt);
            System.Console.WriteLine("Hashed password = " + hashedPassword);

            System.Console.WriteLine(BCrypt.Net.BCrypt.Verify(voter.password, hashedPassword));

            Voter voterToStore = new Voter
            {
                Username = voter.username,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
            };

            await _context.voters.AddAsync(voterToStore);

            if(await _context.SaveChangesAsync() > 0)
            {
                return new VoterToReturn
                {
                    username = voter.username
                };
            }

            else return null;
        }

        public async Task<bool> UserNameExists(string username, bool voter)
        {
            if(voter)
            {
                return await _context.voters.FirstOrDefaultAsync(u => u.Username == username) == null;
            }
            else
            {
                return await _context.candidates.FirstOrDefaultAsync(u => u.Username == username) == null;
            }
        }
    }
}