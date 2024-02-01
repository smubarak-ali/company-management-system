using CompanyManagement.Repository.Context;
using CompanyManagement.Repository.Entity;
using CompanyManagement.Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Created this method to only get the id and name for performance reasons.
        /// Using dynamic here to allow for the flexibility of writing a simple select and mapping the object dynamically
        /// </summary>
        /// <returns>ICollection<dynamic></returns>
        public async Task<ICollection<dynamic>> GetCompaniesForDropdown()
        {
            return await _dbContext.Company.Select(x => new { Id = x.Id, Name = x.CompanyName }).ToListAsync<dynamic>();
        }

        public async Task<int> SaveCompany(Company company)
        {
            await _dbContext.Company.AddAsync(company);
            await _dbContext.SaveChangesAsync();
            return company.Id;
        }
    }
}
