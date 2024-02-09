using System.Security.Claims;
using EmployeeManagementApp.DTOs;
using EmployeeManagementApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementApp.Controllers
{
    [Route("api/sessions")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly UserService _userService;

        public SessionsController(UserService userService)
        {
            _userService = userService;
        }

        // POST: api/sessions (Login)
        [HttpPost]
        public async Task<IActionResult> CreateSession([FromBody] LoginDto loginDto)
        {
            var user = _userService.Authenticate(loginDto.Username, loginDto.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.Username),
                new(ClaimTypes.Role, user.Role.ToString())
                // Optionally, include more claims as necessary
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, // Make this dynamic based on client request if necessary
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Ok(new { message = "Login successful" });
        }

        // DELETE: api/sessions (Logout)
        [HttpDelete]
        public async Task<IActionResult> DeleteSession()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Logout successful" });
        }
    }
}
