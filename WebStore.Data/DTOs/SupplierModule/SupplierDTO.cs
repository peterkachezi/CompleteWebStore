using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebStore.Data.DTOs.SupplierModule
{
    public class SupplierDTO
    {
        public System.Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + MiddleName + " " + LastName;
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Company { get; set; }
        public string Website { get; set; }
        public string PIN { get; set; }
        public string AccountNo { get; set; }
        public string Notes { get; set; }
        public string AttachmentFileName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Guid CountryId { get; set; }
        public string CreateByName { get; set; }
        public string SupplierNumber { get; set; }
        public HttpPostedFileBase AttachmentFileName2 { get; set; }
    }
}
