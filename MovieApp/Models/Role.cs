using Microsoft.AspNetCore.Identity;

namespace MovieApp.Models
{
    public class Role: IdentityRole
    {
        public virtual System.Collections.Generic.ICollection<UserRole> userRoles { get; set; }
    }
}