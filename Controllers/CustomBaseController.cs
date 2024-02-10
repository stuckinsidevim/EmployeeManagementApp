
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApp.Controllers
{

    public class CustomBaseController : ControllerBase
    {
        protected int? GetEmployeeIdFromClaims()
        {
            var employeeIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (employeeIdClaim != null && int.TryParse(employeeIdClaim.Value, out var employeeId))
            {
                return employeeId;
            }
            return null;
        }
    }
}
