using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeMonthlyPayslipApp.Interfaces;
using EmployeeMonthlyPayslipInterfaces;
using Fclp;
using EmployeeMonthlyPayslipApp.Models.Models;
using EmployeeMonthlyPayslipInterfaces.TypeMaps;

namespace EmployeeMonthlyPayslipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //get input
            var inputArguments = new FluentCommandLineParser<EmployeeDetailsInput>();
            
            inputArguments.Setup(arg => arg.FirstName)
                .As('f', "firstname")
                .Required();
            inputArguments.Setup(arg => arg.LastName)
                .As('l', "lastname")
                .Required();
            inputArguments.Setup(arg => arg.AnnualSalary)
                .As('a', "annualsalary")
                .Required();
            inputArguments.Setup(arg => arg.SuperPercentage)
                .As('s', "super")
                .WithDescription("Enter super rate as a percentage for e.g. 9%")
                .Required();
            inputArguments.Setup(arg => arg.TaxPeriod)
                .As('p', "taxperiod")
                .WithDescription("Enter monthly tax period that should appear on the pay slip. ")
                .Required();

            inputArguments.SetupHelp("h", "help", "?")
                .Callback(()=> Console.WriteLine());

            var parseResult = inputArguments.Parse(args);

            var applicationResult = parseResult.HasErrors ? LogErrorToConsole(parseResult) : RunApplication(inputArguments.Object);
        }

        private static object RunApplication(EmployeeDetailsInput employeeDetailsInput)
        {
            var mapper = InitializeTypeMapper();
            var employeeDetails = mapper.Map<EmployeeDetailsInput, IEmployeeDetails>(employeeDetailsInput);
            return null;
        }

        private static IMapper InitializeTypeMapper()
        {
            var typeMapConfiguration = TypeMapConfiguration.Initilize();
            typeMapConfiguration.AssertConfigurationIsValid();
            return typeMapConfiguration.CreateMapper();
        }

        private static object LogErrorToConsole(ICommandLineParserResult parseResult)
        {
            throw new NotImplementedException();
        }
    }
}
