namespace TechParts.API.Models
{
    public class UserPart
    {
        public int Id { get; set; }

        public int PartId { get; set; }

        public virtual Part Part { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public bool IsWishlist { get; set; }

        public int Count { get; set; }

        public UserPart(int userId, int partId, bool IsWishlist)
        {
            this.UserId = userId;
            this.PartId = partId;
            this.IsWishlist = IsWishlist;
            this.Count = 1;
        }
    }
}