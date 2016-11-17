using EmployeeMonthlyPayslipInterfaces.TaxStructure;
using System;
using System.Linq;

namespace EmployeePayDetailsDomain.Models
{
    public class SalarySlip
    {
        private readonly Salary _salary;
        private readonly ITaxStructure _taxStructure;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal GrossIncome => Math.Round(_salary.AnnualSalary / 12,0,MidpointRounding.AwayFromZero) ;
        public decimal NetIncome => GrossIncome - IncomeTax;
        public decimal IncomeTax => CalculateIncomeTax();
        public decimal Super => GrossIncome * _salary.SuperRate;

        private SalarySlip(Salary salary, ITaxStructure taxStructure)
        {
            _salary = salary;
            _taxStructure = taxStructure;
        }

        public static SalarySlip CreateSalarySlip(Salary salary,ITaxStructure taxStructure) => new SalarySlip(salary, taxStructure);

        private decimal CalculateIncomeTax()
        {
            const decimal incomeTax = 0.00M;

            var taxSlab = _taxStructure
                            .TaxRate
                            .TaxSlab
                            .FirstOrDefault(x => x.MinIncome >= _salary.AnnualSalary && 
                                                x.MaxIncome <= _salary.AnnualSalary);
            if(taxSlab == null) 
            // no tax
            if (taxSlab.Tax == null)
                return incomeTax;

            var tax = taxSlab.Tax;

            var annualTax = (_salary.AnnualSalary - taxSlab.MinIncome) *
                            (tax.AdditionalTaxPercentageOverMinIncome.Value);

            var monthlyTax = tax.FlatTax == null ? 
                                Math.Round(annualTax/12, 0, MidpointRounding.AwayFromZero) : 
                                tax.FlatTax + Math.Round(annualTax / 12, 0, MidpointRounding.AwayFromZero);

            return (decimal) monthlyTax;
        }

    }


}