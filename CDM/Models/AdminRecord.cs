//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CDM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AdminRecord
    {
        public int ID { get; set; }
        public Nullable<int> Transaction_ID { get; set; }
        public Nullable<int> Order_ID { get; set; }
        public Nullable<int> Customer_ID { get; set; }
    
        public virtual CustomerTB CustomerTB { get; set; }
        public virtual OrderTB OrderTB { get; set; }
        public virtual TransactionTB TransactionTB { get; set; }
    }
}
