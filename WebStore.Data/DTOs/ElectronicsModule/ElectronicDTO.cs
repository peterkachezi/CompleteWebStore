using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.ElectronicsModule
{
    public class ElectronicDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string ModelNumber { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedByName { get; set; }
        public long Status { get; set; }
        public string StatusDescription { get; set; }
        public string SerialNumber { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}
