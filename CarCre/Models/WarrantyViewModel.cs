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
        [Required]
        public long VehicleId { get; set; }
        public List<SelectListItem> Vehicles { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string WarrantyProvider { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public Nullable<System.DateTime> WarrantyStartDate { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public Nullable<System.DateTime> WarrantyExpirationDate { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string WarrantyCoverage { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public Nullable<decimal> WarrantyCost { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public long OwnerId { get; set; }
    }
}