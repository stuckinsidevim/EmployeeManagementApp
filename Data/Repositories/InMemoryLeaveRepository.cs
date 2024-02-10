using EmployeeManagementApp.Data.Repositories.Interfaces;
using EmployeeManagementApp.Models;


namespace EmployeeManagementApp.Data.Repositories
{
    public class InMemoryLeaveRepository : ILeaveRepository
    {

        private readonly InMemoryDataContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        public InMemoryLeaveRepository(InMemoryDataContext dataContext, IEmployeeRepository employeeRepository)
        {
            _context = dataContext;
            _employeeRepository = employeeRepository;
        }

        public IEnumerable<Leave> GetAll(int employeeId)
        {
            return _context.Leaves.Where(l => l.EmployeeId == employeeId);
        }

        public Leave GetById(int id)
        {
            return _context.Leaves.FirstOrDefault(l => l.Id == id);
        }

        public void Add(Leave leave)
        {
            var employee = _employeeRepository.GetById(leave.EmployeeId) ?? throw new InvalidOperationException($"Employee with ID {leave.EmployeeId} not found.");
            // Ensure the employee's Leaves list is initialized and updated
            employee.Leaves ??= new List<Leave>();
            employee.Leaves.Add(leave);
            leave.Employee = employee;

            _context.Leaves.Add(leave);
        }

        public IEnumerable<Leave> GetByEmployeeId(int employeeId)
        {
            return _context.Leaves.Where(l => l.EmployeeId == employeeId).ToList();
        }

        public bool Update(Leave leave)
        {
            var existingLeave = _context.Leaves.FirstOrDefault(l => l.Id == leave.Id);
            if (existingLeave == null)
            {
                return false;
            }

            var properties = typeof(Leave).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == "Id" || property.Name == "Employee" || (property.PropertyType.IsClass && property.PropertyType != typeof(string)))
                {
                    continue;
                }

                var newValue = property.GetValue(leave);
                var defaultValue = property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : null;

                if (newValue != null && !newValue.Equals(defaultValue))
                {
                    property.SetValue(existingLeave, newValue);
                }
            }

            return true;
        }
    }
}
