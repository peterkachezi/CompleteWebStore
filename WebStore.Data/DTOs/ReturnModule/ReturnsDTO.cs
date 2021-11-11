using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Data.DTOs.ReturnModule
{
    public class ReturnsDTO
    {
        public System.Guid Id { get; set; }
        public System.Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string CreateById { get; set; }
        public System.DateTime CreateDate { get; set; }
    }
}
