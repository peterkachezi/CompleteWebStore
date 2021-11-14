using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStore.Data.DTOs.ExpenseModule;
using WebStore.Data.Services.ExpenseModule;
using WebStore.Data.Services.ExpenseTypeModule;

namespace WebStore.Areas.Administrator.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpenseService expenseService;
        private readonly IExpenseTypeService expenseTypeService;
        public ExpensesController(IExpenseService expenseService, IExpenseTypeService expenseTypeService)
        {
            this.expenseService = expenseService;
            this.expenseTypeService = expenseTypeService;
        }
        public async Task<ActionResult> Index()
        {
            var expenses = await expenseService.GetAll();

            return View(expenses);
        }
        public async Task<ActionResult> GetExpenses()
        {
            var expenses = await expenseService.GetAll();

            return Json(new { data = expenses }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> AddExpense()
        {
            ViewBag.ProductType = await expenseTypeService.GetAll();

            return View();
        }

        public async Task<ActionResult> Create(ExpenseDTO expenseDTO)
        {
            try
            {
                if (expenseDTO.ReceiptAttachment2 != null)
                {
                    var ExpenseId = Guid.NewGuid();

                    var user = User.Identity.GetUserId();

                    string FileName = ExpenseId + "_" + expenseDTO.ReceiptAttachment2.FileName;

                    var pathToSave = Server.MapPath("~/Content/ExpenseFiles/") + FileName;

                    expenseDTO.Id = ExpenseId;

                    expenseDTO.ReceiptAttachment = FileName;

                    expenseDTO.ReceiptAttachmentDesc = expenseDTO.ReceiptAttachment2.FileName;

                    expenseDTO.CreatedBy = user;

                    expenseDTO.ReceiptAttachment2.SaveAs(pathToSave);

                }

                var results = await expenseService.Create(expenseDTO);

                if (results == true)
                {
                    return Json(new { success = true, responseText = "Record has been successfully saved" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Record has been  not been saved" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception dex)
            {
                return Json(new
                {
                    success = false,
                    responseText = dex.Message
                });

            }
        }
        public async Task<ActionResult> GetById(Guid Id)
        {
            try
            {
                var data = await expenseService.GetById(Id);

                if (data != null)
                {
                    ExpenseDTO file = new ExpenseDTO()
                    {
                        Id = data.Id,

                        ExpenseTypeId = data.ExpenseTypeId,

                        Amount = data.Amount,

                        ExpenseDate = data.ExpenseDate,

                        Description = data.Description,

                        ReceiptAttachment = data.ReceiptAttachment,

                        CreateDate = data.CreateDate,

                        CreatedBy = data.CreatedBy,

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
                var results = await expenseService.Delete(Id);

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
        public async Task<ActionResult> Update(ExpenseDTO expenseDTO)
        {
            try
            {
                var results = await expenseService.Update(expenseDTO);

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
        public ActionResult DownlodExpenseFiles(string FileName)
        {
            try
            {

                if (string.IsNullOrEmpty(FileName))
                {
                    TempData["Error"] = "Unable to download,File not available!";

                    return RedirectToAction("Index");
                }
                else
                {
                    var filename = Server.MapPath("~/Content/ExpenseFiles/") + FileName;

                    var file = System.IO.File.ReadAllBytes(filename);

                    return File(file, "application/excel", FileName);
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