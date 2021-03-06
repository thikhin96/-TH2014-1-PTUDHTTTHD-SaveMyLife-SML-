//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        public int idBill { get; set; }
        public Nullable<decimal> purchase { get; set; }
        public Nullable<System.DateTime> createdDate { get; set; }
        public string description { get; set; }
        public Nullable<int> idDeliveryOrder { get; set; }
        public Nullable<int> idStaff { get; set; }
        public Nullable<int> idDistributor { get; set; }
    
        public virtual DeliveryOrder DeliveryOrder { get; set; }
        public virtual Distributor Distributor { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
