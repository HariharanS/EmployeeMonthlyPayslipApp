namespace EmployeeMonthlyPayslipInterfaces
{
    public interface IEmployeeDetails
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        decimal AnnualSalary { get; set; }
        int SuperRate { get; set; }
    }
}
