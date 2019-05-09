using Microsoft.EntityFrameworkCore;
using System;

namespace WebAnalytics.Misc.Identity
{
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> dbContextOptions) : base(dbContextOptions)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasData(new User[] {
                    new User()
                    {
                        UserId = Guid.NewGuid(),
                        Login = "admin",
                        Password = "admin"
                    }
                });
        }

        public DbSet<User> Users { get; set; }
    }
}
