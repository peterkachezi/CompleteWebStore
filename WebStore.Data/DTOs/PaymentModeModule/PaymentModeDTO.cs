using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.PaymentModeModule
{
  public  class PaymentModeDTO
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
