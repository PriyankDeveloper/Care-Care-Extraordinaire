using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Models
{
    public class ServiceRecordViewModel
    {
        public long ServiceId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public long VehicleId { get; set; }
        public List<SelectListItem> Vehicles { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public long ServiceTypeId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public System.DateTime ServiceDate { get; set; }
        public string SDate { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public long ServiceStationId { get; set; }
        public List<SelectListItem> ServiceStations { get; set; }
        public Nullable<System.DateTime> CompletedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<decimal> ServiceCost { get; set; }
        public string ServiceNote { get; set; }
        public long OwnerId { get; set; }
        public string VehicleMark { get; set; }
        public string VehicleModel { get; set; }
        public string VechicleYear { get; set; }
        public string VechicleDealer { get; set; }
        public string VINNumber { get; set; }
        public string StationOwnedBy { get; set; }
        public string StationCity { get; set; }
        public string StationState { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public Nullable<int> StationZipCode { get; set; }
        public string StationStreetAddress { get; set; }
        public string ControllerName { get; set; }
    }
}