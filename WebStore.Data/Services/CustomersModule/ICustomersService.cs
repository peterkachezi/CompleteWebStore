using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CustomerModule;

namespace WebStore.Data.Services.CustomersModule
{
    public interface ICustomersService
    {
        Task<CustomerDTO> AddCustomer(CustomerDTO customerDTO);
        Task<CustomerDTO> Update(CustomerDTO customerDTO);
        Task<bool> DeleteCustomer(Guid Id);
        Task<List<CustomerDTO>> GetAllCustomers();
        Task<CustomerDTO> GetCustomerById(Guid Id);
    }
}
