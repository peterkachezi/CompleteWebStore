using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStore.Data.Services.SalesModule;


namespace WebStore.Areas.Administrator.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ISalesService salesService;

        public OrdersController(ISalesService salesService)
        {
            this.salesService = salesService;
        }
        public async Task<ActionResult> Index(string OrderNumber)
        {

            if (OrderNumber != null)
            {
                var empList = await salesService.GetSalesOrdersDetailsByOrderNumber(OrderNumber);

                return View(empList);
            }


            return View();
        }

        public async Task<ActionResult> GetList()
        {

            var empList = await salesService.GetSalesOrdersDetails();


            return Json(new { data = empList }, JsonRequestBehavior.AllowGet);


        }


    }
}