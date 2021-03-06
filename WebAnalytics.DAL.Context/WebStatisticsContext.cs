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
                .Entity<Entities.Action>()
                .Property(a => a.ActionType)
                .HasConversion(
                    t => t.ToString(),
                    t => (ClientActionType)Enum.Parse(typeof(ClientActionType), t));

            modelBuilder
                .Entity<Client>()
                .Property(et => et.ClientId)
                .ValueGeneratedNever();
        }

        public DbSet<Entities.Action> Actions { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<TimeOnPage> TimesOnPages { get; set; }
    }
}
