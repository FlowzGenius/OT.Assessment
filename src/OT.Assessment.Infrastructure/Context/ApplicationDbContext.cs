using Microsoft.EntityFrameworkCore;
using OT.Assessment.Infrastructure.Entities;
using OT.Assessment.Infrastructure.Mappings;

namespace OT.Assessment.Infrastructure.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Player> Player { get; set; }
        public DbSet<Wager> Wager { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlayerMapping());
            modelBuilder.ApplyConfiguration(new WagerMapping());
        }
    }
}
