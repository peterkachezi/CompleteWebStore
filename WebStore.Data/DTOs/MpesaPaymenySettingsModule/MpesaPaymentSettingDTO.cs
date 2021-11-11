using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.MpesaPaymenySettingsModule
{
   public class MpesaPaymentSettingDTO
    {
        public System.Guid Id { get; set; }
        public string MpesaKey { get; set; }
        public string Secrete { get; set; }
        public string BusinessShortCode { get; set; }
        public string passkey { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
