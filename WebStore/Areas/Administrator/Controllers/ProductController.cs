﻿using System;
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
    public class ProductController : Controller
    {
        private readonly IProductService productService;

        private readonly IProductPackingService productPackingService;

        public ProductController
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


        public async Task<ActionResult> CreateProduct()
        {

            ViewBag.Packaging = await productPackingService.GetAll();

            return View();
        }

        public async Task<ActionResult> Create(ProductDTO productDTO)
        {
            try
            {
                var isProductExist = (await productService.GetAll()).Where(x => x.PackagingId == productDTO.PackagingId).Count();

                if (isProductExist > 0)
                {
                    return Json(new { success = false, responseText = "The product already exist" }, JsonRequestBehavior.AllowGet);

                }

                var user = User.Identity.GetUserId();

                productDTO.CreatedBy = user;

                var result = await productService.Create(productDTO);

                if (result != null)
                {

                    return Json(new { success = true, responseText = "Product has been successfully created" }, JsonRequestBehavior.AllowGet);
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
        public async Task<ActionResult> Update(ProductDTO productDTO)
        {
            try
            {
                var user = User.Identity.GetUserId();

                productDTO.UpdateBy = user;

                var results = await productService.Update(productDTO);

                if (results != null)

                {
                    return Json(new { success = true, responseText = "Product  has been  successfully updated!" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    return Json(new { success = false, responseText = "Failed to update Product  !" }, JsonRequestBehavior.AllowGet);
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

                    Status = data.Status,

                    CreatedByName = data.CreatedByName,

                    PackagingId = data.PackagingId,
                };

                return Json(new { data = file }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { data = false }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Delete(Guid Id)
        {
            try
            {
                var results = await productService.Delete(Id);

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
    }
}