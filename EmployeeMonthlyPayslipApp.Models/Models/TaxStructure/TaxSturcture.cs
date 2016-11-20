using EmployeeMonthlyPayslipApp.Interfaces.TaxStructure;

namespace EmployeeMonthlyPayslipApp.Models.Models.TaxStructure
{
    public class TaxStructure : ITaxStructure
    {
        public ITaxRate TaxRate { get; set; }
    }
}
