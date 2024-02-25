using CompanyManagement.Criteria;
using CompanyManagement.Repository.Entity;
using CompanyManagement.Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Repository.Repository.Interface
{
    public interface ICompanyRepository
    {
        Task<Company?> GetById(int id, CancellationToken cancellationToken = default);
        Task<PaginatedList<Company>> GetCompanies(CompanySearchCriteria criteria, CancellationToken cancellationToken = default);
        Task<ICollection<Company>> GetCompaniesForDropdown(CancellationToken cancellationToken = default);
        Task<int> SaveCompany(Company company, CancellationToken cancellationToken = default);
        Task UpdateCompany(int companyId, Company company, CancellationToken cancellationToken = default);
    }
}
