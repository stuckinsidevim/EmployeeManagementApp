using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Models;


namespace EmployeeManagementApp.Data.Repositories
{
    public class InMemoryLeaveRepository : ILeaveRepository
    {

        private readonly InMemoryDataContext _dataContext;
        private readonly IEmployeeRepository _employeeRepository;
        public InMemoryLeaveRepository(InMemoryDataContext dataContext, IEmployeeRepository employeeRepository)
        {
            _dataContext = dataContext;
            _employeeRepository = employeeRepository;
        }

        public Leave GetById(int id)
        {
            return _dataContext.Leaves.FirstOrDefault(l => l.LeaveId == id);
        }

        public void Add(Leave leave)
        {
            // Assuming Leave.EmployeeId is already set and valid
            var employee = _employeeRepository.GetById(leave.EmployeeId);
            if (employee == null)
            {
                throw new InvalidOperationException($"Employee with ID {leave.EmployeeId} not found.");
            }
            // Ensure the employee's Leaves list is initialized and updated
            employee.Leaves ??= new List<Leave>();
            employee.Leaves.Add(leave);

            _dataContext.Leaves.Add(leave);
        }

        public IEnumerable<Leave> GetByEmployeeId(int employeeId)
        {
            return _dataContext.Leaves.Where(l => l.EmployeeId == employeeId).ToList();
        }

        public void Update(Leave leave)
        {
            var index = _dataContext.Leaves.FindIndex(l => l.LeaveId == leave.LeaveId);
            if (index != -1)
            {
                var employee = _dataContext.Employees.FirstOrDefault(e => e.EmployeeId == leave.EmployeeId);
                if (employee == null)
                {
                    throw new InvalidOperationException($"Employee with ID {leave.EmployeeId} not found for updating leave.");
                }
                leave.Employee = employee; // Ensure the leave's employee reference is updated.
                _dataContext.Leaves[index] = leave;
            }
            else
            {
                throw new InvalidOperationException($"Leave with ID {leave.LeaveId} not found.");
            }
        }
    }
}
