using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Models
{
    public class InsuranceViewModel
    {
        public long InsuranceId { get; set; }
        [Required(ErrorMessage = "Policy Number is required")]
        public string PolicyNumber { get; set; }
        [Required(ErrorMessage = "Vehicle is required")]
        public long VehicleId { get; set; }
        public List<SelectListItem> Vehicles { get; set; }
        public string InsuranceProvider { get; set; }
        [Required(ErrorMessage = "Start Date is required")]
        public System.DateTime InsuranceStartDate { get; set; }
        public Nullable<System.DateTime> InsuranceExpirationDate { get; set; }
        public string InsuranceCoverage { get; set; }
        public Nullable<decimal> InsuranceCost { get; set; }
        public long OwnerId { get; set; }
    }
}