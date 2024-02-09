using EmployeeManagementApp.Data;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Services
{
    public class UserService
    {
        private readonly InMemoryDataContext _context;

        public UserService(InMemoryDataContext context)
        {
            _context = context;
        }

        public Employee? Authenticate(string username, string password)
        {
            Employee? user = _context.Employees.FirstOrDefault(u => u.Username == username);
            return user != null && VerifyPasswordHash(password, user.Password) ? user : null;
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // Implement verification of hashed password
            // This is pseudo-code. Use a proper hashing library like BCrypt.Net
            return true;
        }
    }
}
