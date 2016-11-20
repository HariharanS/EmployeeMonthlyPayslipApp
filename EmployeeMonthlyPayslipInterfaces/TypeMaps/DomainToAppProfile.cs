using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeMonthlyPayslipApp.Interfaces;
using EmployeeMonthlyPayslipApp.Models.Models;
using EmployeePayDetailsDomain.Interfaces;
using EmployeePayDetailsDomain.Models;

namespace EmployeePayDetailsCommon.TypeMaps
{
    public class DomainToAppProfile : Profile
    {
        public DomainToAppProfile()
        {
            // App to Domain
            CreateMap<IEmployeeDetails, EmployeeDetails>().ReverseMap();
            CreateMap<IEmployee, Employee>().ReverseMap();
            CreateMap<IPerson, Person>().ReverseMap();
            CreateMap<ISalary, Salary>().ReverseMap();
            CreateMap<IEmployeeDetails, IEmployee>()
                .IncludeBase<IEmployeeDetails, IPerson>().ReverseMap();
            CreateMap<IEmployeeDetails, ISalary>().ReverseMap();

            // Domain to App
            CreateMap<ISalarySlip, SalarySlip>().ReverseMap();
            CreateMap<IPaySlipDetails, PaySlipDetails>().ReverseMap();
            CreateMap<ISalarySlip, IPaySlipDetails>().ReverseMap();
            CreateMap<IEmployee, IPaySlipDetails>()
                .ForMember(dest => dest.FullName, opt=> opt.MapFrom(src=> src.FirstName.Trim() + " " + src.LastName.Trim()))
                .ReverseMap()
                ;

        }
    }
}
