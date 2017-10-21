//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CarCare.CarCareDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Warranty
    {
        public long WarrantyId { get; set; }
        public Nullable<long> VehicleId { get; set; }
        public string WarrantyProvider { get; set; }
        public Nullable<System.DateTime> WarrantyStartDate { get; set; }
        public Nullable<System.DateTime> WarrantyExpirationDate { get; set; }
        public string WarrantyCoverage { get; set; }
        public Nullable<decimal> Cost { get; set; }
    
        public virtual Vehicle Vehicle { get; set; }
    }
}
