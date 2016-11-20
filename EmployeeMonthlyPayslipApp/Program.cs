using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeMonthlyPayslipApp.Interfaces;
using EmployeeMonthlyPayslipApp.Interfaces.TaxStructure;
using EmployeeMonthlyPayslipInterfaces;
using Fclp;
using EmployeeMonthlyPayslipApp.Models.Models;
using EmployeeMonthlyPayslipApp.Models.Models.TaxStructure;
using EmployeeMonthlyPayslipInterfaces.TypeMaps;
using EmployeePayDetailsCommon.TypeMaps;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Core;

namespace EmployeeMonthlyPayslipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var employeeMonthlyPayslipApplication = new EmployeeMonthlyPayslipApplication(args);
            employeeMonthlyPayslipApplication.RunApplication();
        }
    }
}
