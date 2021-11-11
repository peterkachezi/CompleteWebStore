using System;

namespace WebStore.Data.DTOs.ProductModule
{
    public class ProductDTO
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public int NewQuantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int ReOrderLevel { get; set; }
        public decimal ExpectedProfit { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string UOM { get; set; }
        public bool Status { get; set; }

        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public System.Guid ProductId { get; set; }
        public string ProductName { get; set; }
         public string ProductUnit { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid PackagingId { get; set; }
        public Guid UnitOfMeasureId { get; set; }
        public string PackagingName { get; set; }

        public System.DateTime ExpiryDate { get; set; }
    }
}
