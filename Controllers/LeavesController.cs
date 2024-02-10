using System.Security.Claims;
using EmployeeManagementApp.DTOs;
using EmployeeManagementApp.Models;
using EmployeeManagementApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeavesController : CustomBaseController
    {
        private readonly LeaveService _leaveService;

        public LeavesController(LeaveService leaveService)
        {
            _leaveService = leaveService;
        }


        [HttpGet]
        [Authorize(Roles = $"{nameof(Role.Employee)},{nameof(Role.Manager)}")]
        public IActionResult GetAllLeaves()
        {

            var employeeId = GetEmployeeIdFromClaims();
            if (!employeeId.HasValue)
            {
                return Unauthorized();
            }
            var leaves = _leaveService.GetAllLeaves(employeeId.Value);
            return Ok(leaves);
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(Role.Employee)},{nameof(Role.Manager)}")]
        public IActionResult ApplyForLeave([FromBody] LeaveSetDto leaveDto)
        {

            var employeeIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (employeeIdClaim == null)
            {
                return Unauthorized();
            }
            var employeeId = int.Parse(employeeIdClaim.Value);
            var leave = _leaveService.ApplyForLeave(leaveDto, employeeId);
            return CreatedAtAction("GetLeaveById", new { id = leave.Id }, leave);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{nameof(Role.Employee)},{nameof(Role.Manager)}")]
        public IActionResult GetLeaveById(int id)
        {
            var leave = _leaveService.GetLeaveById(id);
            return leave != null ? Ok(leave) : NotFound();
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateLeaveStatus(int id, [FromBody] LeaveStatusDto statusDto)
        {
            var managerId = GetEmployeeIdFromClaims();
            if (managerId.HasValue && !_leaveService.CanManagerUpdateLeave(managerId.Value, id))
            {
                return Unauthorized();
            }
            bool success = _leaveService.UpdateLeaveStatus(id, statusDto);
            return success ? NoContent() : NotFound();
        }
    }
}
