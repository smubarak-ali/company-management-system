using CompanyManagement.Repository.Repository.Interface;
using CompanyManagement.Shared.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Query
{
    public record GetCompanyForDropdownQuery : IRequest<ICollection<CompanyDropdownDto>>;


    public sealed class GetCompanyForDropdown : IRequestHandler<GetCompanyForDropdownQuery, ICollection<CompanyDropdownDto>>
    {
        private readonly ICompanyRepository _companyRepository;

        public GetCompanyForDropdown(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<ICollection<CompanyDropdownDto>> Handle(GetCompanyForDropdownQuery request, CancellationToken cancellationToken)
        {
            var result = await _companyRepository.GetCompaniesForDropdown(cancellationToken);
            List<CompanyDropdownDto> dtoList = result.Select(x => new CompanyDropdownDto { Id = x.Id, Name = x.CompanyName }).ToList();
            return dtoList;
        }
    }

}
