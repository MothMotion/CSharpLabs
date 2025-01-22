using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}

        public DbSet<Game> Games {get; set;}
        public DbSet<Player> Players {get; set;}
        public DbSet<GameOwnership> GameOwnerships {get; set;}
        public DbSet<Developer> Developers {get; set;}
        public DbSet<Series> GameSeries {get; set;}
    }
}