using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Asernatat_3.Models;

namespace Asernatat_3.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Art> Arts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Art>()
                .HasOne(r => r.Category)
                .WithMany(i => i.Arts)
                .IsRequired();
            //modelBuilder.Entity<Order>();
        }        
    }
}
