using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data.Repositories.Interfaces
{
    public interface ILeaveRepository
    {
        Leave GetById(int id);
        void Add(Leave leave);
        IEnumerable<Leave> GetByEmployeeId(int employeeId);
        void Update(Leave leave);
        // Add other necessary operations
    }
}
