using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void AddEmployee(Employee employee)
        {
            _employeeRepository.Add(employee);
        }

        public void AddReportee(int managerId, Employee reportee)
        {
            var manager = _employeeRepository.GetById(managerId);
            if (manager != null)
            {
                reportee.ManagerId = manager.EmployeeId;
                manager.Reportees.Add(reportee);
                _employeeRepository.Add(reportee);
            }
        }

        public IEnumerable<Employee> GetReportees(int managerId)
        {
            return _employeeRepository.GetReportees(managerId);
        }
        // Additional methods as needed
    }
}
