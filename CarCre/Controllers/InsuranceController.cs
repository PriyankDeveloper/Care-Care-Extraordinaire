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
    public class InsuranceController : Controller
    {
        private IBusinessInterface BusinessInterface;

        public InsuranceController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }


        // GET: Insurance
        public ActionResult Index()
        {
            var insuranceList = BusinessInterface.GetAllInsuranceRecords();

            return View(insuranceList);
        }

        //Add new Insurance Record
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

            return PartialView("AddInsurance", new InsuranceViewModel());
        }

        //Edit Insurance Record
        public ActionResult EditInsurance(long insuranceId)
        {
            InsuranceViewModel insuranceRecord = BusinessInterface.GetAllInsuranceRecords().FirstOrDefault(i => i.InsuranceId == insuranceId);
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

            return PartialView("EditInsurance", insuranceRecord);

        }

        //Save Insurance Record
        [HttpPost]
        public ActionResult SaveInsurance(InsuranceViewModel model)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<InsuranceViewModel, CarCareDatabase.Insurance>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new InsuranceViewModel();
            var dest = mapper.Map<InsuranceViewModel, CarCareDatabase.Insurance>(model);

            dest.InsuranceStartDate = DateTime.Now;
            var modelData = BusinessInterface.SaveInsuranceRecord(dest);
            return Redirect("Index");
        }

        //Delete Insurance Record
        public ActionResult DeleteInsurance(long insuranceId)
        {
            BusinessInterface.DeleteInsuranceRecord(insuranceId);
            return Redirect("Index");
        }


        private List<InsuranceViewModel> MapViewModel(List<CarCareDatabase.Insurance> dbModel)
        {
            List<InsuranceViewModel> ListofViewModel = new List<InsuranceViewModel>();

            foreach (var item in dbModel)
            {
                ListofViewModel.Add(new InsuranceViewModel()
                {
                    InsuranceId = item.InsuranceId,
                    InsuranceStartDate = item.InsuranceStartDate,
                    InsuranceExpirationDate = item.InsuranceExpirationDate,
                    InsuranceCost = item.InsuranceCost,
                    InsuranceCoverage = item.InsuranceCoverage,
                    OwnerId = item.Vehicle.OwnerId,
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