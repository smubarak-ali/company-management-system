using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Shared.Model
{
    public class PaginatedResponse<T> : List<T> where T : class
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
    }
}
