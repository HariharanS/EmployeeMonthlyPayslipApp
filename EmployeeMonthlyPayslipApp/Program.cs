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

namespace EmployeeMonthlyPayslipApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //get input
            RunEmployeeMonthlyPayslipApplication(args);
        }

        public static void RunEmployeeMonthlyPayslipApplication(string[] args)
        {
            ICommandLineParserResult parseResult;
            var inputArguments = SetupFluentCommandLineParser(args, out parseResult);

            var applicationResult = parseResult.HasErrors
                ? LogErrorToConsole(parseResult)
                : RunApplication(inputArguments.Object);
        }

        private static FluentCommandLineParser<EmployeeDetailsInput> SetupFluentCommandLineParser(string[] args,
            out ICommandLineParserResult parseResult)
        {
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
                .Callback(() => Console.WriteLine());

            parseResult = inputArguments.Parse(args);
            return inputArguments;
        }

        private static IPaySlipDetails RunApplication(EmployeeDetailsInput employeeDetailsInput)
        {
            Console.WriteLine(
                "Employee details input : FirstName: {0}, Last Name: {1}, Annual Salary: {2}, Super rate (%): {3}, Payment Period: {4}",
                employeeDetailsInput.FirstName, employeeDetailsInput.LastName, employeeDetailsInput.AnnualSalary, employeeDetailsInput.SuperPercentage, employeeDetailsInput.TaxPeriod);
            var taxStructure = GetTaxStructure();
            var mapper = InitializeTypeMapper();
            
            var employeeDetails = mapper.Map<EmployeeDetailsInput, IEmployeeDetails>(employeeDetailsInput);
            
            var employeePayDetailsService = new EmployeePayDetailsService.EmployeePayDetailsService(employeeDetails,mapper);
            var paySlip = employeePayDetailsService.GetPaySlip(taxStructure);

            Console.WriteLine(
                "Employee monthly Pay Slip : FullName: {0},Pay Period: {1}, Gross Income: {2}, Income Tax: {3}, Net Income: {4}, Super: {5}",
                paySlip.FullName, paySlip.TaxPeriod, paySlip.GrossIncome, paySlip.IncomeTax, paySlip.NetIncome,
                paySlip.Super);
            return paySlip;
        }

        private static ITaxStructure GetTaxStructure()
        {
            
            var path = AppDomain.CurrentDomain.BaseDirectory;

            const string taxJsonFileName = "TaxRate.json";
            var taxJsonFilePath = Path.Combine(path, taxJsonFileName);
            string fileContent;
            using (var reader = new StreamReader(taxJsonFilePath))
            {
                fileContent = reader.ReadToEnd().Trim();
            }
            var taxStructureRoot = JsonConvert.DeserializeObject<TaxStructureRoot>(fileContent,new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            taxStructureRoot.taxStructure.TaxRate.TaxSlab.FirstOrDefault(x => x.MaxIncome == decimal.Zero).MaxIncome =
                decimal.MaxValue;
            return taxStructureRoot.taxStructure;
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
