using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarCare.Models
{
    public class ServiceRecordViewModel
    {
        public long ServiceId { get; set; }
        public long VehicleId { get; set; }
        public long ServiceTypeId { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public Nullable<long> ServiceStationId { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<decimal> ServiceCost { get; set; }
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