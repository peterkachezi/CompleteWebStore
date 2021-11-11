
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStore.Data.DTOs.CustomerModule;
using WebStore.Data.DTOs.CustomerOrderModule;
using WebStore.Data.DTOs.MpesaModule;
using WebStore.Data.DTOs.PaymentModule;
using WebStore.Data.Services.CustomerOrderModule;
using WebStore.Data.Services.MpesaModule;
using WebStore.Data.Services.PaymentModule;
using WebStore.Data.Services.Services.ProductModule;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMpesaService mpesaService;

        private readonly IPaymentService paymentService;

        private readonly IProductService productService;

        private readonly ICustomerOrderService customerOrderService;
        public HomeController(ICustomerOrderService customerOrderService, IMpesaService mpesaService, IPaymentService paymentService, IProductService productService)
        {
            this.mpesaService = mpesaService;

            this.paymentService = paymentService;

            this.productService = productService;

            this.customerOrderService = customerOrderService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Product()
        {
            ViewBag.Product = await productService.GetAll();

            return View();
        }

        public async Task<ActionResult> Checkout(CustomerOrderDTO customerOrderDTO)
        {
            try
            {
                var results = await customerOrderService.Create(customerOrderDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Record  has been  successfully updated!" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update the record  !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveCallBack(DarajaResponseAfterUserEntersPin darajaResponse)
        {
            try
            {
                if (darajaResponse.Body.stkCallback.ResultCode == 0)
                {
                    var s = new PaymentDTO
                    {
                        CheckoutRequestID = darajaResponse.Body.stkCallback.CheckoutRequestID,

                        MerchantRequestID = darajaResponse.Body.stkCallback.MerchantRequestID,

                        ResultCode = darajaResponse.Body.stkCallback.ResultCode,

                        ResultDesc = darajaResponse.Body.stkCallback.ResultDesc,

                        Amount = Convert.ToDecimal(darajaResponse.Body.stkCallback.CallbackMetadata.Item.Where(p => p.Name.Contains("Amount")).FirstOrDefault().Value.ToString()),

                        TransactionNumber = darajaResponse.Body.stkCallback.CallbackMetadata.Item.Where(p => p.Name.Contains("MpesaReceiptNumber")).FirstOrDefault().Value.ToString(),

                        TransactionDate = darajaResponse.Body.stkCallback.CallbackMetadata.Item.Where(p => p.Name.Contains("TransactionDate")).FirstOrDefault().Value.ToString(),

                        PhoneNumber = darajaResponse.Body.stkCallback.CallbackMetadata.Item.Where(p => p.Name.Contains("PhoneNumber")).FirstOrDefault().Value.ToString(),

                        FirstName = darajaResponse.Body.stkCallback.CallbackMetadata.Item.Where(p => p.Name.Contains("FirstName")).FirstOrDefault().Value.ToString(),

                        LastName = darajaResponse.Body.stkCallback.CallbackMetadata.Item.Where(p => p.Name.Contains("LastName")).FirstOrDefault().Value.ToString(),

                    };

                    var result = await paymentService.SaveCallBackAsync(s);

                }
                return Json(darajaResponse);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

    }
}