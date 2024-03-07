using CompanyManagement.Criteria;
using CompanyManagement.Repository.Repository.Interface;
using CompanyManagement.Shared.Dto;
using CompanyManagement.Shared.Mapper;
using CompanyManagement.Shared.Model;
using MediatR;

namespace CompanyManagement.BL.Query
{
    public record GetCompanyByCriteriaQuery(CompanySearchCriteria criteria) : IRequest<PaginatedResponse<CompanyDto>>;

    public sealed class GetCompanyByCriteriaHandler(ICompanyRepository companyRepository) : IRequestHandler<GetCompanyByCriteriaQuery, PaginatedResponse<CompanyDto>>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;

        public async Task<PaginatedResponse<CompanyDto>> Handle(GetCompanyByCriteriaQuery request, CancellationToken cancellationToken)
        {
            var list = await _companyRepository.GetCompanies(request.criteria);
            PaginatedResponse<CompanyDto> responseList = new();
            responseList.TotalPages = list.TotalPages;
            responseList.Items = list.ToDto();
            return responseList;
        }
    }
}