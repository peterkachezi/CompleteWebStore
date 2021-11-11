using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.CustomerOrderModule
{
    public class CustomerOrderDTO
    {
        public System.Guid Id { get; set; }
        public System.Guid CustomerId { get; set; }
        public System.Guid ProductId { get; set; }
        public string OrderNumber { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string OrderNotes { get; set; }
        public int Quantity { get; set; }
        public string FullName => FirstName + "  " + MiddleName + "  " + LastName;
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AttachmentFileName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string CustomerNumber { get; set; }
        public string NewCreateDate { get { return OrderDate.ToShortDateString(); } }
        public string StartDateString { get; set; }
        public string DeliveryLocation { get; set; }
        public string Apartment { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
