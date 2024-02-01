using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Shared.Dto
{
    public class CompanyDto
    {
        public int Id { get; set; }

        public int CompanyNo { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression("[a-z A-Z]+")]
        public string CompanyName { get; set; }

        public int IndustryId { get; set; }

        [Range(1, 1000000)]
        public int TotalEmployees { get; set; }

        [MaxLength(50)]
        [RegularExpression("[a-z A-Z-]+")]
        public string? City { get; set; }

        public string ParentCompany { get; set; }

        public int Level { get; set; }

    }

    public class CompanyDropdownDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
