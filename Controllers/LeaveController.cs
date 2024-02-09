using EmployeeManagementApp.DTOs;
using EmployeeManagementApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApp.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly LeaveService _leaveService;

        public LeaveController(LeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpPost]
        public IActionResult ApplyForLeave([FromBody] LeaveDto leaveDto)
        {
            _leaveService.ApplyForLeave(leaveDto);
            return CreatedAtAction("GetLeaveById", new { id = leaveDto.LeaveId }, leaveDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetLeaveById(int id)
        {
            var leave = _leaveService.GetLeaveById(id);
            return leave != null ? Ok(leave) : NotFound();
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateLeaveStatus(int id, [FromBody] LeaveStatusDto statusDto)
        {
            bool success = _leaveService.UpdateLeaveStatus(id, statusDto);
            return success ? NoContent() : NotFound();
        }

        [HttpGet("employee/{employeeId}")]
        public IActionResult GetLeavesForEmployee(int employeeId)
        {
            IEnumerable<LeaveDto> leaves = _leaveService.GetLeavesForEmployee(employeeId);
            return Ok(leaves);
        }
    }
}
