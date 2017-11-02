using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CarCare.BusinessLogic;
using CarCare.Models;

namespace CarCare.Controllers
{
    public class WarrantyController : Controller
    {
        private IBusinessInterface BusinessInterface;

        public WarrantyController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }


        // GET: Warranty
        public ActionResult Index()
        {
            var insuranceList = BusinessInterface.GetAllWarrantyRecords();

            return View(insuranceList);
        }

        //Add new Warranty Record
        public ActionResult AddNewRecord()
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var allVehicle = BusinessInterface.GetAllVehicles(userId).ToList();
            //var allServiceStation = BusinessInterface.GetAllServiceStations().ToList();

            List<SelectListItem> vList = new List<SelectListItem>();
            //List<SelectListItem> sList = new List<SelectListItem>();

            foreach (var vehicle in allVehicle)
            {
                vList.Add(new SelectListItem
                {
                    Text = vehicle.VINNumber,
                    Value = vehicle.VehicleId.ToString()
                });
            }
            ViewBag.Vehicles = vList;

            /*
            foreach (var station in allServiceStation)
            {
                sList.Add(new SelectListItem
                {
                    Text = station.StreetAddress,
                    Value = station.ServiceStationId.ToString()
                });
            }
            ViewBag.ServiceStations = sList;
            */

            return PartialView("AddWarranty", new WarrantyViewModel());
        }

        //Edit Warranty Record
        public ActionResult EditWarranty(long insuranceId)
        {
            WarrantyViewModel insuranceRecord = BusinessInterface.GetAllWarrantyRecords().FirstOrDefault(i => i.WarrantyId == insuranceId);
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var allVehicle = BusinessInterface.GetAllVehicles(userId).ToList();
            //var allServiceStation = BusinessInterface.GetAllServiceStations().ToList();

            List<SelectListItem> vList = new List<SelectListItem>();
            //List<SelectListItem> sList = new List<SelectListItem>();

            foreach (var vehicle in allVehicle)
            {
                vList.Add(new SelectListItem
                {
                    Text = vehicle.VINNumber,
                    Value = vehicle.VehicleId.ToString()
                });
            }
            ViewBag.Vehicles = vList;

            /*
            foreach (var station in allServiceStation)
            {
                sList.Add(new SelectListItem
                {
                    Text = station.StreetAddress,
                    Value = station.ServiceStationId.ToString()
                });
            }
            ViewBag.ServiceStations = sList;
            */

            return PartialView("EditWarranty", insuranceRecord);

        }

        //Save Warranty Record
        [HttpPost]
        public ActionResult SaveWarranty(WarrantyViewModel model)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<WarrantyViewModel, CarCareDatabase.Warranty>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new WarrantyViewModel();
            var dest = mapper.Map<WarrantyViewModel, CarCareDatabase.Warranty>(model);

            dest.WarrantyStartDate = DateTime.Now;
            var modelData = BusinessInterface.SaveWarrantyRecord(dest);
            return Redirect("Index");
        }

        //Delete Warranty Record
        public ActionResult DeleteWarranty(long insuranceId)
        {
            BusinessInterface.DeleteWarrantyRecord(insuranceId);
            return Redirect("Index");
        }


        private List<WarrantyViewModel> MapViewModel(List<CarCareDatabase.Warranty> dbModel)
        {
            List<WarrantyViewModel> ListofViewModel = new List<WarrantyViewModel>();

            foreach (var item in dbModel)
            {
                ListofViewModel.Add(new WarrantyViewModel()
                {
                    WarrantyId = item.WarrantyId,
                    WarrantyStartDate = item.WarrantyStartDate,
                    WarrantyExpirationDate = item.WarrantyExpirationDate,
                    WarrantyCost = item.WarrantyCost,
                    WarrantyCoverage = item.WarrantyCoverage,
                    //OwnerId = item.Vehicle.OwnerId,

                    //VechicleDealer = item.Vehicle.VechicleDealer,
                    //VechicleYear = item.Vehicle.VechicleYear,
                    //VehicleId = item.VehicleId,
                    //VehicleMark = item.Vehicle.VehicleMark,
                    //VehicleModel = item.Vehicle.VehicleModel,
                    //VINNumber = item.Vehicle.VINNumber,
                    //StationCity = item.ServiceStation.City,
                    //StationOwnedBy = item.ServiceStation.OwnedBy,
                    //StationState = item.ServiceStation.State,
                    //StationStreetAddress = item.ServiceStation.StreetAddress,
                    //StationZipCode = item.ServiceStation.ZipCode
                });
            }

            return ListofViewModel;
        }
    }
}