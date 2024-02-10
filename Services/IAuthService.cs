namespace EmployeeManagementApp.Services
{
    public interface IAuthService<TUser> where TUser : class
    {
        TUser Authenticate(string username, string password);
    }
}
