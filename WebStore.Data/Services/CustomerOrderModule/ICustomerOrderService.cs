using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CustomerOrderModule;

namespace WebStore.Data.Services.CustomerOrderModule
{
     public interface ICustomerOrderService
    {
        Task<CustomerOrderDTO> Create(CustomerOrderDTO customerOrderDTO);
        Task<CustomerOrderDTO> Update(CustomerOrderDTO customerOrderDTO);
        Task<bool> Delete(Guid Id);
        Task<List<CustomerOrderDTO>> GetAll();
        Task<CustomerOrderDTO> GetById(Guid Id);
        Task<CustomerOrderDTO> GetByOrderNumber(string OrderNumber);
    }
}