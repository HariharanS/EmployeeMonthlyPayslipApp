using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMonthlyPayslipInterfaces;

namespace EmployeeMonthlyPayslipApp.Models
{
    public class EmployeeDetails : IEmployeeDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public string TaxPeriod { get; set; }
        public decimal SuperRate { get; set; }
    }

    public class EmployeeDetailsInput 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal AnnualSalary { get; set; }
        public string TaxPeriod { get; set; }
        public string SuperPercentage { get; set; }
    }
}
