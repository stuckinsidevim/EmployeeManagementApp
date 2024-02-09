using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.DTOs
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; }
        public int? ManagerId { get; set; }
        // Optional: Include additional fields such as a collection of ReporteeIds for simplicity.
    }

}
