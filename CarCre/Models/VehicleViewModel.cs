using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarCare.Models
{
    public class VehicleViewModel
    {
        public long VehicleId { get; set; }
        public long OwnerId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string VehicleMark { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string VehicleModel { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string VechicleYear { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string VechicleDealer { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string VINNumber { get; set; }
    }
}