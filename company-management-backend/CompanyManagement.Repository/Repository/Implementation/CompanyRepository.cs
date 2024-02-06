using CompanyManagement.Criteria;
using CompanyManagement.Repository.Context;
using CompanyManagement.Repository.Entity;
using CompanyManagement.Repository.Extensions;
using CompanyManagement.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Repository.Repository.Implementation
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ManagementDbContext _dbContext;

        public CompanyRepository(ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Company?> GetById(int id)
        {
            return await _dbContext.Company.Where(x => Equals(x.Id, id)).FirstOrDefaultAsync();
        }

        public async Task<PaginatedList<Company>> GetCompanies(CompanySearchCriteria criteria)
        {
            var companies = _dbContext.Company.Include(x => x.Industry).Select(x => x);
            if (!string.IsNullOrEmpty(criteria.ParentCompany))
            {
                companies = companies.Where(x => x.ParentCompany.Contains(criteria.ParentCompany, StringComparison.OrdinalIgnoreCase));
            }
            if (!string.IsNullOrEmpty(criteria.CompanyName))
            {
                companies = companies.Where(x => x.CompanyName.Contains(criteria.CompanyName, StringComparison.OrdinalIgnoreCase));
            }
            if (criteria.IndustryId != null)
            {
                companies = companies.Where(x => x.IndustryId.Equals(criteria.IndustryId));
            }
            if (criteria.CompanyNo != null)
            {
                companies = companies.Where(x => x.CompanyNo.Equals(criteria.CompanyNo));
            }
            if (!string.IsNullOrEmpty(criteria.City))
            {
                companies = companies.Where(x => x.City.Contains(criteria.City, StringComparison.OrdinalIgnoreCase));
            }
            
            companies = criteria.CompanyNameDesc ? companies.OrderByDescending(x => x.CompanyName) : companies.OrderBy(x => x.CompanyName);


            var list = await PaginatedList<Company>.GetAsync(companies.AsNoTracking(), criteria.PageIndex, criteria.PageSize);
            return list;
        }

        /// <summary>
        /// Created this method to only get the id and name for performance reasons
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Company>> GetCompaniesForDropdown()
        {
            return await _dbContext.Company.Select(x => new Company { Id = x.Id, CompanyName = x.CompanyName }).ToListAsync();
        }

        public async Task<int> SaveCompany(Company company)
        {
            await _dbContext.Company.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return company.Id;
        }

        public async Task UpdateCompany(int companyId, Company company)
        {
            Company? entity = await _dbContext.Company.Where(x => Equals(x.Id, companyId)).FirstOrDefaultAsync();

            if (entity == null) return;

            _dbContext.Entry(entity).CurrentValues.SetValues(company);
            await _dbContext.SaveChangesAsync();
        }
    }
}