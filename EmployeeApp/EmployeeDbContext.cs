//#pragma warning disable 1591

namespace EmployeeApp
{
    using EmployeeApp.Data;
    using EmployeeApp.Model;
    using Microsoft.EntityFrameworkCore;

    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee", "dbo");
                entity.HasKey("ID");
                entity.Property(e => e.Name);
                entity.Property(e => e.Age).HasMaxLength(20);
            });
        }
    }
}

//#pragma warning restore 1591