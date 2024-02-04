using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Repository.Entity
{
    public class Company
    {
        public int Id { get; set; }
        public int CompanyNo { get; set; }
        public string CompanyName { get; set; }
        public int IndustryId { get; set; }
        public int TotalEmployees { get; set; }
        public string? City { get; set; }
        public string ParentCompany { get; set; }
        public int Level { get; set; }

        // relationship
        public Industry Industry { get; set; }

    }
}
