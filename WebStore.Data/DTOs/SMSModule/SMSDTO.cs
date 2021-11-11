using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.SMSModule
{
  public  class SMSDTO
    {

        public string Number { get; set; }
        public string Status { get; set; }
        public string MessageId { get; set; }
        public string Cost { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
