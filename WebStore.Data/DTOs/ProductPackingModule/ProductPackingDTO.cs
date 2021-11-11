using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.ProductPackingModule
{
    public class ProductPackingDTO
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string FullName => Name + " " + Unit;
        public string Unit { get; set; }
        public DateTime CreateDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
    }
}
