using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.DTOs.UserModule;

namespace WebStore.Data.Services.UserModule
{
    public interface IUserService
    {

        Task<bool> ApproveUser(string Id);
        Task<bool> DisableUser(string Id);
        Task<bool> EnableUser(string Id);
        Task<bool> DeleteUser(string Id);
        Task<bool> CreateUser(string Id);
        Task<bool> UpdateUserInformation(string Id);
        List<UserDTO> GetAllUsers();
    }
}
