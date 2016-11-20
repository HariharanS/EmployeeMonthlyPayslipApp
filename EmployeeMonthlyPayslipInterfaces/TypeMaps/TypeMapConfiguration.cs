using System;
using System.Text.RegularExpressions;
using AutoMapper;
using EmployeeMonthlyPayslipApp.Interfaces;
using EmployeeMonthlyPayslipApp.Models.Models;

namespace EmployeeMonthlyPayslipInterfaces.TypeMaps
{
    public class TypeMapConfiguration
    {
        private static readonly IConfigurationProvider _autoMapperConfiguration;
        static TypeMapConfiguration()
        {
            _autoMapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AppModelProfile>();
            });
        }

        public static IConfigurationProvider Initilize()
        {
            new TypeMapConfiguration();
            return _autoMapperConfiguration;
        }
    }
}
