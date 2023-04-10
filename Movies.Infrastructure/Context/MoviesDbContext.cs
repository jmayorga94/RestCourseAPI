using Microsoft.EntityFrameworkCore;
using Movies.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Context
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genres> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Movie>()
                .HasKey(k => k.Id);
            modelBuilder.Entity<Movie>()
                .Property(i => i.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Movie>().Property(i => i.Title).HasMaxLength(50);

            modelBuilder.Entity<Genres>().ToTable("Genres");
            modelBuilder.Entity<Genres>()
               .HasKey(k => k.Id);
            modelBuilder.Entity<Genres>()
                .Property(i => i.Id).ValueGeneratedOnAdd();

        }



    }
}
