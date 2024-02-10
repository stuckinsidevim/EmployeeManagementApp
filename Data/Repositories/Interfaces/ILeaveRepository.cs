using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data.Repositories.Interfaces
{
    public interface ILeaveRepository
    {
        Leave GetById(int id);
        IEnumerable<Leave> GetAll(int employeeId);
        void Add(Leave leave);
        IEnumerable<Leave> GetByEmployeeId(int employeeId);
        bool Update(Leave leave);
    }
}
