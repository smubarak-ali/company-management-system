using CompanyManagement.Repository.Context;
using CompanyManagement.Repository.Entity;
using CompanyManagement.Shared.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Repository.Repository.Implementation
{
    public class IndustryRepository : IIndustryRepository
    {
        private readonly ManagementDbContext _dbContext;

        public IndustryRepository(ManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ICollection<Industry>> GetIndustries()
        {
            return await _dbContext.Industry.ToListAsync();
        }
    }
}