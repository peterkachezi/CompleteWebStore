using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebStore.Data.DTOs.ProductModule;
using WebStore.Data.Services.ProductPackingModule;
using WebStore.Data.Services.Services.ProductModule;

namespace WebStore.Areas.Administrator.Controllers
{
    public class ManageStockController : Controller
    {

        private readonly IProductService productService;
        private readonly IProductPackingService productPackingService;

        public ManageStockController
            (
            IProductService productService,
             IProductPackingService productPackingService

            )

        {
            this.productService = productService;
            this.productPackingService = productPackingService;

        }
        public async Task<ActionResult> Index()
        {
            var products = await productService.GetAll();

            ViewBag.Packaging = await productPackingService.GetAll();

            return View(products);
        }
        public async Task<ActionResult> UpdateStock(ProductDTO productDTO)
        {
            try
            {
                var user = User.Identity.GetUserId();

                productDTO.UpdateBy = user;

                var results = await productService.UpdateStock(productDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Stock  has been  successfully updated!" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update stock  !" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
        public async Task<ActionResult> RemoveStock(ProductDTO productDTO)
        {
            try
            {
                var user = User.Identity.GetUserId();

                productDTO.UpdateBy = user;

                var results = await productService.RemoveStock(productDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Stock  has been  successfully updated!" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update stock  !" }, JsonRequestBehavior.AllowGet);
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
            var data = await productService.GetById(Id);

            if (data != null)
            {
                ProductDTO file = new ProductDTO
                {
                    Id = data.Id,

                    Name = data.Name,

                    ProductCode = data.ProductCode,

                    Quantity = data.Quantity,

                    CostPrice = data.CostPrice,

                    SellingPrice = data.SellingPrice,

                    ReOrderLevel = data.ReOrderLevel,

                    ExpectedProfit = data.ExpectedProfit,

                    CreateDate = data.CreateDate,

                    CreatedBy = data.CreatedBy,

                    UpdateBy = data.CreatedBy,

                    UpdatedDate = data.UpdatedDate,

                    PackagingId = data.PackagingId,

                    Status = data.Status,

                    CreatedByName = data.CreatedByName,

                    ProductName = data.ProductName,
                };

                return Json(new { data = file }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }

    }
}