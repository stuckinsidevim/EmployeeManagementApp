using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data
{
    public class InMemoryDataContext
    {
        public List<Employee> Employees { get; } = new List<Employee>();
        public List<Leave> Leaves { get; } = new List<Leave>();

        private static InMemoryDataContext? _instance;
        // TODO: make it thread safe.
        public static InMemoryDataContext Instance => _instance ??= new InMemoryDataContext();

        private InMemoryDataContext() { }
    }
}
