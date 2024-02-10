using System.Text.Json.Serialization;

namespace EmployeeManagementApp.Models
{

    public class Leave
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveStatus Status { get; set; }
        public int EmployeeId { get; set; }

        [JsonIgnore]
        public Employee Employee { get; set; } = null!;
    }

    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
