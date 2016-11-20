using EmployeeMonthlyPayslipApp.Interfaces.TaxStructure;
using EmployeePayDetailsDomain.Interfaces;
using EmployeePayDetailsDomain.Models;

namespace EmployeePayDetailsService
{
    public class EmployeePayDetailsService
    {
        private readonly ISalary _salary;

        public EmployeePayDetailsService(ISalary salary)
        {
            _salary = salary;
        }

        public ISalarySlip GetPaySlip(ITaxStructure taxStructure)
        {
            return SalarySlip.CreateSalarySlip(_salary, taxStructure);
        }
    }
}
