using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebStore.Areas.PointOfSale.Controllers
{
    public class CustomerOrdersController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
    }
}