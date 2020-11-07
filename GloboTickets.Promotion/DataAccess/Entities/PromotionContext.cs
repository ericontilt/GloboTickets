﻿using Microsoft.EntityFrameworkCore;

namespace GloboTickets.Promotion.DataAccess.Entities
{
    public class PromotionContext : DbContext
    {
        public PromotionContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Show> Show { get; set; }
        public DbSet<Content> Content { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Show>()
                .HasAlternateKey(show => new { show.ShowGuid });

            modelBuilder.Entity<ShowRemoved>()
                .HasAlternateKey(showRemoved => new { showRemoved.ShowId, showRemoved.RemovedDate });

            modelBuilder.Entity<ShowDescription>()
                .HasAlternateKey(showDescription => new { showDescription.ShowId, showDescription.ModifiedDate });

            modelBuilder.Entity<Content>()
                .HasKey(content => content.Hash);
            modelBuilder.Entity<Content>()
                .Property(content => content.Binary)
                .IsRequired();
        }
    }
}