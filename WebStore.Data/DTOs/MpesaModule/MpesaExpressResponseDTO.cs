using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Data.DTOs.MpesaModule
{
    public class MpesaExpressResponseDTO
    {
        public string MerchantRequestID { get; set; }

        public string CheckoutRequestID { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseDescription { get; set; }

        public string CustomerMessage { get; set; }
    }
}