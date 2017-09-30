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
    }
}