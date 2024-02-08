using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data
{
    public class InMemoryDataContext
    {
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

        private InMemoryDataContext() { }
    }
}
