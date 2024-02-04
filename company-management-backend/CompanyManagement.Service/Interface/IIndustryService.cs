using CompanyManagement.Repository.Entity;
using CompanyManagement.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Service.Interface
{
    public interface IIndustryService
    {
        Task<ICollection<IndustryDto>> GetIndustries();
    }
}
