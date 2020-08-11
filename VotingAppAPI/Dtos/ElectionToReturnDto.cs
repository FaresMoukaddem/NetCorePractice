using System.Collections.Generic;
using VotingAppAPI.Models;

namespace VotingAppAPI.Dtos
{
    public class ElectionToReturnDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<VoterToReturn> Votes { get; set; }

        public virtual ICollection<CandidateToReturn> Candidates { get; set; }

        public bool IsOver { get; set; }

        public virtual Candidate Winner { get; set; }
    }
}