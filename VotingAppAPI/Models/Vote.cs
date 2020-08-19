namespace VotingAppAPI.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public virtual Voter Voter { get; set; }

        public int VoterId { get; set; }

        public virtual Candidate Candidate { get; set; }

        public int CandidateId { get; set; }

        public int ElectionId { get; set; }

        public virtual Election Election { get; set; }

        public Vote(int elecId, int votrId, int candId)
        {
            this.ElectionId = elecId;
            this.VoterId = votrId;
            this.CandidateId = candId;
        }

        public Vote()
        {
            
        }
    }
}