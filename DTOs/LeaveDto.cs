using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.DTOs
{
    public class LeaveDto
    {
        public int LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatus Status { get; set; }
        // Additional fields as necessary.
    }

}
