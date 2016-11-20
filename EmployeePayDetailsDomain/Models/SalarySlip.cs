using System;
using System.Linq;
using EmployeeMonthlyPayslipApp.Interfaces.TaxStructure;
using EmployeePayDetailsDomain.Interfaces;

namespace EmployeePayDetailsDomain.Models
{
    public class SalarySlip : ISalarySlip
    {
        private readonly ISalary _salary;
        private readonly ITaxStructure _taxStructure;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal GrossIncome => Math.Round(_salary.AnnualSalary / 12,0,MidpointRounding.AwayFromZero) ;
        public decimal NetIncome => GrossIncome - IncomeTax;
        public decimal IncomeTax => CalculateIncomeTax();
        public decimal Super => GrossIncome * _salary.SuperRate;

        private SalarySlip(ISalary salary, ITaxStructure taxStructure)
        {
            _salary = salary;
            _taxStructure = taxStructure;
        }

        public static SalarySlip CreateSalarySlip(ISalary salary,ITaxStructure taxStructure) => new SalarySlip(salary, taxStructure);

        private decimal CalculateIncomeTax()
        {
            const decimal incomeTax = 0.00M;

            var taxSlab = _taxStructure
                            .TaxRate
                            .TaxSlab
                            .FirstOrDefault(x => x.MinIncome >= _salary.AnnualSalary && 
                                                x.MaxIncome <= _salary.AnnualSalary);
            // check if tax slab is null and throw an exception
            if (taxSlab == null) throw new NullReferenceException("Tax Slab is null");
            // no tax
            if (taxSlab.Tax == null)
                return incomeTax;
            //get tax structure
            var tax = taxSlab.Tax;
            
            var annualTaxPartial = ((_salary.AnnualSalary - taxSlab.MinIncome) * (tax.AdditionalTaxPercentageOverMinIncome.Value));
            // calculate annual tax
            var annualTax = tax.FlatTax == null ? annualTaxPartial  : tax.FlatTax + annualTaxPartial;
            // calculate monthly tax
            var monthlyTax =  Math.Round(annualTax.Value/12, 0, MidpointRounding.AwayFromZero);

            return (decimal) monthlyTax;
        }

    }


}