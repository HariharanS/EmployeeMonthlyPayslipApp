using System.Collections.Generic;
using EmployeeMonthlyPayslipApp.Interfaces.TaxStructure;

namespace EmployeeMonthlyPayslipApp.Models.Models.TaxStructure
{
    public class TaxRate : ITaxRate
    {
        public IEnumerable<ITaxSlab> TaxSlab { get; set; }
        public int TaxYearEnd { get; set; }
        public int TaxYearStart { get; set; }
    }
}