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
            var employee = (await GetCompanyByIdAsync(id)).Employee;
            _context.Companies.Remove(employee);
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
        var employee = await _context.Employees.Include(p => p.CompanyId).FirstOrDefaultAsync(c => c.Id == id);
        return (true, null , employee);
    }

    public Task<(bool IsSuccess, Exception Exception, List<Employee> Employees)> GetEmployeesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<(bool IsSuccess, Exception Exception)> UpdateEmployeeAsync(Employee entity)
    {
        throw new NotImplementedException();
    }
}