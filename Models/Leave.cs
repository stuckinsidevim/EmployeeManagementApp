namespace EmployeeManagementApp.Models
{

    public class Leave
    {
        public int LeaveId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatus Status { get; set; } // Assuming LeaveStatus is an enum
        public Employee Employee { get; set; }
    }
    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
