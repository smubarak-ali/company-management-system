using CompanyManagement.Repository.Entity;
using CompanyManagement.Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Shared.Mapper
{
    public static class EntityToDto
    {
        public static List<IndustryDto> ToDto(this ICollection<Industry> entityList)
        {
            var dtoList = new List<IndustryDto>();
            if (entityList == null)
                return dtoList;

            dtoList = entityList.Select(x => new IndustryDto { Id = x.Id, Name = x.IndustryName }).ToList();  
            return dtoList;
        }

        public static List<CompanyDto> ToDto(this ICollection<Company> companyList)
        {
            var dtoList = new List<CompanyDto>();
            if (companyList == null)
                return dtoList;

            dtoList = companyList.Select(x => x.ToDto()).ToList();
            return dtoList;
        }

        public static CompanyDto ToDto(this Company company)
        {
            var dto = new CompanyDto();
            if (company == null)
                return dto;

            dto.Id = company.Id;
            dto.IndustryId = company.IndustryId;
            dto.City = company.City;
            dto.CompanyNo = company.CompanyNo;
            dto.TotalEmployees = company.TotalEmployees;
            dto.CompanyName = company.CompanyName;
            dto.Level = company.Level;
            dto.ParentCompany = company.ParentCompany;
            if (company.Industry != null)
                dto.IndustryName = company.Industry.IndustryName;
            
            return dto;
        }
    }
}