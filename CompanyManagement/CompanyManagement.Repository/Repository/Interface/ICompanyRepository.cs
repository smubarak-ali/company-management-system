using CompanyManagement.Repository.Entity;
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
        Task<ICollection<Company>> GetCompanies();
        Task<ICollection<Company>> GetCompaniesForDropdown();
        Task<int> SaveCompany(Company company);
        Task UpdateCompany(int companyId, Company company);
    }
}
