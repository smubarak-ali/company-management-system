using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Criteria
{
    public class CompanySearchCriteria
    {
        public int? CompanyNo { get; set; }
        public string? CompanyName { get; set; }
        public int? IndustryId { get; set; }
        public string? City { get; set; }
        public string? ParentCompany { get; set; }

        public bool CompanyNameDesc { get; set; } = false;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }
}
