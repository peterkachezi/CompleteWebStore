using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.EmployeeModule;


namespace WebStore.Data.Services.EmployeeModule
{
    public interface IEmployeeService
    {
        List<EmployeeDTO> GetAllEmployees();
    }
}
