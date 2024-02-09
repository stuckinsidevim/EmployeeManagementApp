using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.DTOs;
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

        public void ApplyForLeave(LeaveDto leaveDto)
        {
            var leave = new Leave
            {
                EmployeeId = leaveDto.EmployeeId,
                StartDate = leaveDto.StartDate,
                EndDate = leaveDto.EndDate,
                Status = leaveDto.Status, // Assuming initial status is set here.
            };
            _leaveRepository.Add(leave);
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

        public IEnumerable<LeaveDto> GetLeavesForEmployee(int employeeId)
        {
            var leaves = _leaveRepository.GetByEmployeeId(employeeId);
            return leaves.Select(l => new LeaveDto
            {
                LeaveId = l.LeaveId,
                EmployeeId = l.EmployeeId,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Status = l.Status,
            }).ToList();
        }

        internal object GetLeaveById(int id)
        {
            throw new NotImplementedException();
        }

        internal bool UpdateLeaveStatus(int id, LeaveStatusDto statusDto)
        {
            throw new NotImplementedException();
        }
        // Additional methods as needed
    }


}
