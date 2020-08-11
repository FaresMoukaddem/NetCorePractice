using System.Collections.Generic;

namespace VotingAppAPI.Models
{
    public class Election
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }

        public virtual ICollection<Candidate> Candidates { get; set; }

        public bool IsOver { get; set; }

        public virtual Candidate Winner { get; set; }

        public Election(string name)
        {
            this.Name = name;
            this.IsOver = false;
            this.Winner = null;
        }
    }
}