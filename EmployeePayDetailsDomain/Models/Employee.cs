using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayDetailsDomain.Models
{
    public class Employee : Person
    {
        public Salary Salary { get; set; }
        public IEnumerable<SalarySlip> SalarySlips { get; set; }
    }
}
