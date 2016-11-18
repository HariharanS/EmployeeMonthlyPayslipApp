namespace EmployeeMonthlyPayslipInterfaces.Domain
{
    public interface ISalary
    {
        decimal AnnualSalary { get; set; }
        decimal SuperRate { get; set; }
    }
}