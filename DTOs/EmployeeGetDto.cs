using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.DTOs
{
    public class EmployeeGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Role { get; set; } = null!;

        public string Username { get; set; } = null!;

        public int? ManagerId { get; set; }

        public Employee Manager { get; set; } = null!;
        public ICollection<Employee> Reportees { get; set; } = null!;
        public ICollection<Leave> Leaves { get; set; } = null!;
    }

}
