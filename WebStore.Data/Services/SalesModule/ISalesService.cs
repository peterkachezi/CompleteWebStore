using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Data.DTOs.SalesDetailsModule;
using WebStore.Data.DTOs.SalesModule;

namespace WebStore.Data.Services.SalesModule
{
    public interface ISalesService
    {
        Task<SalesDTO> AddSalesOrder(SalesDTO salesOrederDTO);
        Task<List<SalesDTO>> GetCustomerOrders();
        Task<List<SalesDetailsDTO>> GetSalesOrdersDetails();
        Task<List<SalesDetailsDTO>> GetSalesOrdersDetailsByOrderId(Guid Id);
        Task<List<SalesDetailsDTO>> GetSalesOrdersDetailsByOrderNumber(string OrderNumber);
        Task<SalesDetailsDTO> GetSalesOrdersDetailsById(Guid Id);

    }
}