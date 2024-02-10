
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.DTOs
{
    public class LeaveGetDto
    {
        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Employee Employee { get; set; } = null!;
    }

}
