using EmployeeMonthlyPayslipInterfaces.TaxStructure;

namespace EmployeeMonthlyPayslipApp.Models.TaxStructure
{
    public class Tax : ITax
    {
        public decimal? AdditionalTaxPercentageOverMinIncome { get; set; }
        public decimal? FlatTax { get; set; }
    }
}