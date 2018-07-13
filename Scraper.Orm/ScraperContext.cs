//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="ScraperContext.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the ScraperContext type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Orm
{
    using Microsoft.EntityFrameworkCore;

    using Scraper.Orm.Models;

    public sealed class ScraperContext : DbContext
    {
        public ScraperContext(DbContextOptions<ScraperContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<Show> Shows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>().ToTable(nameof(Actor)).HasOne(actor => actor.Show).WithMany(show => show.Cast).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Actor>().HasKey(a => a.ActorId);
            modelBuilder.Entity<Show>().ToTable(nameof(Show));

            modelBuilder.Entity<Show>().Property(show => show.Id).ValueGeneratedNever();
        }
    }
}
