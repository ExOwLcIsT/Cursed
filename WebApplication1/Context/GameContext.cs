using Cursed.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cursed.Context
{
    public class GameContext : IdentityDbContext<User>
    {
        public GameContext(DbContextOptions<GameContext> connection) : base(connection)
        {
            Database.EnsureCreated();
        }
        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasOne(x => x.Skin).WithMany(x=>x.Users).HasForeignKey(x=>x.SkinId);
            modelBuilder.Entity<Skin>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.ImagePath).HasDefaultValue("~/images/Default_pfp.jpg");
        }
        DbSet<User> Users;
        DbSet<Skin> Skins;
    }
}
