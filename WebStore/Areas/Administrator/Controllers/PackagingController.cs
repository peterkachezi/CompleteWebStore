using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebStore.Data.DTOs.ProductPackingModule;
using WebStore.Data.Services.ProductPackingModule;

namespace WebStore.Areas.Administrator.Controllers
{
    public class PackagingController : Controller
    {
        private readonly IProductPackingService productPackingService;
        public PackagingController(IProductPackingService productPackingService)
        {
            this.productPackingService = productPackingService;
        }
        public async Task<ActionResult> Index()
        {
            var packaging = await productPackingService.GetAll();

            return View(packaging);
        }
        public async Task<ActionResult> Create(ProductPackingDTO productPackingDTO)
        {
            try
            {

                var iSPackageExist = (await productPackingService.GetAll()).Where(x => x.Name == productPackingDTO.Name && x.Unit == productPackingDTO.Unit).Count();

                if (iSPackageExist > 0)
                {
                    return Json(new { success = false, responseText = "The record already exists" }, JsonRequestBehavior.AllowGet);

                }


                var user = User.Identity.GetUserId();

                productPackingDTO.CreatedBy = user;

                var result = await productPackingService.Create(productPackingDTO);

                if (result != null)
                {

                    return Json(new { success = true, responseText = "Record has been successfully created" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Unable to save the product" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> Update(ProductPackingDTO productPackingDTO)
        {
            try
            {
                var results = await productPackingService.Update(productPackingDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Product  has been  successfully updated!" }, JsonRequestBehavior.AllowGet);
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
        public async Task<ActionResult> GetById(Guid Id)
        {
            try
            {
                var data = await productPackingService.GetById(Id);

                if (data != null)
                {
                    ProductPackingDTO file = new ProductPackingDTO
                    {
                        Id = data.Id,

                        Name = data.Name,

                        Unit = data.Unit,

                        CreateDate = data.CreateDate,

                        CreatedBy = data.CreatedBy
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
        public async Task<ActionResult> Delete(Guid Id)
        {
            try
            {
                var results = await productPackingService.Delete(Id);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record  has been  successfully Deleted!" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Failed to Delete because the file is in use with other records!" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public async Task<ActionResult> GetData(Models.JqueryDatatableParam param)
        {
            var midnight = DateTime.Today;

            var endOfToday = midnight.AddHours(23).AddMinutes(59).AddSeconds(59);

            var customers = await productPackingService.GetAll();

            //customers.ToList().ForEach(x => x.StartDateString = x.CreateDate.ToString());

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                customers = customers.Where(x => x.Name.ToLower().Contains(param.sSearch.ToLower())
                                              || x.Unit.ToLower().Contains(param.sSearch.ToLower())
                                              || x.CreateDate.ToString().ToLower().Contains(param.sSearch.ToLower())).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);

            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];

            if (sortColumnIndex == 3)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.Name).ToList() : customers.OrderByDescending(c => c.Name).ToList();
            }
            else if (sortColumnIndex == 4)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.Unit).ToList() : customers.OrderByDescending(c => c.Unit).ToList();
            }
            else if (sortColumnIndex == 5)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.CreateDate).ToList() : customers.OrderByDescending(c => c.CreateDate).ToList();
            }
            else
            {
                Func<ProductPackingDTO, string> orderingFunction = e => sortColumnIndex == 0 ? e.Name : sortColumnIndex == 1 ? e.Unit : e.CreateDate.ToShortDateString();

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