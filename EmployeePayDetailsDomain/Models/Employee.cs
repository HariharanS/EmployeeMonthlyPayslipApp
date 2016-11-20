using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeePayDetailsDomain.Interfaces;

namespace EmployeePayDetailsDomain.Models
{
    public class Employee : Person, IEmployee
    {
        public ISalary Salary { get; set; }
        public IEnumerable<ISalarySlip> SalarySlips { get; set; }
    }
}
