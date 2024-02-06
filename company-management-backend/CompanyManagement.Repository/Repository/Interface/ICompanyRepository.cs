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
        Task<Company?> GetById(int id);
        Task<PaginatedList<Company>> GetCompanies(CompanySearchCriteria criteria);
        Task<ICollection<Company>> GetCompaniesForDropdown();
        Task<int> SaveCompany(Company company);
        Task UpdateCompany(int companyId, Company company);
    }
}
