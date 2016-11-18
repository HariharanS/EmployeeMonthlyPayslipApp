using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeMonthlyPayslipInterfaces.Domain;

namespace EmployeePayDetailsDomain.Models
{
    public class Person : IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
