using EmployeeManagementApp.DTOs;
using EmployeeManagementApp.Models;
using EmployeeManagementApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
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
        public IActionResult AddEmployee([FromBody] Employee employeeDto)
        {
            _employeeService.AddEmployee(employeeDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employeeDto.EmployeeId }, employeeDto);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (id != employeeDto.EmployeeId) return BadRequest("ID mismatch");

            var success = _employeeService.UpdateEmployee(employeeDto);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var success = _employeeService.DeleteEmployee(id);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpGet("{managerId}/reportees")]
        public IActionResult GetReportees(int managerId)
        {
            var reportees = _employeeService.GetReportees(managerId);
            return Ok(reportees);
        }
    }
}
