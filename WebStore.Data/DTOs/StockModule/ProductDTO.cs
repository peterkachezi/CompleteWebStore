using System;


namespace WebStore.Data.DTOs.StockModule
{
    public class StockDTO
    {
        public System.Guid Id { get; set; }
        public System.Guid SupplierId { get; set; }
        public System.Guid ProductId { get; set; }
        public string ProductCode { get; set; }
        public string CreatedBy { get; set; }
        public int Quantity { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal CostPrice { get; set; }
        public Nullable<int> ReOrderLevel { get; set; }
        public decimal ExpectedProfit { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public System.DateTime UpdateDate { get; set; }

        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public string CreatedByName { get; set; }
    }
}
