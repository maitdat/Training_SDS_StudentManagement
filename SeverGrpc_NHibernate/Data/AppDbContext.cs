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
                new Teacher { Id = 1, Name = "Nguyen Van A", DateOfBirth = new DateTime(1980, 5, 20) },
                new Teacher { Id = 2, Name = "Tran Thi B", DateOfBirth = new DateTime(1985, 8, 15) },
                new Teacher { Id = 3, Name = "Le Van C", DateOfBirth = new DateTime(1975, 3, 10) },
                new Teacher { Id = 4, Name = "Pham Thi D", DateOfBirth = new DateTime(1982, 6, 25) },
                new Teacher { Id = 5, Name = "Hoang Van E", DateOfBirth = new DateTime(1978, 11, 30) },
                new Teacher { Id = 6, Name = "Do Thi F", DateOfBirth = new DateTime(1983, 4, 5) },
                new Teacher { Id = 7, Name = "Bui Van G", DateOfBirth = new DateTime(1981, 9, 12) },
                new Teacher { Id = 8, Name = "Nguyen Thi H", DateOfBirth = new DateTime(1984, 2, 18) },
                new Teacher { Id = 9, Name = "Tran Van I", DateOfBirth = new DateTime(1979, 7, 22) },
                new Teacher { Id = 10, Name = "Le Thi K", DateOfBirth = new DateTime(1986, 10, 28) }
            );

            // Seed Class (5 classes per teacher)
            modelBuilder.Entity<Class>().HasData(
                new Class { Id = 1, Name = "Software Engineering 101", Subject = "Software Engineering", TeacherId = 1 },
                new Class { Id = 2, Name = "Software Engineering 102", Subject = "Software Engineering", TeacherId = 1 },
                new Class { Id = 3, Name = "Software Engineering 103", Subject = "Software Engineering", TeacherId = 1 },
                new Class { Id = 4, Name = "Software Engineering 104", Subject = "Software Engineering", TeacherId = 1 },
                new Class { Id = 5, Name = "Software Engineering 105", Subject = "Software Engineering", TeacherId = 1 },

                new Class { Id = 6, Name = "Database Systems 201", Subject = "Database Systems", TeacherId = 2 },
                new Class { Id = 7, Name = "Database Systems 202", Subject = "Database Systems", TeacherId = 2 },
                new Class { Id = 8, Name = "Database Systems 203", Subject = "Database Systems", TeacherId = 2 },
                new Class { Id = 9, Name = "Database Systems 204", Subject = "Database Systems", TeacherId = 2 },
                new Class { Id = 10, Name = "Database Systems 205", Subject = "Database Systems", TeacherId = 2 },

                new Class { Id = 11, Name = "Computer Networks 301", Subject = "Computer Networks", TeacherId = 3 },
                new Class { Id = 12, Name = "Computer Networks 302", Subject = "Computer Networks", TeacherId = 3 },
                new Class { Id = 13, Name = "Computer Networks 303", Subject = "Computer Networks", TeacherId = 3 },
                new Class { Id = 14, Name = "Computer Networks 304", Subject = "Computer Networks", TeacherId = 3 },
                new Class { Id = 15, Name = "Computer Networks 305", Subject = "Computer Networks", TeacherId = 3 },

                new Class { Id = 16, Name = "Artificial Intelligence 401", Subject = "Artificial Intelligence", TeacherId = 4 },
                new Class { Id = 17, Name = "Artificial Intelligence 402", Subject = "Artificial Intelligence", TeacherId = 4 },
                new Class { Id = 18, Name = "Artificial Intelligence 403", Subject = "Artificial Intelligence", TeacherId = 4 },
                new Class { Id = 19, Name = "Artificial Intelligence 404", Subject = "Artificial Intelligence", TeacherId = 4 },
                new Class { Id = 20, Name = "Artificial Intelligence 405", Subject = "Artificial Intelligence", TeacherId = 4 },

                new Class { Id = 21, Name = "Machine Learning 501", Subject = "Machine Learning", TeacherId = 5 },
                new Class { Id = 22, Name = "Machine Learning 502", Subject = "Machine Learning", TeacherId = 5 },
                new Class { Id = 23, Name = "Machine Learning 503", Subject = "Machine Learning", TeacherId = 5 },
                new Class { Id = 24, Name = "Machine Learning 504", Subject = "Machine Learning", TeacherId = 5 },
                new Class { Id = 25, Name = "Machine Learning 505", Subject = "Machine Learning", TeacherId = 5 },

                new Class { Id = 26, Name = "Cyber Security 601", Subject = "Cyber Security", TeacherId = 6 },
                new Class { Id = 27, Name = "Cyber Security 602", Subject = "Cyber Security", TeacherId = 6 },
                new Class { Id = 28, Name = "Cyber Security 603", Subject = "Cyber Security", TeacherId = 6 },
                new Class { Id = 29, Name = "Cyber Security 604", Subject = "Cyber Security", TeacherId = 6 },
                new Class { Id = 30, Name = "Cyber Security 605", Subject = "Cyber Security", TeacherId = 6 },

                new Class { Id = 31, Name = "Data Science 701", Subject = "Data Science", TeacherId = 7 },
                new Class { Id = 32, Name = "Data Science 702", Subject = "Data Science", TeacherId = 7 },
                new Class { Id = 33, Name = "Data Science 703", Subject = "Data Science", TeacherId = 7 },
                new Class { Id = 34, Name = "Data Science 704", Subject = "Data Science", TeacherId = 7 },
                new Class { Id = 35, Name = "Data Science 705", Subject = "Data Science", TeacherId = 7 },

                new Class { Id = 36, Name = "Cloud Computing 801", Subject = "Cloud Computing", TeacherId = 8 },
                new Class { Id = 37, Name = "Cloud Computing 802", Subject = "Cloud Computing", TeacherId = 8 },
                new Class { Id = 38, Name = "Cloud Computing 803", Subject = "Cloud Computing", TeacherId = 8 },
                new Class { Id = 39, Name = "Cloud Computing 804", Subject = "Cloud Computing", TeacherId = 8 },
                new Class { Id = 40, Name = "Cloud Computing 805", Subject = "Cloud Computing", TeacherId = 8 },

                new Class { Id = 41, Name = "Software Testing 901", Subject = "Software Testing", TeacherId = 9 },
                new Class { Id = 42, Name = "Software Testing 902", Subject = "Software Testing", TeacherId = 9 },
                new Class { Id = 43, Name = "Software Testing 903", Subject = "Software Testing", TeacherId = 9 },
                new Class { Id = 44, Name = "Software Testing 904", Subject = "Software Testing", TeacherId = 9 },
                new Class { Id = 45, Name = "Software Testing 905", Subject = "Software Testing", TeacherId = 9 },

                new Class { Id = 46, Name = "Human-Computer Interaction 1001", Subject = "Human-Computer Interaction", TeacherId = 10 },
                new Class { Id = 47, Name = "Human-Computer Interaction 1002", Subject = "Human-Computer Interaction", TeacherId = 10 },
                new Class { Id = 48, Name = "Human-Computer Interaction 1003", Subject = "Human-Computer Interaction", TeacherId = 10 },
                new Class { Id = 49, Name = "Human-Computer Interaction 1004", Subject = "Human-Computer Interaction", TeacherId = 10 },
                new Class { Id = 50, Name = "Human-Computer Interaction 1005", Subject = "Human-Computer Interaction", TeacherId = 10 }
            );
        }
    }
}
