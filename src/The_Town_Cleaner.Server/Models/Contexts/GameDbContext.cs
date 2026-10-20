using Microsoft.EntityFrameworkCore;
using The_Town_Cleaner.Shared.Models.Entities;

namespace The_Town_Cleaner.Server.Models.Contexts {
    public class GameDbContext : DbContext{
        public DbSet<User> Users { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<Stage> Stages { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
    }
}
