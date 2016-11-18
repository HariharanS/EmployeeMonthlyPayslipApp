using System.Collections.Generic;

namespace EmployeeMonthlyPayslipInterfaces.Domain
{
    public interface IEmployee
    {
        ISalary Salary { get; set; }
        IEnumerable<ISalarySlip> SalarySlips { get; set; }
    }
}