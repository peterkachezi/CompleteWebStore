using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using RazorEngine.Templating;

using WebStore.Data.DTOs.SMSModule;
using WebStore.Data.DTOs.UserModule;
using WebStore.Data.Services.SMSModule;
using WebStore.Data.Services.UserModule;
using WebStore.Helpers;
using WebStore.Models;
using WebStore.Services;

namespace WebStore.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;

        private ApplicationUserManager _userManager;



        public AccountController()
        {

        }


        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = UserManager.FindByEmail(model.Email);

                if (user == null)
                {
                    TempData["Info"] = "Invalid user account";

                    return RedirectToAction("Login", "Account");
                }

                if (user.IsApproved == 0)
                {
                    TempData["Info"] = "Your account has not been approved please contact the system administrator for help";

                    return RedirectToAction("Login", "Account");
                }

                if (user.AccountStatus == 0)
                {
                    TempData["Error"] = "Your account has  been disabled please contact the system administrator for help";

                    return RedirectToAction("Login", "Account");
                }

                if (user.IsApproved == 2)
                {
                    TempData["Error"] = "Your account has been disabled please contact the system administrator for help";

                    return RedirectToAction("Login", "Account");
                }

                if (user.EmailConfirmed == false)
                {
                    TempData["Error"] = "Your account has not been comfirmed,please login to your email to confirm your account";

                    return RedirectToAction("Login", "Account");
                }

                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

                var rolename = UserManager.GetRoles(user.Id).FirstOrDefault();

                switch (result)
                {
                    case SignInStatus.Success:

                        switch (rolename)
                        {
                            case "Admin":
                                return RedirectToAction("Index", "Dashboard", new { area = "Administrator" });
                            case "Cashier":
                                return RedirectToAction("Index", "Dashboard", new { area = "PointOfSale" });
                            case "Agent":
                                return RedirectToAction("Index", "Dashboard", new { area = "Agent" });
                            default:
                                Console.WriteLine("Default case");
                                break;
                        }

                        return RedirectToAction("Index", "Home");

                    case SignInStatus.LockedOut:

                        TempData["Error"] = "You have been Lockout,please contact the system administartor for support";

                        return RedirectToAction("Login", "Account");

                    case SignInStatus.RequiresVerification:

                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                    case SignInStatus.Failure:

                    default:

                        TempData["Error"] = "Invalid login attempt";

                        return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<ActionResult> ResendPassword(UserDTO userDTO)
        {

            try
            {
                var user = await UserManager.FindByEmailAsync(userDTO.Email);

                if (user == null)
                {
                    return Json(new { success = false, responseText = "Failed to update the record  !" }, JsonRequestBehavior.AllowGet);

                }

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

                var send_mail = SendAccountConfirmationEmail(userdetails);

                return Json(new { success = true, responseText = "Email with a link has been sent to " + user.Email + " on how to reset your password.It may take 1 - 2 mins for email to arrive. " }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }

        public ActionResult SendAccountConfirmationEmail(UserDTO userDTO)
        {
            try
            {

                MailAddressCollection mailAddressesTo = new MailAddressCollection();

                mailAddressesTo.Add(new MailAddress(userDTO.Email));

                MailAddress mailAddressFrom = new MailAddress(ConfigurationManager.AppSettings["SMTPUserName"]);

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = mailAddressFrom;

                foreach (var to in mailAddressesTo)
                    mailMessage.To.Add(to);

                mailMessage.Subject = "Winkad Fresh Drops: ";

                var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

                var templatePath = Path.Combine(baseDir, @"EmailTemplates\UserAccountResetNotification.cshtml");

                var template = System.IO.File.ReadAllText(templatePath);

                var templateResult = RazorEngine.Engine.Razor.RunCompile(template, $"{Guid.NewGuid()}_EmailTemplate", null, userDTO);

                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                mailMessage.Body = templateResult.Trim();

                mailMessage.IsBodyHtml = true;

                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = ConfigurationManager.AppSettings["SMTPMailServer"];

                    client.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);

                    if (ConfigurationManager.AppSettings["SMTPUseSSL"] != string.Empty)
                    {
                        client.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTPUseSSL"]);
                    }

                    client.UseDefaultCredentials = false;

                    bool bNetwork = bool.Parse(ConfigurationManager.AppSettings["SMTPEmailToNetwork"]);

                    if (bNetwork)
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    }
                    else
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    }

                    client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUserName"], ConfigurationManager.AppSettings["SMTPPassword"]);

                    client.ServicePoint.MaxIdleTime = 2;

                    client.ServicePoint.ConnectionLimit = 1;

                    client.Send(mailMessage);
                }

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }


        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }


        public ActionResult get()
        {
            return View();
        }


        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {


            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    IsApproved = 0,

                };


                var result = await UserManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    model.Url = callbackUrl;

                    //var s = SendSMS(model);

                    var sendMail = SendAccountConfirmationEmail(model);

                    TempData["Success"] = "Your account has been successfully created awaiting approval . Please procced  to your email to confirm your account";

                    return RedirectToAction("Register", "Account");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form



            return View(model);
        }

        public ActionResult SendAccountConfirmationEmail(RegisterViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    MailMessage message = new MailMessage();
                    message.To.Add(new MailAddress(model.Email));
                    message.From = new MailAddress(ConfigurationManager.AppSettings["SMTPUserName"]);
                    message.Subject = "Winkad Fresh Drops: ";

                    var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

                    var templatePath = Path.Combine(baseDir, @"EmailTemplates\EmailAccountNotification.cshtml");

                    var template = System.IO.File.ReadAllText(templatePath);

                    var templateResult = RazorEngine.Engine.Razor.RunCompile(template, $"{Guid.NewGuid()}_EmailTemplate", null, model);

                    message.BodyEncoding = System.Text.Encoding.UTF8;

                    message.Body = templateResult.Trim();

                    message.IsBodyHtml = true;

                    message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    message.Headers.Add("Message-Id", String.Concat("<", DateTime.Now, ".", DateTime.Now, "winkadfreshdropsbottledwater.co.ke>"));

                    SmtpClient client = new SmtpClient();
                    client.Port = 25;
                    client.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTPUseSSL"]);

                    client.UseDefaultCredentials = true;
                    client.Host = ConfigurationManager.AppSettings["SMTPMailServer"];

                    client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUserName"], ConfigurationManager.AppSettings["SMTPPassword"]);

                    //Add this line to bypass the certificate validation
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                            System.Security.Cryptography.X509Certificates.X509Chain chain,
                            System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                    client.Send(message);

                    Response.Write("Mail Sent!!!");

                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }

        public ActionResult SendMail(UserDTO userDTO)
        {
            try
            {

                MailAddressCollection mailAddressesTo = new MailAddressCollection();

                mailAddressesTo.Add(new MailAddress(userDTO.Email));

                MailAddress mailAddressFrom = new MailAddress(ConfigurationManager.AppSettings["SMTPUserName"]);

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = mailAddressFrom;

                foreach (var to in mailAddressesTo)
                    mailMessage.To.Add(to);

                mailMessage.Subject = "Winkad Fresh Drops: ";

                var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

                var templatePath = Path.Combine(baseDir, @"EmailTemplates\UserAccountNotification.cshtml");

                var template = System.IO.File.ReadAllText(templatePath);

                var templateResult = RazorEngine.Engine.Razor.RunCompile(template, $"{Guid.NewGuid()}_EmailTemplate", null, userDTO);

                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;

                mailMessage.Body = templateResult.Trim();

                mailMessage.IsBodyHtml = true;

                using (SmtpClient client = new SmtpClient())
                {
                    client.Host = ConfigurationManager.AppSettings["SMTPMailServer"];

                    client.Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]);

                    if (ConfigurationManager.AppSettings["SMTPUseSSL"] != string.Empty)
                    {
                        client.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTPUseSSL"]);
                    }

                    client.UseDefaultCredentials = false;

                    bool bNetwork = bool.Parse(ConfigurationManager.AppSettings["SMTPEmailToNetwork"]);

                    if (bNetwork)
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    }
                    else
                    {
                        client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    }

                    client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUserName"], ConfigurationManager.AppSettings["SMTPPassword"]);

                    client.ServicePoint.MaxIdleTime = 2;

                    client.ServicePoint.ConnectionLimit = 1;

                    client.Send(mailMessage);
                }

                return null;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        //public ActionResult SendSMS(RegisterViewModel model)
        //{
        //    var username = ConfigurationManager.AppSettings["username"];

        //    var apikey = ConfigurationManager.AppSettings["apikey"];

        //    var gateway = new AfricasTalkingGateway(username, apikey);

        //    try
        //    {
        //        SMSDTO sMSDTO = new SMSDTO();

        //        var user_fullname = model.FirstName + "   " + model.LastName;
        //        var url = "https://winkadfreshdropsbottledwater.co.ke/";

        //        var message =
        //            "Dear " + user_fullname +
        //            "" +
        //            "Welcom to Winkad Freshdrops" +
        //            "" +
        //            "Your Password is" + model.Password +
        //            "" +
        //           "Your UserName is" + model.Email +
        //           "" +
        //            "You will be notified on your account approval" +
        //            "" +
        //            "Use this link to log in" + url;

        //        var sms = gateway.SendMessage(model.PhoneNumber, message);

        //        foreach (var res in sms["SMSMessageData"]["Recipients"])
        //        {
        //            sMSDTO.Number = (string)res["number"];

        //            sMSDTO.Status = (string)res["status"];

        //            sMSDTO.MessageId = (string)res["messageId"];

        //            sMSDTO.Cost = (string)res["cost"];

        //            //var save = await _iSMSService.SaveResponse(sMSDTO);

        //        }
        //    }
        //    catch (AfricasTalkingGatewayException exception)
        //    {
        //        Console.WriteLine(exception);
        //    }

        //    return View();
        //}






        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword1(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var user = UserManager.FindByEmail(model.Email);


                if (user == null)
                {
                    TempData["Error"] = "Invalid Email Address";
                    return RedirectToAction("ForgotPassword", "Account");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Url = callbackUrl;

                var send_mail = SendMailResetPassword(model);

                TempData["Success"] = "Email with a link has been sent to " + model.Email + " on how to reset your password.It may take 1 - 2 mins for email to arrive.";

                return RedirectToAction("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult SendMailResetPassword(ForgotPasswordViewModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    MailMessage message = new MailMessage();
                    message.To.Add(new MailAddress(model.Email));
                    message.From = new MailAddress(ConfigurationManager.AppSettings["SMTPUserName"]);
                    message.Subject = "Reset password request Winkad Fresh Drops: ";

                    var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

                    var templatePath = Path.Combine(baseDir, @"EmailTemplates\ResetPasswordNotification.cshtml");

                    var template = System.IO.File.ReadAllText(templatePath);

                    var templateResult = RazorEngine.Engine.Razor.RunCompile(template, $"{Guid.NewGuid()}_EmailTemplate", null, model);

                    message.BodyEncoding = System.Text.Encoding.UTF8;

                    message.Body = templateResult.Trim();

                    message.IsBodyHtml = true;

                    message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    message.Headers.Add("Message-Id", String.Concat("<", DateTime.Now, ".", DateTime.Now, "winkadfreshdropsbottledwater.co.ke>"));

                    SmtpClient client = new SmtpClient();
                    client.Port = 25;
                    client.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTPUseSSL"]);

                    client.UseDefaultCredentials = true;
                    client.Host = ConfigurationManager.AppSettings["SMTPMailServer"];

                    client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTPUserName"], ConfigurationManager.AppSettings["SMTPPassword"]);

                    //Add this line to bypass the certificate validation
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                            System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                            System.Security.Cryptography.X509Certificates.X509Chain chain,
                            System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                    client.Send(message);

                    Response.Write("Mail Sent!!!");

                    return View();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View();
        }




        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            ViewBag.Code = code;


            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}