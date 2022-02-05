using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStore.Data.Services.CustomersModule;
using WebStore.Data.Services.SalesModule;
using WebStore.Data.Services.Services.ProductModule;
using WebStore.Data.Services.SupplierModule;
using WebStore.Models;

namespace WebStore.Areas.Administrator.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IProductService   productService;

        private readonly ISalesService   salesService;

        private readonly ICustomersService  customersService;

        private readonly ISupplierService supplierService;
        public DashboardController
            (
            IProductService productService,
            ISalesService salesService,
            ICustomersService customersService, 
            ISupplierService supplierService
            
            )
        {
            this.productService = productService;
            this.salesService = salesService;
            this.customersService = customersService;
            this.supplierService = supplierService;
        }
        // GET: Administrator/Dashboard
        public async Task<ActionResult> Index()
        {

            try
            {
                ViewBag.Products = (await productService.GetAll()).Count;
                //ViewBag.Sales = (await salesService.GetSalesOrdersDetails()).Count;
                ViewBag.Customers = (await customersService.GetAllCustomers()).Count;
                ViewBag.Suppliers = (await supplierService.GetAllSuppliers()).Count;


                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}