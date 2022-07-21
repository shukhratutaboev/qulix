using Microsoft.EntityFrameworkCore;
using Qulix.Data;
using Qulix.Entities;

namespace Qulix.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ILogger<EmployeeRepository> _logger;
    private readonly AppDbContext _context;

    public EmployeeRepository(ILogger<EmployeeRepository> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<(bool IsSuccess, Exception Exception)> AddEmployeeAsync(Employee entity)
    {
        try
        {
            await _context.Employees.AddAsync(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Employee was added.");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Employee was not added. Exception: {e.Message}");
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception Exception)> DeleteEmployeeAsync(int id)
    {
        try
        {
            var employee = (await GetEmployeeByIdAsync(id)).Employee;
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Employee was deleted.");
            return(true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Employee was not deleted. Exception: {e.Message}");
            return(false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception Exception, Employee Employee)> GetEmployeeByIdAsync(int id)
    {
        var employee = await _context.Employees.Include(p => p.Company).FirstOrDefaultAsync(c => c.Id == id);
        return (true, null , employee);
    }

    public async Task<(bool IsSuccess, Exception Exception, List<Employee> Employees)> GetEmployeesAsync()
    {
        var employees = _context.Employees.Include(p => p.Company).ToList();
        return (true, null, employees);
    }

    public async Task<(bool IsSuccess, Exception Exception)> UpdateEmployeeAsync(Employee entity)
    {
        try
        {
            _context.Employees.Update(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Employee was updated.");
            return(true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Employee was not updated. Exception: {e.Message}");
            return(false, e);
        }
    }
}