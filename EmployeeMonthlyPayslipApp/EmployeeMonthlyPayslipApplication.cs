using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeMonthlyPayslipApp.Interfaces;
using EmployeeMonthlyPayslipApp.Interfaces.TaxStructure;
using EmployeeMonthlyPayslipApp.Models;
using EmployeeMonthlyPayslipApp.Models.Models;
using EmployeeMonthlyPayslipApp.Models.Models.TaxStructure;
using EmployeeMonthlyPayslipInterfaces.TypeMaps;
using Fclp;
using Fclp.Internals.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace EmployeeMonthlyPayslipApp
{
    class EmployeeMonthlyPayslipApplication
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ITaxStructure _taxStructure;
        private EmployeeDetailsInput _employeeDetailsInput;
        private CSVParameters _csvParameters;
        public EmployeeMonthlyPayslipApplication(string[] commandLineArgs)
        {
            
            ICommandLineParserResult parseResult;
            var inputArguments = SetupFluentCommandLineParser(commandLineArgs, out parseResult);
            _logger = LogSetup();
            _mapper = InitializeTypeMapper();
            ExtractCommandLineParametersIntoObject(parseResult, inputArguments);
            
            _taxStructure = LoadTaxStructure();
        }

        public IPaySlipDetails RunApplication()
        {
            _logger.Information(
                "Employee details input : FirstName: {0}, Last Name: {1}, Annual Salary: {2}, Super rate (%): {3}, Payment Period: {4}",
                _employeeDetailsInput.FirstName, _employeeDetailsInput.LastName, _employeeDetailsInput.AnnualSalary, _employeeDetailsInput.SuperPercentage, _employeeDetailsInput.TaxPeriod);

            var employeeDetails = _mapper.Map<EmployeeDetailsInput, IEmployeeDetails>(_employeeDetailsInput);

            var employeePayDetailsService = new EmployeePayDetailsService.EmployeePayDetailsService(employeeDetails, _mapper);
            var paySlip = employeePayDetailsService.GetPaySlip(_taxStructure);

            _logger.Information(
                "Employee monthly Pay Slip : FullName: {0},Pay Period: {1}, Gross Income: {2}, Income Tax: {3}, Net Income: {4}, Super: {5}",
                paySlip.FullName, paySlip.TaxPeriod, paySlip.GrossIncome, paySlip.IncomeTax, paySlip.NetIncome,
                paySlip.Super);
            return paySlip;
        }

        private void ExtractCommandLineParametersIntoObject(ICommandLineParserResult parseResult,
            IFluentCommandLineParser<CommandLineInputParameters> inputArguments)
        {
            if (parseResult.HasErrors)
            {
                _logger.Error(parseResult.ErrorText);
            }
            var commandLineInputParameters = inputArguments.Object;
            if (commandLineInputParameters.IsInputInCSVFormat)
            {
                _csvParameters = _mapper.Map<CSVParameters>(commandLineInputParameters);
            }
            _employeeDetailsInput = _mapper.Map<EmployeeDetailsInput>(commandLineInputParameters); ;
        }

        private static ILogger LogSetup()
        {
            var logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}")
                .CreateLogger();

            return logger;
        }

        private static FluentCommandLineParser<CommandLineInputParameters> SetupFluentCommandLineParser(string[] args,
            out ICommandLineParserResult parseResult)
        {
            var inputArguments = new FluentCommandLineParser<CommandLineInputParameters>();

            inputArguments.Setup(arg => arg.FirstName)
                .As('f', "firstname");
            inputArguments.Setup(arg => arg.LastName)
                .As('l', "lastname");
            inputArguments.Setup(arg => arg.AnnualSalary)
                .As('a', "annualsalary");
            inputArguments.Setup(arg => arg.SuperPercentage)
                .As('s', "super")
                .WithDescription("Enter super rate as a percentage for e.g. 9%");
            inputArguments.Setup(arg => arg.TaxPeriod)
                .As('p', "taxperiod")
                .WithDescription("Enter monthly tax period that should appear on the pay slip. ");

            inputArguments.Setup(arg => arg.TaxPeriod)
                .As("IsInputCSV")
                .WithDescription("If the input is a csv file set this to true, if the put is a command line input set this to false ");

            inputArguments.Setup(arg => arg.InputCSVFilePath)
                .As('i',"inputfile");

            inputArguments.Setup(arg => arg.OutputCSVDirectory)
                .As('o', "outputdirectory");

            inputArguments.SetupHelp("h", "help", "?")
                .Callback(() => Console.WriteLine());

            parseResult = inputArguments.Parse(args);
            return inputArguments;
        }

        private static IMapper InitializeTypeMapper()
        {
            return TypeMapper.InitilizeTypeConfiguration().InitializeMapper();
        }

        private static ITaxStructure LoadTaxStructure()
        {

            var path = AppDomain.CurrentDomain.BaseDirectory;

            const string taxJsonFileName = "TaxRate.json";
            var taxJsonFilePath = Path.Combine(path, taxJsonFileName);
            string fileContent;
            using (var reader = new StreamReader(taxJsonFilePath))
            {
                fileContent = reader.ReadToEnd().Trim();
            }
            var taxStructureRoot = JsonConvert.DeserializeObject<TaxStructureRoot>(fileContent, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            taxStructureRoot.taxStructure.TaxRate.TaxSlab.FirstOrDefault(x => x.MaxIncome == decimal.Zero).MaxIncome =
                decimal.MaxValue;
            return taxStructureRoot.taxStructure;
        }
    }
}
