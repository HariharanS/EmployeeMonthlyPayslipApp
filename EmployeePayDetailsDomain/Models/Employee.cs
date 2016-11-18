using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMonthlyPayslipInterfaces.Domain;

namespace EmployeePayDetailsDomain.Models
{
    public class Employee : Person, IEmployee
    {
        public ISalary Salary { get; set; }
        public IEnumerable<ISalarySlip> SalarySlips { get; set; }
    }
}
