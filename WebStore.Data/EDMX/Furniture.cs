//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebStore.Data.EDMX
{
    using System;
    using System.Collections.Generic;
    
    public partial class Furniture
    {
        public System.Guid Id { get; set; }
        public string ItemName { get; set; }
        public string ItemNumber { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public long Status { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
