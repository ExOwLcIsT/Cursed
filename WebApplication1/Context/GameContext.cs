using Cursed.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Cursed.Context
{
    public class GameContext : IdentityDbContext<User>
    {
        public GameContext(DbContextOptions<GameContext> connection) : base(connection)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Skin>().HasKey(x => x.Id);
            modelBuilder.Entity<Skin>().Property(p => p.Id).UseIdentityColumn(1, 1);
            modelBuilder.Entity<User>().HasOne(x => x.Skin).WithMany(x => x.Users).HasForeignKey(x => x.SkinId);
            modelBuilder.Entity<User>().Property(x => x.ImagePath).HasDefaultValue("../images/Default_pfp.jpg");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Skin> Skins { get; set; }

    }
}
