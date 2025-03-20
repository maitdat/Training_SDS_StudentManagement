using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Training_SDS_StudentManagement.Model;
using Training_SDS_StudentManagement.Model.DataSeeding;

namespace Training_SDS_StudentManagement.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; } = default!;
        public DbSet<Teacher> Teacher { get; set; } = default!;
        public DbSet<Classes> Classes { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Seed data
            modelBuilder.Entity<Classes>().HasData(
                DataSeeding.SeedClasses()
            );

            modelBuilder.Entity<Teacher>().HasData(
                DataSeeding.SeedTeachers()
            );

        }
    }
}
