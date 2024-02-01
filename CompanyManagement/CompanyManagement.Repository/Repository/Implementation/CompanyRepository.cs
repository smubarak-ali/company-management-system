using CompanyManagement.Repository.Context;
using CompanyManagement.Repository.Entity;
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
            return await _dbContext.Company.Where(x => int.Equals(x.Id, id)).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Company>> GetCompanies()
        {
            var list = await _dbContext.Company.ToListAsync();
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
            Company? entity = await _dbContext.Company.Where(x => int.Equals(x.Id, companyId)).FirstOrDefaultAsync();

            if (entity == null) return;

            _dbContext.Entry(entity).CurrentValues.SetValues(company);
            await _dbContext.SaveChangesAsync();
        }
    }
}