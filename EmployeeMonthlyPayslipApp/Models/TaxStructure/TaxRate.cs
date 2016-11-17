using System.Collections.Generic;
using EmployeeMonthlyPayslipInterfaces.TaxStructure;

namespace EmployeeMonthlyPayslipApp.Models.TaxStructure
{
    public class TaxRate : ITaxRate
    {
        public IEnumerable<ITaxSlab> TaxSlab { get; set; }
        public int TaxYearEnd { get; set; }
        public int TaxYearStart { get; set; }
    }
}