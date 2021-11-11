

namespace WebStore.Data.DTOs.FurnitureModule
{
    public class FurnitureDTO
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string NewCreateDate { get { return CreateDate.ToShortDateString(); } }
        public string ItemNumber { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public string UpdatedBy { get; set; }
        public long Status { get; set; }
        public string StatusDescription { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public string ItemName { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }

    }
}
