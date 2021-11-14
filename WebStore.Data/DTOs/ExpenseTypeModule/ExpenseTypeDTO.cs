using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.ExpenseTypeModule
{
   public  class ExpenseTypeDTO
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreateDate { get; set; }

        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
    }
}
