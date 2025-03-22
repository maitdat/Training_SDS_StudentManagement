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

            

        }
    }
}
