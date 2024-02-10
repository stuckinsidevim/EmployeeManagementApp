using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.DTOs;
using EmployeeManagementApp.Models;
using EmployeeManagementApp.Utilities;

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

        public IEnumerable<Leave> GetAllLeaves(int employeeId)
        {
            return _leaveRepository.GetAll(employeeId);
        }

        public Leave ApplyForLeave(LeaveSetDto leaveDto, int employeeId)
        {
            if (leaveDto.StartDate >= leaveDto.EndDate)
            {
                throw new ArgumentException("Start date must be before the end date.");
            }
            var leave = new Leave
            {
                Id = Uid.NewUid(), // NOTE: may be create Uid class for each model
                EmployeeId = employeeId,
                StartDate = leaveDto.StartDate,
                EndDate = leaveDto.EndDate,
                Status = LeaveStatus.Pending,
            };
            _leaveRepository.Add(leave);
            return leave;
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

        public IEnumerable<LeaveSetDto> GetLeavesForEmployee(int employeeId)
        {
            throw new NotImplementedException();
            // var leaves = _leaveRepository.GetByEmployeeId(employeeId);
            // return leaves.Select(l => new LeaveSetDto
            // {
            //     Id = l.Id,
            //     StartDate = l.StartDate,
            //     EndDate = l.EndDate,
            //     Status = l.Status,
            // }).ToList();
        }

        public Leave GetLeaveById(int id)
        {
            return _leaveRepository.GetById(id);
        }

        public bool UpdateLeaveStatus(int id, LeaveStatusDto leaveStatusDto)
        {

            if (!Enum.TryParse(leaveStatusDto.Status, true, out LeaveStatus parsedLeaveStatus))
            {
                throw new ArgumentException($"Invalid role value: {leaveStatusDto.Status}");
            }
            var toUpdateLeave = new Leave
            {
                Id = id,
                Status = parsedLeaveStatus,
            };
            return _leaveRepository.Update(toUpdateLeave);

        }

        public bool CanManagerUpdateLeave(int managerId, int leaveId)
        {
            var leave = _leaveRepository.GetById(leaveId);
            if (leave == null) return false;

            return _employeeRepository.GetReportees(managerId).Any(r => r.Id == leave.EmployeeId);
        }
    }


}
