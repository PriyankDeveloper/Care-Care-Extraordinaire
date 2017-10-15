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
    public class TuneUpController : Controller
    {
        private IBusinessInterface BusinessInterface;

        public TuneUpController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }

        // GET: TuneUp
        public ActionResult Index()
        {
            var TuneUp = BusinessInterface.GetAllServiceRecords().Where(i => i.ServiceTypeId == 3).ToList();
            var viewModel = MapViewModel(TuneUp);
            var allVehicle = BusinessInterface.GetAllVehicles().ToList();
            var allServiceStation = BusinessInterface.GetAllServiceStations().ToList();

            List<SelectListItem> vList = new List<SelectListItem>();
            List<SelectListItem> sList = new List<SelectListItem>();

            foreach (var vehicle in allVehicle)
            {
                vList.Add(new SelectListItem
                {
                    Text = vehicle.VINNumber,
                    Value = vehicle.VehicleId.ToString()
                });
            }
            ViewBag.Vehicles = vList;

            foreach (var station in allServiceStation)
            {
                sList.Add(new SelectListItem
                {
                    Text = station.StreetAddress,
                    Value = station.ServiceStationId.ToString()
                });
            }

            ViewBag.ServiceStations = sList;

            return View(viewModel);
        }

        //Add new Record
        public ActionResult AddNewRecord()
        {
            var allVehicle = BusinessInterface.GetAllVehicles().ToList();
            var allServiceStation = BusinessInterface.GetAllServiceStations().ToList();

            List<SelectListItem> vList = new List<SelectListItem>();
            List<SelectListItem> sList = new List<SelectListItem>();

            foreach (var vehicle in allVehicle)
            {
                vList.Add(new SelectListItem
                {
                    Text = vehicle.VINNumber,
                    Value = vehicle.VehicleId.ToString()
                });
            }
            ViewBag.Vehicles = vList;

            foreach (var station in allServiceStation)
            {
                sList.Add(new SelectListItem
                {
                    Text = station.StreetAddress,
                    Value = station.ServiceStationId.ToString()
                });
            }

            ViewBag.ServiceStations = sList;
            return PartialView("AddTuneUp", new ServiceRecordViewModel());
        }

        //Edit TuneUp ServiceRecord
        public ActionResult EditTuneUp(long serviceId)
        {
            var serviceRecord = BusinessInterface.GetAllServiceRecords().FirstOrDefault(i => i.ServiceId == serviceId);

            var viewModel = MapViewModel(new List<CarCareDatabase.ServiceRecord> { serviceRecord });

            var allVehicle = BusinessInterface.GetAllVehicles().ToList();
            var allServiceStation = BusinessInterface.GetAllServiceStations().ToList();

            List<SelectListItem> vList = new List<SelectListItem>();
            List<SelectListItem> sList = new List<SelectListItem>();

            foreach (var vehicle in allVehicle)
            {
                vList.Add(new SelectListItem
                {
                    Text = vehicle.VINNumber,
                    Value = vehicle.VehicleId.ToString()
                });
            }
            ViewBag.Vehicles = vList;

            foreach (var station in allServiceStation)
            {
                sList.Add(new SelectListItem
                {
                    Text = station.StreetAddress,
                    Value = station.ServiceStationId.ToString()
                });
            }

            ViewBag.ServiceStations = sList;

            return PartialView("EditTuneUp", viewModel.FirstOrDefault());
        }

        //Save TuneUpRecord
        [HttpPost]
        public ActionResult SaveTuneUp(ServiceRecordViewModel model)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ServiceRecordViewModel, CarCareDatabase.ServiceRecord>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new ServiceRecordViewModel();
            var dest = mapper.Map<ServiceRecordViewModel, CarCareDatabase.ServiceRecord>(model);

            dest.LastModifiedDate = DateTime.Now;
            dest.ServiceTypeId = 3;
            dest.ServiceDate = DateTime.Now;
            var modelData = BusinessInterface.SaveServiceRecord(dest);
            return Redirect("Index");
        }

        //Delete OilChangeRecord
        public ActionResult DeleteTuneUp(long serviceId)
        {
            BusinessInterface.DeleteServiceRecord(serviceId);
            return Redirect("Index");
        }


        private List<ServiceRecordViewModel> MapViewModel(List<CarCareDatabase.ServiceRecord> dbModel)
        {
            List<ServiceRecordViewModel> ListofViewModel = new List<ServiceRecordViewModel>();

            foreach (var item in dbModel)
            {
                ListofViewModel.Add(new ServiceRecordViewModel()
                {
                    CompletedDate = item.CompletedDate,
                    LastModifiedDate = item.LastModifiedDate,
                    OwnerId = item.Vehicle.OwnerId,
                    ServiceCost = item.ServiceCost,
                    ServiceDate = item.ServiceDate,
                    ServiceId = item.ServiceId,
                    ServiceStationId = item.ServiceStationId,
                    ServiceTypeId = item.ServiceTypeId,
                    VechicleDealer = item.Vehicle.VechicleDealer,
                    VechicleYear = item.Vehicle.VechicleYear,
                    VehicleId = item.VehicleId,
                    VehicleMark = item.Vehicle.VehicleMark,
                    VehicleModel = item.Vehicle.VehicleModel,
                    VINNumber = item.Vehicle.VINNumber,
                    StationCity = item.ServiceStation.City,
                    StationOwnedBy = item.ServiceStation.OwnedBy,
                    StationState = item.ServiceStation.State,
                    StationStreetAddress = item.ServiceStation.StreetAddress,
                    StationZipCode = item.ServiceStation.ZipCode
                });
            }

            return ListofViewModel;
        }

    }
}