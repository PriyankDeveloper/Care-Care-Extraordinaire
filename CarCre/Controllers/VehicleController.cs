using AutoMapper;
using CarCare.BusinessLogic;
using CarCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Controllers
{
    public class VehicleController : Controller
    {
        private IBusinessInterface BusinessInterface;

        public VehicleController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }

        // GET: Vehicle
        public ActionResult Index()
        {
            var vehicleList = BusinessInterface.GetAllVehicles();
            return View(vehicleList);
        }

        //Edit vehicle
        public ActionResult EditVehicle(int vehicleId)
        {
            VehicleViewModel vehicle = BusinessInterface.GetAllVehicles().FirstOrDefault(i=>i.VehicleId == vehicleId);

            return PartialView("EditVehicle", vehicle);
        }

        //Add New Vehicle
        public ActionResult AddNewVehicle()
        {
            return PartialView("AddNewVehicle", new VehicleViewModel());
        }

        //Save Vehicle
        [HttpPost]
        public ActionResult SaveVehicle(VehicleViewModel model)
        {
            var config = new MapperConfiguration(cfg => { 
                cfg.CreateMap<VehicleViewModel, CarCareDatabase.Vehicle>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new VehicleViewModel();
            var dest = mapper.Map<VehicleViewModel, CarCareDatabase.Vehicle>(model);

           var modelData = BusinessInterface.SaveVehicle(dest);
            return Redirect("Index");
        }

        //Delete Vehicle
        public ActionResult DeleteVehicle(int vehicleId)
        {
            BusinessInterface.DeleteVehicle(vehicleId);
            return Redirect("Index");
        }
    }
}