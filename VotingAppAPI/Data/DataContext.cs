using Microsoft.EntityFrameworkCore;
using VotingAppAPI.Models;

namespace VotingAppAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Voter> voters { get; set; }

        public DbSet<Candidate> candidates { get ; set; }

        public DbSet<Vote> votes { get; set; }

        public DbSet<Election> elections { get; set; }
    }
}