using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data.Repositories
{
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private readonly InMemoryDataContext _context;

        public InMemoryEmployeeRepository(InMemoryDataContext dataContext)
        {
            _context = dataContext;
        }

        public Employee? GetById(int id)
        {
            // Using recursive method to ensure even nested reportees can be found
            return FindEmployeeById(id, _context.Employees);
        }

        public void Add(Employee employee)
        {
            if (employee.Role == Role.Manager)
            {
                employee.Reportees = new List<Employee>();
            }

            if (employee.ManagerId.HasValue)
            {
                var manager = GetById(employee.ManagerId.Value);
                if (manager != null)
                {
                    employee.Manager = manager;
                    manager.Reportees ??= new List<Employee>();
                    manager.Reportees.Add(employee);
                }
            }

            _context.Employees.Add(employee);
        }

        public void Update(Employee employee)
        {
            var existingEmployee = GetById(employee.Id);
            if (existingEmployee != null)
            {
                // Directly manipulating the list item, assuming employee reference is found
                // This could be expanded to explicitly update fields if necessary
                var index = _context.Employees.IndexOf(existingEmployee);
                if (index != -1)
                {
                    _context.Employees[index] = employee;
                }
            }
        }

        public IEnumerable<Employee> GetAllManagers()
        {
            return _context.Employees.Where(e => e.Reportees != null && e.Reportees.Any()).ToList();
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
                if (employee.Id == id)
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
            return _context.Employees;
        }

        public Employee? GetByUsername(string username)
        {
            return _context.Employees.Find(e => e.Username == username);
        }
    }
}
