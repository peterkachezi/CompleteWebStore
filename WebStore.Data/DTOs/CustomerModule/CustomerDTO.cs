using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebStore.Data.DTOs.CustomerModule
{
    public class CustomerDTO
    {
        public System.Guid Id { get; set; }
        public string FullName => FirstName + "  " + MiddleName + "  " + LastName;
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string AttachmentFileName { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName => CreatedByFirstName + " " + CreatedByLastName;
        public string CreatedByFirstName { get; set; }
        public string CreatedByLastName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CustomerNumber { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string StartDateString { get; set; }
        public string DeliveryLocation { get; set; }
        public string Apartment { get; set; }

    }
}
