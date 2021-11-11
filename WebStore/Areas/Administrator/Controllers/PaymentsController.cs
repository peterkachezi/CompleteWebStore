
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebStore.Data.DTOs.MpesaModule;
using WebStore.Data.DTOs.PaymentModule;
using WebStore.Data.Services.PaymentModule;

namespace WebStore.Areas.Administrator.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IPaymentService paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }
        public ActionResult Index()
        {

            return View();

        }
        public ActionResult TestStk()
        {

            return View();

        }
        public ActionResult STKSuccess()
        {

            return View();

        }
        public async Task<ActionResult> GetAllPayments()
        {
            var payment = await paymentService.GetAll();

            return Json(new { data = payment }, JsonRequestBehavior.AllowGet);

        }
        public static string GeneratePassword()
        {
            try
            {
                var lipa_time = DateTime.Now.ToString("yyyyMMddHHmmss");

                var passkey = "c308930e0ea0919259e32eeaf0d04f10e79d26954f52eced2436c8a7ff2ed0fc";

                int BusinessShortCode = 328108;

                var timestamp = lipa_time;

                var passwordBytes = System.Text.Encoding.UTF8.GetBytes(BusinessShortCode + passkey + timestamp);

                var password = Convert.ToBase64String(passwordBytes);

                return password;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<string> generateAccessToken(string key, string secrete)
        {
            try
            {
                var url = @"https://api.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials";

                HttpClient client = new HttpClient();

                var byteArray = System.Text.Encoding.ASCII.GetBytes(key + ":" + secrete);

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

                HttpResponseMessage response = await client.GetAsync(url);

                HttpContent content = response.Content;

                string result = await content.ReadAsStringAsync();

                var mpesaAccessToken = JsonConvert.DeserializeObject<MpesaAccessTokenDTO>(result);

                return mpesaAccessToken.access_token;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> MpesaPayment(string PhoneNumber)
        {
            try
            {
                //var msisdn = formatPhoneNumber(PhoneNumber);

                string url = @"https://api.safaricom.co.ke/mpesa/stkpush/v1/processrequest";

                HttpClient client = new HttpClient();

                var key = "QQGUDGoGV9WoXMbTzxPRMnguUkL8SqG5";

                var secrete = "0HkuMBkfO00JIhF2";

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + await generateAccessToken(key, secrete));



                var mpesaExpressRequestDTO = new MpesaExpressRequestDTO
                {
                    BusinessShortCode = 328108,
                    Password = GeneratePassword(),
                    Timestamp = DateTime.Now.ToString("yyyyMMddHHmmss"),
                    TransactionType = "CustomerPayBillOnline",
                    Amount = 1,
                    PartyA = PhoneNumber,
                    PartyB = 328108,
                    PhoneNumber = PhoneNumber,
                    CallBackURL = "https://winkadfreshdropsbottledwater.co.ke/Home/SaveCallBack",
                    AccountReference = "ref",
                    TransactionDesc = "Testing stk push on api"

                };

                HttpResponseMessage result = await client.PostAsJsonAsync(url, mpesaExpressRequestDTO);

                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadAsStringAsync();

                var mpesaExpressResponse = JsonConvert.DeserializeObject<MpesaExpressResponseDTO>(response);

                var mpesaResponse = new MpesaExpressResponseDTO
                {
                    MerchantRequestID = mpesaExpressResponse.MerchantRequestID,
                    CheckoutRequestID = mpesaExpressResponse.CheckoutRequestID,
                    ResponseCode = mpesaExpressResponse.ResponseCode,
                    ResponseDescription = mpesaExpressResponse.ResponseDescription,
                    CustomerMessage = mpesaExpressResponse.CustomerMessage,
                };

                var h = await paymentService.SaveResponse(mpesaResponse);

                return View("STKSuccess");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public ActionResult ValidatePayment()
        {
            return View();
        }
        public async Task<ActionResult> GetPayment(string TransId)
        {
            try
            {
                var TransactionId = TransId.ToUpper();

                var data = await paymentService.GetByTransId(TransactionId);

                if (data != null)
                {
                    PaymentDTO file = new PaymentDTO
                    {
                        CheckoutRequestID = data.CheckoutRequestID,

                        MerchantRequestID = data.MerchantRequestID,

                        ResultCode = data.ResultCode,

                        ResultDesc = data.ResultDesc,

                        Amount = data.Amount,

                        TransactionNumber = data.TransactionNumber,

                        TransactionDate = data.TransactionDate,

                        PhoneNumber = data.PhoneNumber,

                        FirstName = data.FirstName,

                        LastName = data.LastName,

                    };

                    return Json(new { data = file }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { data = false }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}