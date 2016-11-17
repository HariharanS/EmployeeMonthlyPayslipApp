namespace EmployeeMonthlyPayslipInterfaces.TaxStructure
{
    public interface ITax
    {
        decimal? AdditionalTaxPercentageOverMinIncome { get; set; }
        decimal? FlatTax { get; set; }
    }
}