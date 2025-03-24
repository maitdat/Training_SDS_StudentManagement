using Microsoft.EntityFrameworkCore;
using SeverGrpc_NHibernate.Model;

namespace SeverGrpc_NHibernate.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Student { get; set; } = default!;
        public DbSet<Teacher> Teacher { get; set; } = default!;
        public DbSet<Class> Class { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, Name = "John Doe", DateOfBirth = new DateTime(1980, 5, 20) },
                new Teacher { Id = 2, Name = "Jane Smith", DateOfBirth = new DateTime(1985, 8, 15) },
                new Teacher { Id = 3, Name = "Robert Johnson", DateOfBirth = new DateTime(1975, 3, 10) }
            );

            // Seed Class (3 classes per teacher)
            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, Name = "Math 101", Subject = "Mathematics", TeacherId = 1 },
                new Class { Id = 2, Name = "Math 102", Subject = "Mathematics", TeacherId = 1 },
                new Class { Id = 3, Name = "Math 103", Subject = "Mathematics", TeacherId = 1 },

                new Class { Id = 4, Name = "Physics 201", Subject = "Physics", TeacherId = 2 },
                new Class { Id = 5, Name = "Physics 202", Subject = "Physics", TeacherId = 2 },
                new Class { Id = 6, Name = "Physics 203", Subject = "Physics", TeacherId = 2 },

                new Class { Id = 7, Name = "Chemistry 301", Subject = "Chemistry", TeacherId = 3 },
                new Class { Id = 8, Name = "Chemistry 302", Subject = "Chemistry", TeacherId = 3 },
                new Class { Id = 9, Name = "Chemistry 303", Subject = "Chemistry", TeacherId = 3 }
            );


        }
    }
}
