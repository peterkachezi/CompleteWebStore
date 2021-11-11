//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebStore.Data.EDMX
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderPayment
    {
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> OrderId { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string MpesaReference { get; set; }
        public string PhoneNumber { get; set; }
        public string MpesaCheckoutRequestId { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<byte> PaymentMode { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string ResultCode { get; set; }
        public string ResultDescription { get; set; }
        public Nullable<decimal> TotalAmountPaidInMpesa { get; set; }
        public Nullable<decimal> InsuranceAmount { get; set; }
    }
}