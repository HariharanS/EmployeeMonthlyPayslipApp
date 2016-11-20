﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using EmployeeMonthlyPayslipApp.Interfaces;
using EmployeeMonthlyPayslipApp.Models.Models;

namespace EmployeeMonthlyPayslipInterfaces.TypeMaps
{
    public class AppModelProfile : Profile
    {
        public AppModelProfile()
        {
            CreateMap<IEmployeeDetails, EmployeeDetails>();
            CreateMap<EmployeeDetailsInput, IEmployeeDetails>()
                    .ForMember(dest => dest.SuperRate, opt => opt.ResolveUsing<SuperRateResolver>());
        }
    }

    public class SuperRateResolver : IValueResolver<EmployeeDetailsInput, IEmployeeDetails, decimal>
    {
        private const string NUMBERWITHPERCENTAGEVALIDATIONREGEX = @"^\d+[.]?\d*%?$";
        public decimal Resolve(EmployeeDetailsInput source, IEmployeeDetails destination, decimal destMember, ResolutionContext context)
        {
            if (source.SuperPercentage == null)
            {
                Console.WriteLine("Super Percentage is null. Setting Super percentage to zero.");
                return decimal.Zero;
            }
            if (!Regex.IsMatch(source.SuperPercentage, NUMBERWITHPERCENTAGEVALIDATIONREGEX))
            {
                Console.WriteLine("SuperPercentage {0} supplied is not in valid format. Setting Super percentage to zero.", source.SuperPercentage);
                return decimal.Zero;
            }
            decimal super;
            if (!decimal.TryParse(source.SuperPercentage.Substring(0, source.SuperPercentage.Length - 2), out super))
            {
                Console.WriteLine("SuperPercentage {0} supplied is not in valid format. It cannot be parsed into a valid decimal number. Setting Super percentage to zero.", source.SuperPercentage);
                return decimal.Zero;
            }

            return super;
        }
    }
}