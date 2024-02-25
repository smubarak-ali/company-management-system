using CompanyManagement.Repository.Entity;
using CompanyManagement.Service.Interface;
using CompanyManagement.Shared.Dto;
using CompanyManagement.Shared.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyManagement.Shared.Mapper;

namespace CompanyManagement.Service.Implementation
{
    public class IndustryService : IIndustryService
    {
        private readonly IIndustryRepository _industryRepository;

        public IndustryService(IIndustryRepository industryRepository)
        {
            _industryRepository = industryRepository;
        }

        public async Task<ICollection<IndustryDto>> GetIndustries()
        {
            var list = await _industryRepository.GetIndustriesAsync();
            return list.ToDto();
        }
    }
}