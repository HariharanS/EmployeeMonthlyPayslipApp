using EmployeeMonthlyPayslipApp.Interfaces.TaxStructure;

namespace EmployeeMonthlyPayslipApp.Models.Models.TaxStructure
{
    public class TaxSlab : ITaxSlab
    {
        
        public decimal MaxIncome { get; set; }
        public decimal MinIncome { get; set; }
        public ITax Tax { get; set; }
    }
}