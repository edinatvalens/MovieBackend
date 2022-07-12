using VaBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace VaBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<MovieCategory> MovieCategory { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
