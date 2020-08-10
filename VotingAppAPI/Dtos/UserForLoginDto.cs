

namespace VotingAppAPI.Dtos
{
    public class UserForLoginDto
    {
        public string username { get; set; }

        public string password { get; set; }
    
        public bool isVoter { get; set; }
    }
}