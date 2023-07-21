using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cursed.Context
{
    public class GameContext : IdentityDbContext
    {
        public GameContext(DbContextOptions<GameContext> connection) : base(connection)
        {
            Database.EnsureCreated();
        }
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
