using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.SalesDetailsModule
{
    public class SalesDetailsDTO
    {
        public System.Guid Id { get; set; }
        public System.Guid OrderId { get; set; }
        public System.Guid ProductId { get; set; }
        public Guid? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string SupplierName { get; set; }
        public string ProductName { get; set; }

        public int? PaymentStatus { get; set; }
        public string PaymentStatusDescription { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal OrderTotalAmount { get; set; }
        public decimal Change { get; set; }
        public string OrderNumber { get; set; }
        public DateTime CreateDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string CreatedBy { get; set; }
        public string CreateByName { get; set; }
        public decimal CashPaid { get; set; }
        public string TaxAmount { get; set; }
                public decimal Total { get; set; }

    }
}
