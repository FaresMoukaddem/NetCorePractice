using System; 
using System.Collections.Generic;

namespace TechParts.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string email { get; set; }

        public string username { get; set; }

        public byte[] passwordHash { get; set; }

        public byte[] passwordSalt { get; set; }

        public virtual IEnumerable<UserPart> userParts { get; set; }

        public bool IsVerified { get; set; }

        public DateTime lastActive { get; set; }

        public virtual OTP OTP { get; set; }

        public virtual ICollection<Like> likees { get; set; }

        public virtual ICollection<Like> likers { get; set; }

        public virtual ICollection<Photo> photos { get; set; }

        public string mainPhotoLink { get; set; } = Helpers.HelperMethods.GetHostPath() + "/Images/def.png";

        public User()
        {
            this.lastActive = DateTime.Now;
            this.IsVerified = false;
            this.OTP = new OTP();
        }
    }
}