using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebStore.Areas.Agent.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Agent/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}