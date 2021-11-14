using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebStore.Data.DTOs.ExpenseModule
{
    public class ExpenseDTO
    {
        public System.Guid Id { get; set; }
        public System.Guid ExpenseTypeId { get; set; }
        public string ExpenseTypeName { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime ExpenseDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string Description { get; set; }
        public string ReceiptAttachment { get; set; }

        public HttpPostedFileBase ReceiptAttachment2 { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public long IsRecurring { get; set; }
        public string RecurringExpense { get; set; }
        public string ReceiptAttachmentDesc { get; set; }
    }
}
