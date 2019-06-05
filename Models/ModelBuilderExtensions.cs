﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            builder.Entity<Employee>().HasData(
                   new Employee
                   {
                       Id = 1,
                       Name = "Mark",
                       Department = Dept.IT,
                       Email = "mark@pragimtech.com"
                   }
               );
        }
    }
}
