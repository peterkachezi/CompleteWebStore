using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Reporting.WebForms;
using WebStore.Data.DTOs.SalesDetailsModule;
using WebStore.Data.DTOs.SalesModule;
using WebStore.Data.Services.CustomersModule;
using WebStore.Data.Services.PaymentModeModule;
using WebStore.Data.Services.SalesModule;
using WebStore.Data.Services.Services.ProductModule;

namespace WebStore.Areas.PointOfSale.Controllers
{
    public class SaleController : Controller
    {
        private readonly ISalesService salesService;

        private readonly ICustomersService customerService;

        private readonly IProductService productService;


        private readonly IPaymentModeService _paymentModeService;
        public SaleController(ISalesService salesService, ICustomersService customersService, IProductService productService
            , IPaymentModeService paymentModeService)
        {
            this.salesService = salesService;

            this.customerService = customersService;

            this.productService = productService;

            _paymentModeService = paymentModeService;
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.Customers = await customerService.GetAllCustomers();

            ViewBag.PaymentMode = await _paymentModeService.GetAllPaymentMode();

            var products = await productService.GetAll();

            if (products.Count == 0)
            {
                return RedirectToAction("ProductInformation");

            }


            ViewBag.Products = products;

            return View();
        }

        public ActionResult ProductInformation()
        {
            return View();
        }
        public ActionResult StockInformation()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> GetListAsync()
        {
            var saleList = await salesService.GetCustomerOrders();

            return Json(new { data = saleList }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public async Task<ActionResult> SaveTransaction(SalesDTO salesDTO)
        {

            var user = User.Identity.GetUserId();

            salesDTO.CreatedBy = user;

            salesDTO.Id = Guid.NewGuid();

            var result = await salesService.AddSalesOrder(salesDTO);


            if (result != null)
            {

                return Json(new { success = true, OrderId = salesDTO.Id, OrderNumber = salesDTO.OrderNumber, responseText = "Transaction was successfull" }, JsonRequestBehavior.AllowGet);
                //TempData["ordernumber"] = salesOrederDTO.OrderNumber;
                // return Json(new { success = false, responseText = "Transaction was not successfull" }, JsonRequestBehavior.AllowGet);

                //return Json(new { redirectToUrl = Url.Action("PrintReceipt", "POS") , OrderNumber = salesOrederDTO.OrderNumber });

            }

            else
            {
                return Json(new { success = false, responseText = "Transaction was not successfull" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult PrintReceipt(string OrderNumber)
        {
            ViewBag.OrderNumber = OrderNumber;

            return View();
        }
        //public async Task<ActionResult> GenerateReceiptAsync1(string OrderNumber)
        //{
        //    try
        //    {
        //        ReportDocument rd = new ReportDocument();

        //        rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CustomerSalesReport.rpt"));

        //        var data = await salesService.GetSalesOrdersDetailsByOrderNumber(OrderNumber);

        //        ReportDataSet1 dataSet1 = new ReportDataSet1();

        //        //foreach (var item in data)
        //        //{
        //        //    dataSet1.SalesOrderDetails.AddSalesOrderDetailsRow(

        //        //         item.Id,

        //        //          item.OrderId,

        //        //          item.ProductId,

        //        //          item.Quantity,

        //        //          item.SellingPrice,

        //        //          item.Discount,

        //        //          item.Total,

        //        //          item.OrderNumber == null ? "" : item.OrderNumber.ToString(),

        //        //          item.CreateDate,

        //        //          item.CreatedBy == null ? "" : item.CreatedBy.ToString(),

        //        //          item.ProductName == null ? "" : item.ProductName.ToString(),

        //        //          item.CustomerName == null ? "" : item.CustomerName.ToString()

        //        //      );

        //        //}

        //        rd.SetDataSource(dataSet1);

        //        Response.Buffer = false;

        //        Response.ClearContent();

        //        Response.ClearHeaders();

        //        rd.PrintOptions.PaperOrientation = PaperOrientation.Landscape;

        //        rd.PrintOptions.ApplyPageMargins(new PageMargins(5, 5, 5, 5));

        //        rd.PrintOptions.PaperSize = PaperSize.PaperA5;

        //        Stream stream = rd.ExportToStream(ExportFormatType.PortableDocFormat);

        //        stream.Seek(0, SeekOrigin.Begin);

        //        //return File(stream, "application/pdf", "eConsultation.pdf");
        //        return File(stream, "application/pdf", "OrderReceipt.pdf");

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return null;
        //    }
        //}
        public async Task<ActionResult> GetReceipt(string OrderNumber)
        {

            try
            {
                LocalReport lr = new LocalReport();

                string p = Path.Combine(Server.MapPath("~/Reports"), "OrderReceipt.rdlc");

                lr.ReportPath = p;


                var l = await salesService.GetSalesOrdersDetailsByOrderNumber(OrderNumber);

                ReportDataSource rd = new ReportDataSource("DataSet1", l);

                lr.DataSources.Add(rd);

                string mt, enc, f;

                string[] s;

                Warning[] w;

                byte[] b = lr.Render("PDF", null, out mt, out enc, out f, out s, out w);

                return File(b, mt);
                //return File(b,mt, "application/pdf");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);

                return null;
            }






        }

        public ActionResult SalesDetails()
        {

            return View();
        }

        public ActionResult CustomerOrders()
        {

            return View();
        }
        public async Task<ActionResult> GetSales(Models.JqueryDatatableParam param)
        {

            var customers = await salesService.GetSalesOrdersDetails();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                customers = customers.Where(x => x.CustomerName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.OrderNumber.ToLower().Contains(param.sSearch.ToLower())
                                              // || x.CreateByName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.SellingPrice.ToString().Contains(param.sSearch.ToLower())
                                              || x.Quantity.ToString().Contains(param.sSearch.ToLower())
                                              || x.CreateDate.ToString().Contains(param.sSearch.ToLower())).ToList();

            }
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);

            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];

            if (sortColumnIndex == 3)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.CustomerName).ToList() : customers.OrderByDescending(c => c.CustomerName).ToList();
            }
            else if (sortColumnIndex == 4)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.OrderNumber).ToList() : customers.OrderByDescending(c => c.OrderNumber).ToList();
            }
            else if (sortColumnIndex == 5)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.Quantity).ToList() : customers.OrderByDescending(c => c.Quantity).ToList();
            }
            else
            {
                Func<SalesDetailsDTO, string> orderingFunction = e => sortColumnIndex == 0 ? e.CustomerName : sortColumnIndex == 1 ? e.OrderNumber : e.SellingPrice.ToString();

                customers = sortDirection == "asc" ? customers.OrderBy(orderingFunction).ToList() : customers.OrderByDescending(orderingFunction).ToList();
            }

            var displayResult = customers.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = customers.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);

        }


        public async Task<ActionResult> GetCustomerOrders(Models.JqueryDatatableParam param)
        {

            var customers = await salesService.GetCustomerOrders();

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                customers = customers.Where(x => x.CustomerName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.OrderNumber.ToLower().Contains(param.sSearch.ToLower())
                                              || x.TotalAmount.ToString().Contains(param.sSearch.ToLower())
                                              || x.Change.ToString().Contains(param.sSearch.ToLower())
                                              || x.OrderDate.ToString().Contains(param.sSearch.ToLower())).ToList();

            }
            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);

            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];

            if (sortColumnIndex == 3)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.CustomerName).ToList() : customers.OrderByDescending(c => c.CustomerName).ToList();
            }
            else if (sortColumnIndex == 4)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.OrderNumber).ToList() : customers.OrderByDescending(c => c.OrderNumber).ToList();
            }
            else if (sortColumnIndex == 5)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.TotalAmount).ToList() : customers.OrderByDescending(c => c.TotalAmount).ToList();
            }
            else
            {
                Func<SalesDTO, string> orderingFunction = e => sortColumnIndex == 0 ? e.CustomerName : sortColumnIndex == 1 ? e.OrderNumber : e.TotalAmount.ToString();

                customers = sortDirection == "asc" ? customers.OrderBy(orderingFunction).ToList() : customers.OrderByDescending(orderingFunction).ToList();
            }

            var displayResult = customers.Skip(param.iDisplayStart)
                .Take(param.iDisplayLength).ToList();
            var totalRecords = customers.Count();

            return Json(new
            {
                param.sEcho,
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = displayResult
            }, JsonRequestBehavior.AllowGet);

        }
    }
}