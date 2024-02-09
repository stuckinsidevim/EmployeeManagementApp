using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.DTOs;
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

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        internal object GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }

        internal bool UpdateEmployee(EmployeeDto employeeDto)
        {
            throw new NotImplementedException();
        }

        internal bool DeleteEmployee(int id)
        {
            throw new NotImplementedException();
        }

        // Additional methods as needed
    }
}
