﻿using Microsoft.EntityFrameworkCore;
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
                .Property(a => a.ActionType)
                .HasConversion(
                    t => t.ToString(),
                    t => (ClientActionType)Enum.Parse(typeof(ClientActionType), t));

            modelBuilder
                .Entity<UniqueUsersCounter>()
                .HasData(new UniqueUsersCounter() {
                    Id = 1,
                    Counter = 0
                });
        }

        public DbSet<ClientAction> ClientActions { get; set; }
        public DbSet<UniqueUsersCounter> UniqueUsersCounters { get; set; }
    }
}
