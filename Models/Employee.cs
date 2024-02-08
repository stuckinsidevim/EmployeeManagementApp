namespace EmployeeManagementApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public Role Role { get; set; } // Assuming Role is an enum
        public int? ManagerId { get; set; }
        // Direct references without lazy loading
        public Employee Manager { get; set; }
        public ICollection<Employee> Reportees { get; set; }
        public ICollection<Leave> Leaves { get; set; }

        public Employee()
        {
            Reportees = new List<Employee>();
            Leaves = new List<Leave>();
        }
    }

    public enum Role
    {
        Employee,
        Manager,
    }
}
