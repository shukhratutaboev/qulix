using Qulix.Entities;

namespace Qulix.Repositories;
public interface IEmployeeRepository
{
    Task<(bool IsSuccess, Exception Exception)> AddEmployeeAsync(Employee entity);
    Task<(bool IsSuccess, Exception Exception)> UpdateEmployeeAsync(Employee entity);
    Task<(bool IsSuccess, Exception Exception, Employee Employee)> GetEmployeeByIdAsync(int id);
    Task<(bool IsSuccess, Exception Exception, List<Employee> Employees)> GetEmployeesAsync();
    Task<(bool IsSuccess, Exception Exception)> DeleteEmployeeAsync(int id);
}