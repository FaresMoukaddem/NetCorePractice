

namespace TechParts.API.Models
{
    public class Like
    {
        public int Id { get; set; }

        public int LikerId { get; set; }

        public int LikeeId { get; set; }

        public virtual User Liker { get; set; }

        public virtual User Likee { get; set; }

        public Like(int likerId, int likeeId)
        {
            this.LikeeId = likeeId;
            this.LikerId = likerId;
        }
    }
}