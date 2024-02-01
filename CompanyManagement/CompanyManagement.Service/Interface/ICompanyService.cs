using CompanyManagement.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Service.Interface
{
    public interface ICompanyService
    {
        Task<ICollection<Company>> GetCompanies();
        Task<ICollection<Company>> GetCompaniesForDropdown();
    }
}
