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
using EmployeeMonthlyPayslipInterfaces.TypeMaps;
using EmployeePayDetailsCommon.TypeMaps;
using Newtonsoft.Json;

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
            var taxStructure = GetTaxStructure();
            var employeePayDetailsService = new EmployeePayDetailsService.EmployeePayDetailsService(employeeDetails,mapper);
            var paySlip = employeePayDetailsService.GetPaySlip(taxStructure);

            Console.WriteLine(JsonConvert.SerializeObject(paySlip));
            return null;
        }

        private static ITaxStructure GetTaxStructure()
        {
            var path = Assembly.GetExecutingAssembly().Location;

            var taxJsonFilePath = Path.Combine(path, "TaxRate.json");
            string fileContent = null;
            using (var reader = new StreamReader(taxJsonFilePath))
            {
                fileContent = reader.ReadToEnd().Trim();
            }
            var taxStructure = JsonConvert.DeserializeObject<ITaxStructure>(fileContent);
            return taxStructure;
        }

        private static IMapper InitializeTypeMapper()
        {
            return TypeMapper.InitilizeTypeConfiguration().InitializeMapper();
        }

        private static object LogErrorToConsole(ICommandLineParserResult parseResult)
        {
            throw new NotImplementedException();
        }
    }
}
