using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMonthlyPayslipInterfaces.TaxStructure;

namespace EmployeeMonthlyPayslipApp.Models.TaxStructure
{
    public class TaxStructure : ITaxStructure
    {
        public ITaxRate TaxRate { get; set; }
    }
}
