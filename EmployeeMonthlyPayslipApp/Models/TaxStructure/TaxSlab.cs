using System;
using System.ComponentModel;
using EmployeeMonthlyPayslipInterfaces.TaxStructure;

namespace EmployeeMonthlyPayslipApp.Models.TaxStructure
{
    public class TaxSlab : ITaxSlab
    {
        
        public decimal MaxIncome { get; set; }
        public decimal MinIncome { get; set; }
        public ITax Tax { get; set; }
    }
}