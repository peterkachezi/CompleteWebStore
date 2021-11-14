using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebStore.Data.DTOs.ExpenseTypeModule;
using WebStore.Data.Services.ExpenseTypeModule;

namespace WebStore.Areas.Administrator.Controllers
{
    public class ExpenseTypesController : Controller
    {
        private readonly IExpenseTypeService expenseTypeService;
        public ExpenseTypesController(IExpenseTypeService expenseTypeService)
        {
            this.expenseTypeService = expenseTypeService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> GetExpenseType()
        {
            var expenses = await expenseTypeService.GetAll();

            return Json(new { data = expenses }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> Create(ExpenseTypeDTO expenseTypeDTO)
        {
            try
            {


                var user = User.Identity.GetUserId();

                expenseTypeDTO.CreatedBy = user;

                var results = await expenseTypeService.Create(expenseTypeDTO);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record has been successfully saved" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Record has been  not been saved" }, JsonRequestBehavior.AllowGet);
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
                var data = await expenseTypeService.GetById(Id);

                if (data != null)
                {
                    ExpenseTypeDTO file = new ExpenseTypeDTO()
                    {
                        Id = data.Id,

                        Name = data.Name,

                        CreatedBy = data.CreatedBy,

                        CreatedByName = data.CreatedByName,

                        CreateDate = data.CreateDate,
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
                var results = await expenseTypeService.Delete(Id);

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
        public async Task<ActionResult> Update(ExpenseTypeDTO expenseTypeDTO)
        {
            try
            {
                var results = await expenseTypeService.Update(expenseTypeDTO);

                if (results == true)

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
    }
}