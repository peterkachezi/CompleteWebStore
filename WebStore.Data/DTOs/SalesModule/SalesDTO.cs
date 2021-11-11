using System;
using System.Collections.Generic;
using WebStore.Data.DTOs.SalesDetailsModule;

namespace WebStore.Data.DTOs.SalesModule
{
    public class SalesDTO
    {
        public System.Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public string Address { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string NewOrderDate { get { return OrderDate.ToShortDateString(); } }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderNumber { get; set; }
        public decimal Change { get; set; }
        public decimal CashPaid { get; set; }
        public IEnumerable<SalesDetailsDTO> ListOfSalesDetails { get; set; }
    }
}
