using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.ViewModels
{
    public class ExceptionViewModel
    {

        public string ExceptionMessage { get; set; }
        public string ExceptionPath { get; set; }
        public string StackTrace { get; set; }

    }
}
