using Qulix.Entities;

namespace Qulix.Repositories;
public interface ICompanyRepository
{
    Task<(bool IsSuccess, Exception Exception)> AddCompanyAsync(Company entity);
    Task<(bool IsSuccess, Exception Exception)> UpdateCompanyAsync(Company entity);
    Task<(bool IsSuccess, Exception Exception, Company Company)> GetCompanyByIdAsync(int id);
    Task<(bool IsSuccess, Exception Exception, List<Company> Companies)> GetCompanysAsync();
    Task<(bool IsSuccess, Exception Exception)> DeleteCompanyAsync(int id);
}