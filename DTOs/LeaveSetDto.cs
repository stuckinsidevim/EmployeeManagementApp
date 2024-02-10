namespace EmployeeManagementApp.DTOs
{
    public class LeaveSetDto
    {
        public int? EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
