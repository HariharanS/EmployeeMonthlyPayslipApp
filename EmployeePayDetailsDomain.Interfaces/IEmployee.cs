using System.Collections.Generic;

namespace EmployeePayDetailsDomain.Interfaces
{
    public interface IEmployee
    {
        ISalary Salary { get; set; }
        IEnumerable<ISalarySlip> SalarySlips { get; set; }
    }
}