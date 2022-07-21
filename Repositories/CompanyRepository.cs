using Microsoft.EntityFrameworkCore;
using Qulix.Data;
using Qulix.Entities;

namespace Qulix.Repositories;
public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<CompanyRepository> _logger;

    public CompanyRepository(ILogger<CompanyRepository> logger, AppDbContext context)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<(bool IsSuccess, Exception Exception)> AddCompanyAsync(Company entity)
    {
        try
        {
            await _context.Companies.AddAsync(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Company was added.");
            return (true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Company was not added. Exception: {e.Message}");
            return (false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception Exception)> DeleteCompanyAsync(int id)
    {
        try
        {
            var company = (await GetCompanyByIdAsync(id)).Company;
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Company was deleted.");
            return(true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Company was not deleted. Exception: {e.Message}");
            return(false, e);
        }
    }

    public async Task<(bool IsSuccess, Exception Exception, Company Company)> GetCompanyByIdAsync(int id)
    {
        var company = await _context.Companies.Include(p => p.Employees).FirstOrDefaultAsync(c => c.Id == id);
        return (true, null , company);
    }

    public async Task<(bool IsSuccess, Exception Exception, List<Company> Companies)> GetCompaniesAsync()
    {
        var companies = _context.Companies.Include(p => p.Employees).ToList();
        _logger.LogInformation("Companies query is done");
        return (true, null, companies);
    }

    public async Task<(bool IsSuccess, Exception Exception)> UpdateCompanyAsync(Company entity)
    {
        try
        {
            _context.Companies.Update(entity);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Company was updated.");
            return(true, null);
        }
        catch (Exception e)
        {
            _logger.LogError($"Company was not updated. Exception: {e.Message}");
            return(false, e);
        }
    }
}