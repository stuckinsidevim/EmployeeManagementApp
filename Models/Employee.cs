namespace EmployeeManagementApp.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public int? ManagerId { get; set; }

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
