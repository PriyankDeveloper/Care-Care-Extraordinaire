using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Models
{
    public class WarrantyViewModel
    {        
        public long WarrantyId { get; set; }
        [Required(ErrorMessage = "Policy Number is required")]
        public string PolicyNumber { get; set; }
        [Required(ErrorMessage = "Vehicle is required")]
        public long VehicleId { get; set; }
        public string VINNumber { get; set; }
        public List<SelectListItem> Vehicles { get; set; }
        public string WarrantyProvider { get; set; }
        public System.DateTime WarrantyStartDate { get; set; }
        public Nullable<System.DateTime> WarrantyExpirationDate { get; set; }
        public string WarrantyCoverage { get; set; }
        public Nullable<decimal> WarrantyCost { get; set; }
        public long OwnerId { get; set; }
    }
}