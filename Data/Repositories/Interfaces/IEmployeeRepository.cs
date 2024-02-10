using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee? GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        Employee? GetByUsername(string username);
        IEnumerable<Employee> GetAllManagers();
        IEnumerable<Employee> GetReportees(int managerId);
        IEnumerable<Employee> GetAllEmployees();
        // Add other necessary operations
    }
}
