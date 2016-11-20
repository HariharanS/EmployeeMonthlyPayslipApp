using AutoMapper;
using EmployeeMonthlyPayslipApp.Interfaces;
using EmployeeMonthlyPayslipApp.Interfaces.TaxStructure;
using EmployeePayDetailsCommon.TypeMaps;
using EmployeePayDetailsDomain.Interfaces;
using EmployeePayDetailsDomain.Models;

namespace EmployeePayDetailsService
{
    public class EmployeePayDetailsService
    {
        private readonly IEmployeeDetails _employeeDetails;
        private readonly IMapper _mapper;

        public EmployeePayDetailsService(IEmployeeDetails employeeDetails,IMapper mapper)
        {
            _employeeDetails = employeeDetails;
            _mapper = mapper;
        }

        public IPaySlipDetails GetPaySlip(ITaxStructure taxStructure)
        {
            var employee = _mapper.Map<IEmployeeDetails, IEmployee>(_employeeDetails);
            var salarySlip = SalarySlip.CreateSalarySlip(employee.Salary, taxStructure);
            salarySlip.TaxPeriod = _employeeDetails.TaxPeriod;
            employee.SalarySlips.Add(salarySlip);
            return _mapper.Map<IEmployee, IPaySlipDetails>(employee);
        }
    }
}
