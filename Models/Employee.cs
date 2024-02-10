using System.Text.Json.Serialization;

namespace EmployeeManagementApp.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public Role Role { get; set; }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public int? ManagerId { get; set; }

        [JsonIgnore]
        public Employee Manager { get; set; } = null!;
        [JsonIgnore]
        public ICollection<Employee> Reportees { get; set; } = null!;
        public ICollection<Leave> Leaves { get; set; } = new List<Leave>();
    }

    public enum Role
    {
        Employee,
        Manager,
    }
}
