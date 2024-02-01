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

        public async Task<ICollection<Company>> GetCompanies()
        {
            var list = await _dbContext.Company.ToListAsync();
            return list;
        }

        public async Task<ICollection<Company>> GetCompaniesForDropdown()
        {
            return await _dbContext.Company.ToListAsync();
        }
    }
}
