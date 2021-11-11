using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Data.DTOs.EmployeeModule
{
    public class EmployeeDTO
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public string Age { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string Department { get; set; }
        public Nullable<int> Salary { get; set; }
        public string StartDateString { get; set; }
    }
}