
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.EDMX;
using WebStore.Data.DTOs.UserModule;
using System.ComponentModel;
using static WebStore.Data.Utils.Enumerations;

namespace WebStore.Data.Services.UserModule
{
    public class UserService : IUserService
    {
        private readonly WinkadStoreEntities context;

        public UserService(WinkadStoreEntities context)
        {
            this.context = context;
        }
        public async Task<bool> ApproveUser(string Id)
        {
            try
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    var users = await context.AspNetUsers.FindAsync(Id);
                    {
                        users.IsApproved = 1;

                        await context.SaveChangesAsync();

                        transaction.Commit();

                        return true;

                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public Task<bool> CreateUser(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DisableUser(string Id)
        {
            try
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    var users = await context.AspNetUsers.FindAsync(Id);
                    {
                        users.AccountStatus = 0;

                        transaction.Commit();

                        await context.SaveChangesAsync();

                        return true;

                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public async Task<bool> EnableUser(string Id)
        {
            try
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    var users = await context.AspNetUsers.FindAsync(Id);
                    {
                        users.AccountStatus = 1;

                        transaction.Commit();

                        await context.SaveChangesAsync();

                        return true;

                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return false;
            }
        }

        public List<UserDTO> GetAllUsers()
        {
            try
            {
                var usersWithRoles = (from user in context.AspNetUsers

                                      from userRole in user.AspNetRoles

                                      join role in context.AspNetRoles on userRole.Id equals
                                      role.Id
                                      select new UserDTO()
                                      {
                                          Id = user.Id,

                                          UserName = user.UserName,

                                          FirstName = user.FirstName,

                                          LastName = user.LastName,

                                          Email = user.Email,

                                          PhoneNumber = user.PhoneNumber,

                                          EmailConfirmed = user.EmailConfirmed,

                                          IsApproved = user.IsApproved,

                                          CreateDate = user.CreateDate,

                                          AccountStatus = user.AccountStatus ,

                                          //AccountStatusDescription = GetDescription((AccountStatus)user.AccountStatus),

                                          RoleName = role.Name

                                      }).ToList();

                return usersWithRoles;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }


        public Task<bool> UpdateUserInformation(string Id)
        {
            throw new NotImplementedException();
        }


        private static object SyncObj = new object();
        static Dictionary<Enum, string> _enumDescriptionCache = new Dictionary<Enum, string>();
        public static string GetDescription(Enum value)
        {
            if (value == null) return string.Empty;

            lock (SyncObj)
            {
                if (!_enumDescriptionCache.ContainsKey(value))
                {
                    var description = (from m in value.GetType().GetMember(value.ToString())
                                       let attr = (DescriptionAttribute)m.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault()
                                       select attr == null ? value.ToString() : attr.Description).FirstOrDefault();

                    _enumDescriptionCache.Add(value, description);
                }
            }

            return _enumDescriptionCache[value];
        }
    }
}
