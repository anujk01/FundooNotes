using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.DBContext
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options): base(options)
        { 

        }

        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
            .HasIndex(u => u.email)
            .IsUnique();
        }
    }
}
