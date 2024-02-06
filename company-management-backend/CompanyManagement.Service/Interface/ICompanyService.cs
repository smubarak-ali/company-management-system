using CompanyManagement.Criteria;
using CompanyManagement.Repository.Entity;
using CompanyManagement.Shared.Dto;
using CompanyManagement.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Service.Interface
{
    public interface ICompanyService
    {
        Task<CompanyDto?> GetById(int id);
        Task<PaginatedResponse<CompanyDto>> GetCompanies(CompanySearchCriteria criteria);
        Task<ICollection<CompanyDropdownDto>> GetCompaniesForDropdown();
        Task UpdateCompany(int companyId, CompanyDto company);
        Task<int> InsertCompany(CompanyDto company);
    }
}
