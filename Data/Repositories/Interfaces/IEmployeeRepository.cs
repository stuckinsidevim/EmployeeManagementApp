using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Employee GetById(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        IEnumerable<Employee> GetAllManagers();
        IEnumerable<Employee> GetReportees(int managerId);
        // Add other necessary operations
    }
}
