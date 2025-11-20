// ApplicationDbContext.cs - EF Core DbContext with Employee, Salary, and Attendance tables.
using Microsoft.EntityFrameworkCore;
using emp_management.Models;

namespace emp_management.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}
