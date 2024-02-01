using CompanyManagement.Repository.Entity;
using CompanyManagement.Repository.Repository.Interface;
using CompanyManagement.Service.Interface;
using CompanyManagement.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Service.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public Task<CompanyDto?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CompanyDto>> GetCompanies()
        {
            //var list = await _companyRepository.GetCompanies();
            return null;
        }

        public async Task<ICollection<CompanyDropdownDto>> GetCompaniesForDropdown()
        {
            var list = await _companyRepository.GetCompaniesForDropdown();
            return list.Select(x => new CompanyDropdownDto { Id = x.Id, Name = x.CompanyName }).ToList();
        }

        public Task<int> InsertCompany(CompanyDto company)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCompany(CompanyDto company)
        {
            throw new NotImplementedException();
        }
    }
}