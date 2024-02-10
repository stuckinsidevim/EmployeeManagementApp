using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data
{
    public class InMemoryDataContext
    {
        // TODO: may be a set to throw eror on already existing users.
        public List<Employee> Employees { get; } = new List<Employee>();
        public List<Leave> Leaves { get; } = new List<Leave>();

        private static InMemoryDataContext? _instance;
        private static readonly object _lock = new();
        public static InMemoryDataContext Instance
        {
            get
            {
                lock (_lock)
                {
                    _instance ??= new InMemoryDataContext();
                    return _instance;
                }
            }
        }

        private InMemoryDataContext()
        {
            var topLevelManger = new Employee
            {
                Id = 0,
                Name = "TopG",
                Role = Role.Manager,

                Username = "topg",
                Password = "topg123",
            };

            Employees.Add(topLevelManger);
        }
    }
}
