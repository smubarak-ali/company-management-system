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

        public CompanyRepository(ManagementDbContext dbContext) => _dbContext = dbContext;

        public async Task<Company?> GetById(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Company.Where(x => Equals(x.Id, id)).FirstOrDefaultAsync();
        }

        public async Task<PaginatedList<Company>> GetCompanies(CompanySearchCriteria criteria, CancellationToken cancellationToken = default)
        {
            var companies = _dbContext.Company.Include(x => x.Industry).Select(x => x);
            if (!string.IsNullOrEmpty(criteria.ParentCompany))
            {
                companies = companies.Where(x => x.ParentCompany.ToLower().Contains(criteria.ParentCompany.ToLower()));
            }
            if (!string.IsNullOrEmpty(criteria.CompanyName))
            {
                companies = companies.Where(x => x.CompanyName.ToLower().Contains(criteria.CompanyName.ToLower()));
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
                companies = companies.Where(x => x.City.ToLower().Contains(criteria.City.ToLower()));
            }
            if (criteria.TotalEmployees != null)
            {
                companies = companies.Where(x => x.TotalEmployees.Equals(criteria.TotalEmployees));
            }

            companies = criteria.CompanyNameDesc ? companies.OrderByDescending(x => x.CompanyName) : companies.OrderBy(x => x.CompanyName);

            var list = await PaginatedList<Company>.GetAsync(companies.AsNoTracking(), criteria.PageIndex, criteria.PageSize);
            return list;
        }

        /// <summary>
        /// Created this method to only get the id and name for performance reasons
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<Company>> GetCompaniesForDropdown(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Company.Select(x => new Company { Id = x.Id, CompanyName = x.CompanyName }).ToListAsync();
        }

        public async Task<int> SaveCompany(Company company, CancellationToken cancellationToken = default)
        {
            await _dbContext.Company.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return company.Id;
        }

        public async Task UpdateCompany(int companyId, Company company, CancellationToken cancellationToken = default)
        {
            Company? entity = await _dbContext.Company.Where(x => Equals(x.Id, companyId)).FirstOrDefaultAsync();

            if (entity == null) return;

            _dbContext.Entry(entity).CurrentValues.SetValues(company);
            await _dbContext.SaveChangesAsync();
        }
    }
}