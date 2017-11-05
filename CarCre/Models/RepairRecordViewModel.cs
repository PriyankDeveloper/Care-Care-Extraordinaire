using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Models
{
    public class RepairRecordViewModel
    {
        public long RepairId { get; set; }
        public long VehicleId { get; set; }
        public List<SelectListItem> Vehicles { get; set; }
        public string RepairShortDesc { get; set; }
        public System.DateTime RepairDate { get; set; }
        public string RepairStatus { get; set; }
        public Nullable<long> RepairStationId { get; set; }
        public Nullable<System.DateTime> RepairCompleteDate { get; set; }
        public Nullable<decimal> RepairCost { get; set; }
        public string RepairDetails { get; set; }

        public long OwnerId { get; set; }
        public string VehicleMark { get; set; }
        public string VehicleModel { get; set; }
        public string VechicleYear { get; set; }
        public string VechicleDealer { get; set; }
        public string VINNumber { get; set; }
        public string StationOwnedBy { get; set; }
        public string StationCity { get; set; }
        public string StationState { get; set; }
        public Nullable<int> StationZipCode { get; set; }
        public string StationStreetAddress { get; set; }
    }
}