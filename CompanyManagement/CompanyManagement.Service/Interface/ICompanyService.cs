using CompanyManagement.Repository.Entity;
using CompanyManagement.Shared.Dto;
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
        Task<ICollection<CompanyDto>> GetCompanies();
        Task<ICollection<CompanyDropdownDto>> GetCompaniesForDropdown();
        Task UpdateCompany(CompanyDto company);
        Task<int> InsertCompany(CompanyDto company);
    }
}
