using CompanyManagement.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Shared.Interface.Repository
{
    public interface IIndustryRepository
    {
        Task<ICollection<Industry>> GetIndustries();

    }
}
