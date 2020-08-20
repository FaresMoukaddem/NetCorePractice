using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MovieApp.Models
{
    public class User : IdentityUser<string>
    {
        public string Nickname { get; set; }

        public virtual ICollection<UserRole> userRoles { get; set; }

        public User(string userName)
        {
            this.UserName = this.Nickname = userName;
        }

    }
}