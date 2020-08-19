using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.Models;

namespace MovieApp.Data
{
    public class DataContext: IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // We have to put this since were using identity for some reason
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole => 
            {
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.userRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(r => r.userRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });
        }
    }
}