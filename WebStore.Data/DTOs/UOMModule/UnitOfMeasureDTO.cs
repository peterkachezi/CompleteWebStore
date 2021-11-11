using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.UOMModule
{
    public class UnitOfMeasureDTO
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Unit { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
