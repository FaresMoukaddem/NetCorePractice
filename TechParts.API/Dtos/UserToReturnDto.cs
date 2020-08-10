using System.Collections.Generic;
using TechParts.API.Models;

namespace TechParts.API.Dtos
{
    public class UserToReturnDto
    {
        public int Id { get; set; }

        public string username { get; set; }
        
        public string mainPhotoLink { get; set; } = Helpers.HelperMethods.GetHostPath() + "/Images/def.png";

        public virtual IEnumerable<UserPart> userParts { get; set; }
    }
}