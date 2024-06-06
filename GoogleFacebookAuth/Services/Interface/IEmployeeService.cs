using GoogleFacebookAuth.Models;

namespace GoogleFacebookAuth.Services.Interface
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> SearchEmployeesByNameAsync(string name);
        Task<bool> AddEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
    }
}
