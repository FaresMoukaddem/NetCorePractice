using System.Collections.Generic;
using TechParts.API.Models;

namespace TechParts.API.Dtos
{
    public class SimpleUserToReturnDto
    {
        public int Id { get; set; }

        public string username { get; set; }

        public string mainPhotoLink { get; set; } = Helpers.HelperMethods.GetHostPath() + "/Images/def.png";
    }
}