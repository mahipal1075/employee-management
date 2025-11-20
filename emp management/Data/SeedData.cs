// SeedData.cs - Seeds the database with sample employees, salaries, and attendance records.
using Microsoft.EntityFrameworkCore;
using emp_management.Models;

namespace emp_management.Data
{
    public static class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            
            // Ensure database is created
            context.Database.EnsureCreated();

            // Check if already seeded
            if (context.Employees.Any())
            {
                return; // DB has been seeded
            }

            // Seed Employees
            var employees = new[]
            {
                new Employee
                {
                    FirstName = "John",
                    MiddleName = "A",
                    LastName = "Doe",
                    Email = "john.doe@company.com",
                    Phone = "555-0101",
                    Address = "123 Main St, City",
                    Department = "IT",
                    Designation = "Software Engineer",
                    DateOfJoining = new DateTime(2020, 1, 15)
                },
                new Employee
                {
                    FirstName = "Jane",
                    MiddleName = "B",
                    LastName = "Smith",
                    Email = "jane.smith@company.com",
                    Phone = "555-0102",
                    Address = "456 Oak Ave, City",
                    Department = "HR",
                    Designation = "HR Manager",
                    DateOfJoining = new DateTime(2019, 6, 1)
                }
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();

            // Seed Salaries
            var salaries = new[]
            {
                new Salary
                {
                    EmployeeId = 1,
                    Month = 11,
                    Year = 2025,
                    Basic = 50000,
                    Allowances = 10000,
                    Deductions = 5000,
                    NetSalary = 55000 // Basic + Allowances - Deductions
                },
                new Salary
                {
                    EmployeeId = 2,
                    Month = 11,
                    Year = 2025,
                    Basic = 60000,
                    Allowances = 15000,
                    Deductions = 7000,
                    NetSalary = 68000 // Basic + Allowances - Deductions
                }
            };

            context.Salaries.AddRange(salaries);
            context.SaveChanges();

            // Seed Attendance
            var attendances = new[]
            {
                new Attendance
                {
                    EmployeeId = 1,
                    Date = DateTime.Today.AddDays(-1),
                    Status = "Present"
                },
                new Attendance
                {
                    EmployeeId = 1,
                    Date = DateTime.Today,
                    Status = "Present"
                },
                new Attendance
                {
                    EmployeeId = 2,
                    Date = DateTime.Today.AddDays(-1),
                    Status = "Present"
                },
                new Attendance
                {
                    EmployeeId = 2,
                    Date = DateTime.Today,
                    Status = "Absent"
                }
            };

            context.Attendances.AddRange(attendances);
            context.SaveChanges();
        }
    }
}
