using Microsoft.EntityFrameworkCore;

namespace The_Town_Cleaner.Server.Models.Contexts {
    public class GameDbContext : DbContext{

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options) { }
    }
}
