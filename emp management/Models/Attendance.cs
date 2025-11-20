// Attendance.cs - Model for tracking daily employee attendance status.
using System.ComponentModel.DataAnnotations;

namespace emp_management.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty; // Present, Absent, etc.
    }
}
