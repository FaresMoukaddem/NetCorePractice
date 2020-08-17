
namespace VotingAppAPI.Dtos
{
    public class VoteToSendDto
    {
        public int VoterId { get; set; }

        public int CandidateId { get; set; }

        public int ElectionId { get; set; }
    }
}