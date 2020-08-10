using Microsoft.EntityFrameworkCore;
using TechParts.API.Models;

namespace TechParts.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<User> users { get; set; }

        public DbSet<Part> parts { get ; set; }

        public DbSet<UserPart> userParts { get; set; }

        public DbSet<OTP> otps { get; set; }

        public DbSet<Like> likes { get; set; }

        public DbSet<Photo> photos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Like>()
            .HasOne(u => u.Likee)
            .WithMany(u => u.likers)
            .HasForeignKey(u => u.LikeeId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
            .HasOne(u => u.Liker)
            .WithMany(u => u.likees)
            .HasForeignKey(u => u.LikerId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}