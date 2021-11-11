using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Data.DTOs.MpesaModule
{
    public class MpesaExpressPaymentRequestDTO
    {
        public string TransactionType { get; set; }
        public string TransactionDesc { get; set; }

        public string TransactionId { get; set; }

        public string TransactionTime { get; set; }

        public decimal TransactionAmount { get; set; }

        public string BusinessShortCode { get; set; }

        public string BillReferenceNumber { get; set; }

        public string InvoiceNumber { get; set; }

        public decimal OrganisationAccountBalance { get; set; }

        public string ThirdPartyTransactionId { get; set; }

        public string MSISDN { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public Nullable<byte> Status { get; set; }

        public string Password { get; set; }

        public string Timestamp { get; set; }

        public decimal Amount { get; set; }

        public string PartyA { get; set; }

        public string PartyB { get; set; }

        public string PhoneNumber { get; set; }

        public string CallBackURL { get; set; }

        public string AccountReference { get; set; }

        public string TransactionDescription { get; set; }

        public string MerchantRequestId { get; set; }

        public string CheckoutRequestId { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseDescription { get; set; }

        public string ResultCode { get; set; }

        public string ResultDescrption { get; set; }
    }
}