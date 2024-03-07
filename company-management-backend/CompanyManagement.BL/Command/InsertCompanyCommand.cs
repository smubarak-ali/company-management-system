using CompanyManagement.Repository.Entity;
using CompanyManagement.Repository.Repository.Interface;
using CompanyManagement.Shared.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.BL.Command
{
    public record InsertCompanyCommand(CompanyDto Dto) : IRequest<int>;

    public class InsertCompanyCommandHandler(ICompanyRepository companyRepository) : IRequestHandler<InsertCompanyCommand, int>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;

        public async Task<int> Handle(InsertCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = request.Dto;
            Company entity = new()
            {
                CompanyName = company.CompanyName,
                City = company.City,
                CompanyNo = company.CompanyNo,
                IndustryId = company.IndustryId,
                Level = (!string.IsNullOrEmpty(company.ParentCompany) && !string.Equals(company.ParentCompany, "none", StringComparison.OrdinalIgnoreCase) ? 1 : 0),
                ParentCompany = company.ParentCompany,
                TotalEmployees = company.TotalEmployees
            };

            int id = await _companyRepository.SaveCompany(entity);
            return id;
        }
    }
}
