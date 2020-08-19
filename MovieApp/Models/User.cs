using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MovieApp.Models
{
    public class User : IdentityUser
    {
        public string Nickname { get; set; }

        public virtual ICollection<UserRole> userRoles { get; set; }

    }
}