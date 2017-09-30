using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarCare.Models
{
    public class ServiceStationViewModel
    {
        public long ServiceStationId { get; set; }
        public string OwnedBy { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<int> ZipCode { get; set; }
        public string StreetAddress { get; set; }
    }
}