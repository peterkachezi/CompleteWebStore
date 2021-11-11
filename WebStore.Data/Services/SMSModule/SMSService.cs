using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.EDMX;
using WebStore.Data.DTOs.SMSModule;
using System.Net.Http;
using WebStore.Data.DTOs.CustomerModule;
using System.Configuration;

namespace WebStore.Data.Services.SMSModule
{
    public class SMSService : ISMSService
    {
        public async Task<bool> SaveResponse(SMSDTO sMSDTO)
        {
            try
            {
                using (var db = new WinkadStoreEntities())
                {
                    var s = new SmsRespons
                    {
                        Id = Guid.NewGuid(),

                        Number = sMSDTO.Number,

                        Status = sMSDTO.Status,

                        MessageId = sMSDTO.MessageId,

                        Cost = sMSDTO.Cost,

                        CreatedDate = DateTime.Now,
                    };

                    db.SmsResponses.Add(s);

                    return await db.SaveChangesAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }


        public async Task<bool> SendCustomerSMS(CustomerDTO customerDTO)
        {
            try
            {
                var url = "http://167.172.14.50:4002/v1/send-sms";

                var txtMessage = "Dear" + " " + customerDTO.FirstName + " ," + " Welcome to Winkad Fresh Drops.Your Customer No Is" + " " + customerDTO.CustomerNumber;

                var key = ConfigurationManager.AppSettings["BongaSMSKey"];

                var secrete = ConfigurationManager.AppSettings["BongaSMSSecrete"];

                var apiClientID = ConfigurationManager.AppSettings["BongaSMSApiClientID"];

                var serviceID = ConfigurationManager.AppSettings["BongaSMSServiceID"];

                var msisdn = customerDTO.PhoneNumber;

                var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("apiClientID", apiClientID),
                new KeyValuePair<string, string>("secret", secrete),
                new KeyValuePair<string, string>("key", key),
                new KeyValuePair<string, string>("txtMessage", txtMessage),
                new KeyValuePair<string, string>("MSISDN", msisdn),
                new KeyValuePair<string, string>("serviceID", serviceID),
                new KeyValuePair<string, string>("enqueue", "yes"),
            });

                HttpClient client = new HttpClient();

                HttpResponseMessage apiResult = await client.PostAsync(url, formContent);

                apiResult.EnsureSuccessStatusCode();

                var response = await apiResult.Content.ReadAsStringAsync();

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
