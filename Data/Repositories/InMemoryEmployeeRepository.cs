using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data.Repositories
{
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly InMemoryDataContext _dataContext;

        public InMemoryEmployeeRepository(InMemoryDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Employee GetById(int id)
        {
            // Using recursive method to ensure even nested reportees can be found
            return FindEmployeeById(id, _dataContext.Employees);
        }

        public void Add(Employee employee)
        {
            // Set the Manager relationship
            if (employee.ManagerId.HasValue)
            {
                var manager = GetById(employee.ManagerId.Value);
                if (manager != null)
                {
                    employee.Manager = manager;
                    // Ensure the manager's Reportees list is updated
                    manager.Reportees ??= new List<Employee>();
                    manager.Reportees.Add(employee);
                }
            }

            // Add the employee to the repository
            _dataContext.Employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            var existingEmployee = GetById(employee.EmployeeId);
            if (existingEmployee != null)
            {
                // Directly manipulating the list item, assuming employee reference is found
                // This could be expanded to explicitly update fields if necessary
                var index = _dataContext.Employees.IndexOf(existingEmployee);
                if (index != -1)
                {
                    _dataContext.Employees[index] = employee;
                }
            }
        }

        public IEnumerable<Employee> GetAllManagers()
        {
            return _dataContext.Employees.Where(e => e.Reportees != null && e.Reportees.Any()).ToList();
        }

        public IEnumerable<Employee> GetReportees(int managerId)
        {
            var manager = GetById(managerId);
            return manager?.Reportees ?? new List<Employee>();
        }

        private Employee? FindEmployeeById(int id, IEnumerable<Employee> searchList)
        {
            foreach (var employee in searchList)
            {
                if (employee.EmployeeId == id)
                {
                    return employee;
                }

                var foundInReportees = FindEmployeeById(id, employee.Reportees);
                if (foundInReportees != null)
                {
                    return foundInReportees;
                }
            }
            return null;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _dataContext.Employees;
        }
    }
}
