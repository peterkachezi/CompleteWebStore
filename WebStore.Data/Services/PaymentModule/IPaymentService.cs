using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Data.DTOs.MpesaModule;
using WebStore.Data.DTOs.PaymentModule;

namespace WebStore.Data.Services.PaymentModule
{
    public interface IPaymentService
    {
        Task<MpesaExpressResponseDTO> SaveResponse(MpesaExpressResponseDTO mpesaExpressResponseDTO);
        Task<List<PaymentDTO>> GetAll();
        Task<PaymentDTO> GetByTransId(string TransactionId);
        Task<PaymentDTO> SaveCallBackAsync(PaymentDTO paymentDTO);
    }
}