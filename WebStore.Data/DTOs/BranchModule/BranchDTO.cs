using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.BranchModule
{
    public class BranchDTO
    {
        public System.Guid Id { get; set; }
        public string StoreName { get; set; }
        public string Town { get; set; }
        public System.Guid CountyId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
