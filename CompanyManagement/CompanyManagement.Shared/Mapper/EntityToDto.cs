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

            foreach (var entity in entityList)
            {
                var dto = new IndustryDto { Id = entity.Id, Name = entity.IndustryName };
                dtoList.Add(dto);
            }

            return dtoList;
        }

        //public static List<CompanyDto> ToDto(this ICollection<Company> companyList)
        //{

        //}
    }
}
