using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CustomerModule;
using WebStore.Data.DTOs.SMSModule;

namespace WebStore.Data.Services.SMSModule
{
   public interface ISMSService
    {
        Task<bool> SaveResponse(SMSDTO sMSDTO);
        Task<bool> SendCustomerSMS(CustomerDTO customerDTO);
    }
}
