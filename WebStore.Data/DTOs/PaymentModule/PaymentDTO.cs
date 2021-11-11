using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.PaymentModule
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public string MerchantRequestID { get; set; }
        public string CheckoutRequestID { get; set; }
        public int? ResultCode { get; set; }
        public string ResultDesc { get; set; }
        public decimal? Amount { get; set; }
        public string MpesaReceiptNumber { get; set; }
        public decimal? Balance { get; set; }
        public string TransactionDate { get; set; }

        public string NewTransactionDate { get { return TransactionDate.ToString(); } }

        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string TransactionNumber { get; set; }
    }
}
