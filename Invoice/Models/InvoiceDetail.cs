//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Invoice.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class InvoiceDetail
    {
        public int InvoiceDetailID { get; set; }
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }
        public int Qty { get; set; }
        public Nullable<float> Price { get; set; }
    
        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}
