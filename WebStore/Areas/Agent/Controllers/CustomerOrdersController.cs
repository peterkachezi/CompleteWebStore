using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStore.Data.Services.CustomerOrderModule;

namespace WebStore.Areas.Agent.Controllers
{
    public class CustomerOrdersController : Controller
    {
        private readonly ICustomerOrderService customerOrderService;
        public CustomerOrdersController(ICustomerOrderService customerOrderService)
        {
            this.customerOrderService = customerOrderService;
        }
        public async Task<ActionResult> Index()
        {
            var orders = await customerOrderService.GetAll();

            return View(orders);
        }



    }
}