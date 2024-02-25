using CompanyManagement.Shared.Dto;
using CompanyManagement.Shared.Interface.Repository;
using MediatR;

namespace Service.Query
{
    public record GetIndustryQuery : IRequest<ICollection<IndustryDto>>;


    public class GetIndustryHandler : IRequestHandler<GetIndustryQuery, ICollection<IndustryDto>>
    {
        private readonly IIndustryRepository _industryRepository;

        public GetIndustryHandler(IIndustryRepository industryRepository)
        {
            _industryRepository = industryRepository;
        }

        public async Task<ICollection<IndustryDto>> Handle(GetIndustryQuery request, CancellationToken cancellationToken)
        {
            var list = await _industryRepository.GetIndustriesAsync(cancellationToken);
            var dtoList = list.Select(x => new IndustryDto
            {
                Id = x.Id,
                Name = x.IndustryName
            })
                .ToList();

            return dtoList;
        }
    }
}