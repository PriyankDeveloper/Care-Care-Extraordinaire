using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Models
{
    public class LeaseRecordViewModel
    {
        public long LeaseId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public long VehicleId { get; set; }
        public string VINNumber { get; set; }
        public List<SelectListItem> Vehicles { get; set; }
        public System.DateTime LeaseStartDate { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public int LeaseTerm { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public decimal MonthlyPayment { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Company { get; set; }
        public Nullable<decimal> MoneyFactor { get; set; }
        public Nullable<int> MilesDrivenPerYear { get; set; }
        public Nullable<decimal> AcquistionFee { get; set; }
        public Nullable<decimal> SecurityDeposit { get; set; }
        public string LeaseNotes { get; set; }
    }
}