using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.DTOs;
using EmployeeManagementApp.Models;
using EmployeeManagementApp.Utilities;

namespace EmployeeManagementApp.Services
{

    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee AddEmployee(EmployeeSetDto employeeDto, int? managerId)
        {
            if (!Enum.TryParse(employeeDto.Role, true, out Role parsedRole))
            {
                throw new ArgumentException($"Invalid role value: {employeeDto.Role}");
            }

            var employee = new Employee
            {
                Id = Uid.NewUid(),
                Name = employeeDto.Name,
                Role = parsedRole,

                Username = employeeDto.Username,
                Password = employeeDto.Password,

                ManagerId = managerId
            };
            _employeeRepository.Add(employee);
            return employee;
        }

        public void AddReportee(int managerId, Employee reportee)
        {
            var manager = _employeeRepository.GetById(managerId);
            if (manager != null)
            {
                reportee.ManagerId = manager.Id;
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

        public Employee GetEmployeeById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        internal bool UpdateEmployee(EmployeeSetDto employeeDto)
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
