
using Microsoft.AspNet.Identity;
using RazorEngine;
using RazorEngine.Templating;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

using WebStore.Data.DTOs.CustomerModule;
using WebStore.Data.DTOs.SMSModule;
using WebStore.Data.Services.CountryModule;
using WebStore.Data.Services.CustomersModule;
using WebStore.Data.Services.SMSModule;
using WebStore.Services;

namespace WebStore.Areas.Administrator.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomersService customerService;
        private readonly ICountryService countryService;
        private readonly ISMSService iSMSService;
        private readonly IMailService mailService;


        public CustomerController(IMailService mailService, ICustomersService customerService, ICountryService countryService, ISMSService sMSService)

        {
            this.customerService = customerService;
            this.countryService = countryService;
            this.iSMSService = sMSService;
            this.mailService = mailService;

        }
        public async Task<ActionResult> Index()
        {
            // var customer = await customerService.GetAllCustomers();

            ViewBag.Countries = await countryService.GetAllCountries();

            return View();
        }
        public async Task<ActionResult> GetDataAsync(Models.JqueryDatatableParam param)
        {
            var midnight = DateTime.Today;

            var endOfToday = midnight.AddHours(23).AddMinutes(59).AddSeconds(59);

            var customers = await customerService.GetAllCustomers();

            //customers.ToList().ForEach(x => x.StartDateString = x.CreateDate.ToString());

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                customers = customers.Where(x => x.FirstName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.MiddleName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.LastName.ToLower().Contains(param.sSearch.ToLower())
                                              || x.PhoneNumber.ToString().Contains(param.sSearch.ToLower())
                                              || x.Email.ToString().Contains(param.sSearch.ToLower())
                                              || x.CustomerNumber.ToString().Contains(param.sSearch.ToLower())
                                              || x.CreateDate.ToString().ToLower().Contains(param.sSearch.ToLower())).ToList();
            }

            var sortColumnIndex = Convert.ToInt32(HttpContext.Request.QueryString["iSortCol_0"]);

            var sortDirection = HttpContext.Request.QueryString["sSortDir_0"];

            if (sortColumnIndex == 3)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.FirstName).ToList() : customers.OrderByDescending(c => c.FirstName).ToList();
            }
            else if (sortColumnIndex == 4)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.MiddleName).ToList() : customers.OrderByDescending(c => c.MiddleName).ToList();
            }
            else if (sortColumnIndex == 5)
            {
                customers = sortDirection == "asc" ? customers.OrderBy(c => c.LastName).ToList() : customers.OrderByDescending(c => c.LastName).ToList();
            }
            else
            {
                Func<CustomerDTO, string> orderingFunction = e => sortColumnIndex == 0 ? e.FirstName :
                                                               sortColumnIndex == 1 ? e.MiddleName :
                                                               e.LastName;

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
        public async Task<ActionResult> AddCustomer(CustomerDTO customerDTO)
        {
            try
            {
                var IsCustomerExist = (await customerService.GetAllCustomers()).Where(x => x.Email == customerDTO.Email && x.PhoneNumber == customerDTO.PhoneNumber).Count();

                if (IsCustomerExist > 0)
                {
                    return Json(new { success = false, responseText = "The customer already exists" }, JsonRequestBehavior.AllowGet);

                }

                var user = User.Identity.GetUserId();

                customerDTO.CreatedBy = user;

                var result = await customerService.AddCustomer(customerDTO);

                if (result != null)
                {

                    var sendMail = mailService.SendEmailNotification(customerDTO);

                    //var s = iSMSService.SendCustomerSMS(customerDTO);

                    return Json(new { success = true, responseText = "Customer has been successfully saved" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, responseText = "Customer has been  not been saved" }, JsonRequestBehavior.AllowGet);
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
                var data = await customerService.GetCustomerById(Id);

                if (data != null)
                {
                    CustomerDTO file = new CustomerDTO()
                    {
                        Id = data.Id,

                        FirstName = data.FirstName,

                        MiddleName = data.MiddleName,

                        LastName = data.LastName,

                        Email = data.Email,

                        PhoneNumber = data.PhoneNumber,

                        CreatedBy = data.CreatedBy,

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
                var results = await customerService.DeleteCustomer(Id);

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

        public async Task<ActionResult> Update(CustomerDTO  customerDTO)
        {
            try
            {
                var results = await customerService.Update(customerDTO);

                if (results != null)

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