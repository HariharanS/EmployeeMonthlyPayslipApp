﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeePayDetailsDomain.Interfaces;

namespace EmployeePayDetailsDomain.Models
{
    public class Salary : ISalary
    {
        public decimal AnnualSalary { get; set; }
        public decimal SuperRate { get; set; }
    }
}
