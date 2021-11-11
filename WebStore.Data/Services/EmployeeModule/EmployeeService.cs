using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.EmployeeModule;
using WebStore.Data.EDMX;

namespace WebStore.Data.Services.EmployeeModule
{
    public class EmployeeService : IEmployeeService
    {
        public List<EmployeeDTO> GetAllEmployees()
        {
            using (var db = new WinkadStoreEntities())
            {
                var employee = db.Employees.ToList();

                var employees = new List<EmployeeDTO>();

                foreach (var item in employee)
                {
                    var data = new EmployeeDTO
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Position = item.Position,
                        Location = item.Location,
                        Age = item.Age,
                        CreateDate = item.CreateDate,
                        StartDate = item.StartDate,
                        Department = item.Department,
                        Salary = item.Salary,
                        StartDateString = item.StartDateString,

                    };

                    employees.Add(data);
                }
                return employees;
            }
        }
    }
}
