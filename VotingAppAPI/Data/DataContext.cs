using Microsoft.EntityFrameworkCore;
using VotingAppAPI.Models;

namespace VotingAppAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Voter> voters { get; set; }

        public DbSet<Candidate> candidates { get ; set; }

        /*
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
        */
    }
}