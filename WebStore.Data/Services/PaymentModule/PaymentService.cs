using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.MpesaModule;
using WebStore.Data.DTOs.PaymentModule;
using WebStore.Data.EDMX;

namespace WebStore.Data.Services.PaymentModule
{
    public class PaymentService : IPaymentService
    {
        private readonly WinkadStoreEntities context;
        public PaymentService(WinkadStoreEntities context)
        {
            this.context = context;
        }
        public async Task<List<PaymentDTO>> GetAll()
        {
            try
            {
                var payment = await context.Payments.ToListAsync();

                var payments = new List<PaymentDTO>();

                foreach (var item in payment)
                {
                    var data = new PaymentDTO
                    {
                        Id = item.Id,

                        MerchantRequestID = item.MerchantRequestID,

                        CheckoutRequestID = item.CheckoutRequestID,

                        ResultCode = item.ResultCode,

                        ResultDesc = item.ResultDesc,

                        Amount = item.Amount,

                        TransactionNumber = item.TransactionNumber,

                        Balance = item.Balance,

                        TransactionDate = item.TransactionDate,

                        PhoneNumber = item.PhoneNumber,

                        FirstName = item.FirstName,

                        LastName = item.LastName,

                    };
                    payments.Add(data);

                }

                return payments;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<PaymentDTO> GetByTransId(string TransactionId)
        {
            try
            {
                var payment = (await context.Payments.ToListAsync()).Where(x => x.TransactionNumber == TransactionId).FirstOrDefault();

                if (payment != null)
                {
                    return new PaymentDTO
                    {
                        Id = payment.Id,

                        MerchantRequestID = payment.MerchantRequestID,

                        CheckoutRequestID = payment.CheckoutRequestID,

                        ResultCode = payment.ResultCode,

                        ResultDesc = payment.ResultDesc,

                        Amount = payment.Amount,

                        TransactionNumber = payment.TransactionNumber,

                        Balance = payment.Balance,

                        TransactionDate = payment.TransactionDate,

                        PhoneNumber = payment.PhoneNumber,

                        FirstName = payment.FirstName,

                        LastName = payment.LastName,

                    };
                                     
                }
                return null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<MpesaExpressResponseDTO> SaveResponse(MpesaExpressResponseDTO mpesaExpressResponseDTO)
        {

            try
            {
                var s = new MpesaExpressRespons
                {
                    MerchantRequestID = mpesaExpressResponseDTO.MerchantRequestID,

                    CheckoutRequestID = mpesaExpressResponseDTO.CheckoutRequestID,

                    ResponseCode = mpesaExpressResponseDTO.ResponseCode,

                    ResponseDescription = mpesaExpressResponseDTO.ResponseDescription,

                    CustomerMessage = mpesaExpressResponseDTO.CustomerMessage,
                };

                context.MpesaExpressResponses.Add(s);

                await context.SaveChangesAsync();

                return mpesaExpressResponseDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }


        }
        public async Task<PaymentDTO> SaveCallBackAsync(PaymentDTO paymentDTO)
        {

            try
            {

                var s = new Payment

                {
                    CheckoutRequestID = paymentDTO.CheckoutRequestID,

                    MerchantRequestID = paymentDTO.MerchantRequestID,

                    ResultCode = paymentDTO.ResultCode,

                    ResultDesc = paymentDTO.ResultDesc,

                    Amount = paymentDTO.Amount,

                    TransactionNumber = paymentDTO.TransactionNumber,

                    TransactionDate = paymentDTO.TransactionDate,

                    PhoneNumber = paymentDTO.PhoneNumber,

                    FirstName = paymentDTO.FirstName,

                    LastName = paymentDTO.LastName,

                };

                context.Payments.Add(s);

                await context.SaveChangesAsync();

                return paymentDTO;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
