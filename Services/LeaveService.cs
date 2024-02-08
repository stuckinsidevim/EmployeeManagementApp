using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Services
{

    public class LeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public LeaveService(ILeaveRepository leaveRepository, IEmployeeRepository employeeRepository)
        {
            _leaveRepository = leaveRepository;
            _employeeRepository = employeeRepository;
        }

        public void ApplyForLeave(int employeeId, Leave leave)
        {
            var employee = _employeeRepository.GetById(employeeId);
            if (employee != null)
            {
                leave.EmployeeId = employeeId;
                _leaveRepository.Add(leave);
            }
        }

        public void ApproveLeave(int managerId, int leaveId, LeaveStatus status)
        {
            var leave = _leaveRepository.GetById(leaveId);
            var manager = _employeeRepository.GetById(managerId);
            if (leave != null && manager != null && leave.Employee.ManagerId == managerId)
            {
                leave.Status = status;
                // Assuming Update method exists to handle status changes
                _leaveRepository.Update(leave);
            }
        }

        // Additional methods as needed
    }


}
