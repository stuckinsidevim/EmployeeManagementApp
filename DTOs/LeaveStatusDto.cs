using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.DTOs
{
    public class LeaveStatusDto
    {
        public int LeaveId { get; set; }
        public LeaveStatus Status { get; set; }
        // Optional: Include ManagerId if the status change must be authorized by a specific manager
        public int ManagerId { get; set; }
    }
}
