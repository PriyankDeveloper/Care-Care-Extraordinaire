using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Models
{
    public class WarrantyViewModel
    {        
        public long WarrantyId { get; set; }
        public long VehicleId { get; set; }
        public string WarrantyProvider { get; set; }
        public Nullable<System.DateTime> WarrantyStartDate { get; set; }
        public Nullable<System.DateTime> WarrantyExpirationDate { get; set; }
        public string WarrantyCoverage { get; set; }
        public Nullable<decimal> WarrantyCost { get; set; }
        public long OwnerId { get; set; }
    }
}