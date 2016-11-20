using System;

namespace EmployeePayDetailsDomain.Interfaces
{
    public interface ISalarySlip
    {
        DateTime EndDate { get; set; }
        decimal GrossIncome { get; }
        decimal IncomeTax { get; }
        decimal NetIncome { get; }
        DateTime StartDate { get; set; }
        decimal Super { get; }
    }
}