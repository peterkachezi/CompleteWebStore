using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using RazorEngine;
using RazorEngine.Templating;
using WebStore.Data.DTOs.CustomerModule;
using WebStore.Data.DTOs.UserModule;

namespace WebStore.Services
{
    public class MailService : IMailService
    {
        public bool SendEmailNotification(CustomerDTO customerDTO)
        {
            try
            {

                MailAddressCollection mailAddressesTo = new MailAddressCollection();

                mailAddressesTo.Add(new MailAddress(customerDTO.Email));

                MailAddress mailAddressFrom = new MailAddress(ConfigurationManager.AppSettings["SMTPUserName"]);

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = mailAddressFrom;

                foreach (var to in mailAddressesTo)
                    mailMessage.To.Add(to);

                mailMessage.Subject = "Winkad Fresh Drops: ";

                var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");

                var templatePath = Path.Combine(baseDir, @"EmailTemplates\EmailNotification.cshtml");

                var template = File.ReadAllText(templatePath);

                var templateResult = Engine.Razor.RunCompile(template, $"{Guid.NewGuid()}_EmailTemplate", null, customerDTO);

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

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }




        }

        public bool SendAccountConfirmationEmail(UserDTO userDTO)
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

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }

       
        }

    }
}