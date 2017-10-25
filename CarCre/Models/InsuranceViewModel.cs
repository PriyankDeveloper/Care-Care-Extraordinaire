using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Models
{
    public class InsuranceViewModel
    {
        public long InsuranceId { get; set; }
        public long VehicleId { get; set; }
        public string InsuranceProvider { get; set; }
        public Nullable<System.DateTime> InsuranceStartDate { get; set; }
        public Nullable<System.DateTime> InsuranceExpirationDate { get; set; }
        public string InsuranceCoverage { get; set; }
        public Nullable<decimal> InsuranceCost { get; set; }
        public long OwnerId { get; set; }
    }
}