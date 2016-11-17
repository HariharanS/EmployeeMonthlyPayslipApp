using System.Collections.Generic;

namespace EmployeeMonthlyPayslipInterfaces.TaxStructure
{
    public interface ITaxRate
    {
        IEnumerable<ITaxSlab> TaxSlab { get; set; }
        int TaxYearEnd { get; set; }
        int TaxYearStart { get; set; }
    }
}