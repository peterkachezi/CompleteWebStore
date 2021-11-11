using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.ProductTypeModule
{
    public class ProductTypeDTO
    {
        public System.Guid Id { get; set; }
        public System.Guid UnitOfMeasureId { get; set; }
        public string Name { get; set; }
        public string FullDescription { get; set; }
        public string UnitOfMeasureName { get; set; }
        public string CreatedByName { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
