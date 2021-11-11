using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.CustomerModule;
using WebStore.Data.DTOs.UserModule;

namespace WebStore.Services
{
    public interface IMailService
    {
        bool SendEmailNotification(CustomerDTO customerDTO);
        bool SendAccountConfirmationEmail(UserDTO userDTO);
    }
}
