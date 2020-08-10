

namespace TechParts.API.Models
{
    public class Photo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public Photo(int userId, string name)
        {
            this.UserId = userId;
            this.Name = name;
        }
    }
}