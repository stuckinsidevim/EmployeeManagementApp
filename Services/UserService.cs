using System.Security.Authentication;
using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Services
{
    public class UserService : IAuthService<Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UserService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee Authenticate(string username, string password)
        {
            var user = _employeeRepository.GetByUsername(username) ?? throw new AuthenticationException("User not found.");
            if (!VerifyPasswordHash(password, user.Password))
            {
                throw new AuthenticationException("Password is incorrect.");
            }
            return user;
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // TODO: Implement verification of hashed password
            return true;
        }
    }

}
