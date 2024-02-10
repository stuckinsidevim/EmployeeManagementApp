using EmployeeManagementApp.DTOs;
using EmployeeManagementApp.Models;
using EmployeeManagementApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : CustomBaseController
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.Manager))]
        public IActionResult AddEmployee([FromBody] EmployeeSetDto employeeDto)
        {
            var managerId = GetEmployeeIdFromClaims();
            if (!managerId.HasValue)
            {
                return Unauthorized();
            }
            var employee = _employeeService.AddEmployee(employeeDto, managerId.Value);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        // [HttpPut("{id}")]
        // public IActionResult UpdateEmployee(int id, [FromBody] EmployeeSetDto employeeDto)
        // {
        //     // FIX: do it later
        //     // if (id != employeeDto.Id) return BadRequest("ID mismatch");
        //
        //     var success = _employeeService.UpdateEmployee(employeeDto);
        //     if (!success) return NotFound();
        //
        //     return NoContent();
        // }
        //
        // [HttpDelete("{id}")]
        // public IActionResult DeleteEmployee(int id)
        // {
        //     var success = _employeeService.DeleteEmployee(id);
        //     if (!success) return NotFound();
        //
        //     return NoContent();
        // }

        [HttpGet("{managerId}/reportees")]
        public IActionResult GetReportees(int managerId)
        {
            var reportees = _employeeService.GetReportees(managerId);
            return Ok(reportees);
        }
    }
}
