using System;

namespace EmployeeMonthlyPayslipInterfaces
{
    public interface IPaySlipDetails
    {
        string FullName { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        decimal GrossIncome { get; set; }
        decimal NetIncome { get; set; }
        decimal IncomeTax { get; set; }
        decimal Super { get; set; }
    }
}
