// Salary.cs - Model for employee salary with basic, allowances, deductions, and net salary.
using System.ComponentModel.DataAnnotations;

namespace emp_management.Models
{
    public class Salary
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Basic Salary")]
        public decimal Basic { get; set; }

        [Required]
        public decimal Allowances { get; set; }

        [Required]
        public decimal Deductions { get; set; }

        [Display(Name = "Net Salary")]
        public decimal NetSalary { get; set; }
    }
}
