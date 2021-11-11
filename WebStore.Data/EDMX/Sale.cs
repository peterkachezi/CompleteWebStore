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
    
    public partial class Sale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sale()
        {
            this.SalesDetails = new HashSet<SalesDetail>();
        }
    
        public System.Guid Id { get; set; }
        public Nullable<System.Guid> CustomerId { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string CreatedBy { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderNumber { get; set; }
        public decimal Change { get; set; }
        public decimal CashPaid { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Customer Customer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalesDetail> SalesDetails { get; set; }
    }
}