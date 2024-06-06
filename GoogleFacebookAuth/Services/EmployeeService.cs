using GoogleFacebookAuth.Data;
using GoogleFacebookAuth.Models;
using GoogleFacebookAuth.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace GoogleFacebookAuth.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly GoogleFacebookAuthContext _context;

        public EmployeeService(GoogleFacebookAuthContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IEnumerable<Employee>> SearchEmployeesByNameAsync(string name)
        {
            return await _context.Employees.Where(e => e.Name.Contains(name)).ToListAsync();
        }

        public async Task<bool> AddEmployeeAsync(Employee employee)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var emp = await _context.Employees.FindAsync(employee.Id);
            if (emp == null)
            {
                return false;
            }

            emp.Salary = employee.Salary;
            emp.Department = employee.Department;
            emp.Name = employee.Name;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
