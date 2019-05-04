using Microsoft.EntityFrameworkCore;
using System;
using WebAnalytics.DAL.Entities;
using WebAnalytics.Misc.Common.Enums;

namespace WebAnalytics.DAL.Context
{
    public class WebStatisticsContext : DbContext
    {
        public WebStatisticsContext(DbContextOptions<WebStatisticsContext> dbContextOptions) : base(dbContextOptions)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ClientAction>()
                .Property(l => l.ActionType)
                .HasConversion(
                    t => t.ToString(),
                    t => (ClientActionType)Enum.Parse(typeof(ClientActionType), t));
        }

        public DbSet<ClientAction> ClientActions { get; set; }
    }
}
