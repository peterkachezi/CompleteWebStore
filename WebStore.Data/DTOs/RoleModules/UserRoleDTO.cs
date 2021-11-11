using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.RoleModules
{
  public  class UserRoleDTO
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string IdentityUser_Id { get; set; }
    }
}
