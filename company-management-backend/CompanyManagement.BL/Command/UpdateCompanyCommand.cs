using CompanyManagement.Repository.Entity;
using CompanyManagement.Repository.Repository.Interface;
using CompanyManagement.Shared.Dto;
using CompanyManagement.Shared.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.BL.Command
{
    public record UpdateCompanyCommand(int companyId, CompanyDto company) : IRequest;

    public sealed class UpdateCompanyCommandHandler(ICompanyRepository companyRepository) : IRequestHandler<UpdateCompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository = companyRepository;

        public async Task Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            CompanyDto company = request.company;
            var entity = await _companyRepository.GetById(request.companyId) ?? throw new InvalidIdException("Invalid company Id provided");

            entity.CompanyName = company.CompanyName;
            entity.City = company.City;
            entity.IndustryId = company.IndustryId;
            entity.Level = company.Level;
            entity.ParentCompany = company.ParentCompany;
            entity.TotalEmployees = company.TotalEmployees;

            await _companyRepository.UpdateCompany(request.companyId, entity);
        }
    }
}
