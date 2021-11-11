using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.ExpenseModule
{
    public class ExpenseDTO
    {
        public System.Guid Id { get; set; }
        public System.Guid ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; }
        public decimal ExpenseAmount { get; set; }
        public System.DateTime ExpenseDate { get; set; }
        public string Description { get; set; }
        public string ReceiptAttachment { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
