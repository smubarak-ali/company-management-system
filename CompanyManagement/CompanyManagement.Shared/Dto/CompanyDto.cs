using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Shared.Dto
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public required string CompanyName { get; set; }
        public int IndustryId { get; set; }
        public int TotalEmployees { get; set; }
        public string? City { get; set; }
        public required string ParentCompany { get; set; }
        public int Level { get; set; }

    }
}
