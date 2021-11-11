using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Data.DTOs.MpesaModule
{
    public class MpesaExpressQueryRequestDTO
    {
        public string BusinessShortCode { get; set; }

        public string Password { get; set; }

        public string Timestamp { get; set; }

        public string CheckoutRequestID { get; set; }
    }
}