using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.PaymentModeModule;

namespace WebStore.Data.Services.PaymentModeModule
{
  public  interface IPaymentModeService
    {
        Task<bool> AddPaymentMode(PaymentModeDTO paymentModeDTO);
        Task<bool> EditPaymentMode(Guid Id,PaymentModeDTO paymentModeDTO);
        Task<bool> DeletePaymentMode(Guid Id);
        Task<PaymentModeDTO> GetPaymentModeById(Guid Id);
        Task<List<PaymentModeDTO>> GetAllPaymentMode();
    }
}
