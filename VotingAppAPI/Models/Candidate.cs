

namespace VotingAppAPI.Models
{
    public class Candidate
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public string Statement { get; set; }
    }
}