using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data.Repositories
{

    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees = new();

        public Employee GetById(int id)
        {
            return _employees.FirstOrDefault(e => e.EmployeeId == id);
        }

        public void Add(Employee employee)
        {
            _employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            var index = _employees.FindIndex(e => e.EmployeeId == employee.EmployeeId);
            if (index == -1) return;
            _employees[index] = employee;
        }

        public IEnumerable<Employee> GetAllManagers()
        {
            return _employees.Where(e => e.Reportees.Any());
        }

        public IEnumerable<Employee> GetReportees(int managerId)
        {
            return _employees.Where(e => e.ManagerId == managerId);
        }
    }
}
