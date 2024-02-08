using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data.Repositories
{
    public class InMemoryLeaveRepository : ILeaveRepository
    {
        private readonly List<Leave> _leaves = new();

        public Leave GetById(int id)
        {
            return _leaves.FirstOrDefault(l => l.LeaveId == id);
        }

        public void Add(Leave leave)
        {
            _leaves.Add(leave);
        }

        public IEnumerable<Leave> GetByEmployeeId(int employeeId)
        {
            return _leaves.Where(l => l.EmployeeId == employeeId);
        }

        public void Update(Leave leave)
        {
            throw new NotImplementedException();
        }
    }
}
