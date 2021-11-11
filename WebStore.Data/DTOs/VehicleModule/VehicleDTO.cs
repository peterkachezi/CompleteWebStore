using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.VehicleModule
{
    public class VehicleDTO
    {
        public System.Guid Id { get; set; }
        public string ModelName { get; set; }
        public string ModelYear { get; set; }
        public string PlateNumber { get; set; }
        public int MakeId { get; set; }
        public string MakeName { get; set; }
        public int Capacity { get; set; }
        public string Owner { get; set; }
        public System.DateTime DatePurchased { get; set; }
        public string NewDatePurchased { get { return DatePurchased.ToShortDateString(); } }
        public long Status { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string CreatedByName { get; set; }
        public string StatusDescription { get; set; }



    }
}
