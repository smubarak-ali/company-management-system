using CompanyManagement.Criteria;
using CompanyManagement.Repository.Entity;
using CompanyManagement.Repository.Repository.Interface;
using CompanyManagement.Service.Interface;
using CompanyManagement.Shared.Dto;
using CompanyManagement.Shared.Exceptions;
using CompanyManagement.Shared.Mapper;
using CompanyManagement.Shared.Model;

namespace CompanyManagement.Service.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto?> GetById(int id)
        {
            var company = await _companyRepository.GetById(id);
            return company?.ToDto();
        }

        public async Task<PaginatedResponse<CompanyDto>> GetCompanies(CompanySearchCriteria criteria)
        {
            var list = await _companyRepository.GetCompanies(criteria);
            PaginatedResponse<CompanyDto> responseList = new();
            responseList.TotalPages = list.TotalPages;
            responseList.Items = list.ToDto();
            return responseList;
        }

        public async Task<ICollection<CompanyDropdownDto>> GetCompaniesForDropdown()
        {
            var list = await _companyRepository.GetCompaniesForDropdown();
            List<CompanyDropdownDto> dtoList = list.Select(x => new CompanyDropdownDto { Id = x.Id, Name = x.CompanyName }).ToList();
            return dtoList;
        }

        public async Task<int> InsertCompany(CompanyDto company)
        {
            Company entity = new()
            {
                CompanyName = company.CompanyName,
                City = company.City,
                CompanyNo = company.CompanyNo,
                IndustryId = company.IndustryId,
                Level = (!string.IsNullOrEmpty(company.ParentCompany) && !string.Equals(company.ParentCompany, "none", StringComparison.OrdinalIgnoreCase) ? 1 : 0),
                ParentCompany = company.ParentCompany,
                TotalEmployees = company.TotalEmployees
            };

            int id = await _companyRepository.SaveCompany(entity);
            return id;
        }

        public async Task UpdateCompany(int companyId, CompanyDto company)
        {
            var entity = await _companyRepository.GetById(companyId) ?? throw new InvalidIdException("Invalid company Id provided");

            entity.CompanyName = company.CompanyName;
            entity.City = company.City;
            entity.IndustryId = company.IndustryId;
            entity.Level = company.Level;
            entity.ParentCompany = company.ParentCompany;
            entity.TotalEmployees = company.TotalEmployees;

            await _companyRepository.UpdateCompany(companyId, entity);
            return;
        }
    }
}