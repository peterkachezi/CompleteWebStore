using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebStore.Data.DTOs.UserModule;
using WebStore.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using WebStore.Helpers;
using WebStore.Data.DTOs.RoleModules;
using WebStore.Data.Services.UserModule;
using WebStore.Services;
using PasswordOptions = WebStore.Helpers.PasswordOptions;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace WebStore.Areas.Administrator.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IUserService _userService;

        private readonly IMailService mailService;

        private ApplicationUserManager _userManager;
        public UserManagementController(IUserService userService, IMailService mailService)
        {
            _userService = userService;

            this.mailService = mailService;
        }
        public async Task<ActionResult> Index()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new Microsoft.AspNet.Identity.RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            ViewBag.Roles = (await roleManager.Roles.ToListAsync()).Where(x => x.Name != "Accountant" && x.Name != "Manager" && x.Name != "Employee");

            return View();
        }
        public ActionResult GetUsers()
        {
            var users = _userService.GetAllUsers();

            return Json(new { data = users }, JsonRequestBehavior.AllowGet);
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ActionResult CreateAccount(UserDTO userDTO)
        {
            try
            {
                var IsUserExist = UserManager.FindByEmail(userDTO.Email);

                if (IsUserExist == null)
                {
                    string password = PasswordStore.GenerateRandomPassword(new PasswordOptions
                    {
                        RequiredLength = 8,
                        RequireNonLetterOrDigit = true,
                        RequireDigit = true,
                        RequireLowercase = true,
                        RequireUppercase = true,
                        RequireNonAlphanumeric = true,
                        RequiredUniqueChars = 1
                    });

                    userDTO.Password = password;

                    var user = new ApplicationUser
                    {
                        UserName = userDTO.Email,
                        Email = userDTO.Email,
                        PhoneNumber = userDTO.PhoneNumber,
                        EmailConfirmed = true,
                        FirstName = userDTO.FirstName,
                        LastName = userDTO.LastName,
                        CreateDate = DateTime.Now,
                        AccountStatus = 1,
                        IsApproved = 1,

                    };

                    string userPWD = password;

                    var chkUser = UserManager.Create(user, userPWD);

                    if (chkUser.Succeeded)
                    {
                        var result1 = UserManager.AddToRole(user.Id, userDTO.RoleName);

                    }

                    var send_mail = mailService.SendAccountConfirmationEmail(userDTO);

                    return Json(new { success = true, responseText = "Account has been successfully created" }, JsonRequestBehavior.AllowGet);

                }

                return Json(new { success = false, responseText = "Email is already taken." }, JsonRequestBehavior.AllowGet);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }


        }
        public async Task<ActionResult> ApproveAccount(string Id)
        {
            var results = await _userService.ApproveUser(Id);

            if (results)

            {
                return Json(new { success = true, responseText = "Account has been  successfully approved!" }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { success = false, responseText = "Failed to approve account !" }, JsonRequestBehavior.AllowGet);
            }

        }
        public async Task<ActionResult> DisableAccount(string Id)
        {
            var results = await _userService.DisableUser(Id);

            if (results)

            {
                return Json(new { success = true, responseText = "Account has been  successfully Disabled !" }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { success = false, responseText = "Failed to Disable account !" }, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> EnableAccount(string Id)
        {
            var results = await _userService.EnableUser(Id);

            if (results)

            {
                return Json(new { success = true, responseText = "Account has been  Enabled successfully !" }, JsonRequestBehavior.AllowGet);
            }

            else
            {
                return Json(new { success = false, responseText = "Failed to enable account !" }, JsonRequestBehavior.AllowGet);
            }

        }
        public async Task<ActionResult> ResendAccountLogins(string Id)
        {
            try
            {

                var user = await UserManager.FindByIdAsync(Id);


                var token = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                string password = PasswordStore.GenerateRandomPassword(new PasswordOptions
                {
                    RequiredLength = 8,
                    RequireNonLetterOrDigit = true,
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireUppercase = true,
                    RequireNonAlphanumeric = true,
                    RequiredUniqueChars = 1
                });

                var PlainPassword = password;

                var result = await UserManager.ResetPasswordAsync(user.Id, token, password);

                var rolename = UserManager.GetRoles(user.Id).FirstOrDefault();

                var userdetails = new UserDTO
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email,
                    RoleName = rolename,
                    Password = PlainPassword,
                };


                var send_mail = mailService.SendAccountConfirmationEmail(userdetails);


                return Json(new { success = true, responseText = "Login details has been successfully sent to user's email !" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
         
        public ActionResult UserProfile(string Id)
        {
            var user = _userService.GetAllUsers().Where(x=>x.Id==Id).FirstOrDefault();

            return View(user);
        }
    }
}