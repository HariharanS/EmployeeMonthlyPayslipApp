using System.Text.RegularExpressions;
using AutoMapper;
using EmployeeMonthlyPayslipApp.Models;
using EmployeeMonthlyPayslipInterfaces;

namespace EmployeeMonthlyPayslipApp.ObjectMaps
{
    public class MapConfiguration
    {
        public MapConfiguration()
        {
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDetailsInput, IEmployeeDetails>()
                    .ForMember(dest=> dest.SuperRate, opt=> opt.ResolveUsing<SuperRateResolver>());
            });
        }
    }

    public class SuperRateResolver : IValueResolver<EmployeeDetailsInput, IEmployeeDetails, decimal>
    {
        private const string NUMBERWITHPERCENTAGEVALIDATIONREGEX = @"^\d+[.]?\d*%?$";
        public decimal Resolve(EmployeeDetailsInput source, IEmployeeDetails destination, decimal destMember, ResolutionContext context)
        {
            if (source.SuperPercentage == null) return 0.00M;
            if(!Regex.IsMatch(source.SuperPercentage, NUMBERWITHPERCENTAGEVALIDATIONREGEX)) return 0.00M;


        }
    }
}
