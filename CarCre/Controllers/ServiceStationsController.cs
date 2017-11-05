using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarCare.CarCareDatabase;
using CarCare.BusinessLogic;
using CarCare.Models;
using AutoMapper;


namespace CarCare.Controllers
{
    public class ServiceStationsController : Controller
    {
        private IBusinessInterface BusinessInterface;

        public ServiceStationsController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }

        // GET: Vehicle
        public ActionResult Index()
        {
            var ServiceStationsList = BusinessInterface.GetAllServiceStations();
            return View(ServiceStationsList);
        }

        //Edit vehicle
        public ActionResult EditServiceStation(int ServiceStationsId)
        {
            ServiceStationViewModel ServiceStation = BusinessInterface.GetAllServiceStations().FirstOrDefault(i => i.ServiceStationId == ServiceStationsId);

            return PartialView("EditServiceStation", ServiceStation);
        }

        //Add New Vehicle
        public ActionResult AddNewServiceStation()
        {
            return PartialView("AddNewServiceStation", new ServiceStationViewModel());
        }

        //Save Vehicle
        [HttpPost]
        public ActionResult SaveServiceStation(ServiceStationViewModel ServiceStation)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ServiceStationViewModel, CarCareDatabase.ServiceStation>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new ServiceStationViewModel();
            var dest = mapper.Map<ServiceStationViewModel, CarCareDatabase.ServiceStation>(ServiceStation);

            //dest.CreateDate = DateTime.Now;
            //dest.LastModifiedDate = DateTime.Now;
            //dest.OwnerId = 1;
            var modelData = BusinessInterface.SaveServiceStation(dest);
            return Redirect("Index");
        }

        //Delete Vehicle
        public ActionResult DeleteServiceStation(int ServiceStationId)
        {
            BusinessInterface.DeleteVehicle(ServiceStationId);
            return Redirect("Index");
        }
    }
}
