using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarCare.Models
{
    public class VehicleViewModel
    {
        public long VehicleId { get; set; }
        public long OwnerId { get; set; }
        public string VehicleMark { get; set; }
        public string VehicleModel { get; set; }
        public string VechicleYear { get; set; }
        public string VechicleDealer { get; set; }
        public string VINNumber { get; set; }
    }
}