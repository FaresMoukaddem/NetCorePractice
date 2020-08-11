using System.Collections.Generic;

namespace VotingAppAPI.Models
{
    public class ElectionForCreationDto
    {
        public string Name { get; set; }

        public virtual ICollection<int> Candidates { get; set; }
    }
}