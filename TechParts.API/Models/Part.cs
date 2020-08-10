using System.Collections.Generic;

namespace TechParts.API.Models
{
    public class Part
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public int CountAvailable { get; set; }
    }
}