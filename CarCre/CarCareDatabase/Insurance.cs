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
    
    public partial class Insurance
    {
        public long InsuranceId { get; set; }
        public long VehicleId { get; set; }
        public Nullable<long> OwnerId { get; set; }
        public Nullable<System.DateTime> InsuranceStartDate { get; set; }
        public Nullable<System.DateTime> InsuranceExpirationDate { get; set; }
        public string InsuranceProvider { get; set; }
        public Nullable<decimal> InsuranceCost { get; set; }
        public string InsuranceCoverage { get; set; }
    
        public virtual Vehicle Vehicle { get; set; }
    }
}
