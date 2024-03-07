using CompanyManagement.Repository.Repository.Interface;
using CompanyManagement.Shared.Dto;
using CompanyManagement.Shared.Mapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.BL.Query
{
    public record GetCompanyByIdQuery(int companyId) : IRequest<CompanyDto?>;


    public sealed class GetCompanyById : IRequestHandler<GetCompanyByIdQuery, CompanyDto?>
    {
        private ICompanyRepository _companyRepository;

        public GetCompanyById(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<CompanyDto?> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetById(request.companyId, cancellationToken);
            return company?.ToDto();
        }
    }
}
