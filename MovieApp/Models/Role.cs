using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MovieApp.Models
{
    public class Role: IdentityRole<string>
    {
        public virtual ICollection<UserRole> userRoles { get; set; }
    }
}