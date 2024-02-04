﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Repository.Entity
{
    public class Industry
    {
        public int Id { get; set; }
        public string IndustryName { get; set; }

        public ICollection<Company> Companys { get; set; } = new List<Company>();

    }
}
