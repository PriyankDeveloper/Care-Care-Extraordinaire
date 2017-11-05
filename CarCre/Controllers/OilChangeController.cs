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
    public class OilChangeController : Controller
    {
        private IBusinessInterface BusinessInterface;

        public OilChangeController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }

        // GET: OilChange
        public ActionResult Index()
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var serviceRecords = BusinessInterface.GetAllServiceRecords(userId);
            var oilChange = serviceRecords.Where(i => i.ServiceTypeId == 1).ToList();
            var viewModel = MapViewModel(oilChange);
     
            return View(viewModel);
        }

        //Add new Record
        public ActionResult AddNewRecord()
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var allVehicle = BusinessInterface.GetAllVehicles(userId).ToList();
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
            return PartialView("AddOilChange",new ServiceRecordViewModel());
        }

        //Edit OilChange ServiceRecord
        public ActionResult EditOilChange(long serviceId)
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var serviceRecords = BusinessInterface.GetAllServiceRecords(userId);
            var serviceRecord = serviceRecords.FirstOrDefault(i => i.ServiceId == serviceId);
        
            var viewModel = MapViewModel(new List<CarCareDatabase.ServiceRecord> { serviceRecord});
            
            var allVehicle = BusinessInterface.GetAllVehicles(userId).ToList();
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

            return PartialView("AddOilChange", viewModel.FirstOrDefault());
        }

        //Save OilChangeRecord
        [HttpPost]
        public ActionResult SaveOilChange(ServiceRecordViewModel model)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ServiceRecordViewModel, CarCareDatabase.ServiceRecord>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new ServiceRecordViewModel();
            var dest = mapper.Map<ServiceRecordViewModel, CarCareDatabase.ServiceRecord>(model);

            dest.LastModifiedDate = DateTime.Now;
            dest.ServiceTypeId = 1;
            dest.ServiceDate = DateTime.Now;
            var modelData = BusinessInterface.SaveServiceRecord(dest);
            return Redirect("Index");
        }

        //Delete OilChangeRecord
        public ActionResult DeleteOilChange(long serviceId)
        {
            BusinessInterface.DeleteServiceRecord(serviceId);
            return Redirect("Index");
        }


        private List<ServiceRecordViewModel> MapViewModel(List<CarCareDatabase.ServiceRecord> dbModel)
        {
            List<ServiceRecordViewModel> ListofViewModel = new List<ServiceRecordViewModel>();
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var allVehicle = BusinessInterface.GetAllVehicles(userId).ToList();
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

            foreach (var station in allServiceStation)
            {
                sList.Add(new SelectListItem
                {
                    Text = station.StreetAddress,
                    Value = station.ServiceStationId.ToString()
                });
            }

            foreach (var item in dbModel)
            {
                ServiceRecordViewModel vm = new ServiceRecordViewModel();
                vm.CompletedDate = item.CompletedDate;
                vm.LastModifiedDate = item.LastModifiedDate;
                vm.OwnerId = item.Vehicle.OwnerId;
                vm.Vehicles = vList;
                vm.ServiceStations = sList;
                vm.ServiceCost = item.ServiceCost;
                vm.ServiceDate = item.ServiceDate;
                vm.ServiceId = item.ServiceId;
                vm.ServiceStationId = item.ServiceStationId;
                vm.ServiceTypeId = item.ServiceTypeId;
                vm.ServiceNote = item.ServiceNote;
                if (item.Vehicle != null)
                {
                    vm.VechicleDealer = item.Vehicle.VechicleDealer;
                    vm.VechicleYear = item.Vehicle.VechicleYear;
                    vm.VehicleMark = item.Vehicle.VehicleMark;
                    vm.VehicleModel = item.Vehicle.VehicleModel;
                    vm.VINNumber = item.Vehicle.VINNumber;
                }
                if (item.ServiceStation != null)
                {
                    vm.VehicleId = item.VehicleId;
                    vm.StationCity = item.ServiceStation.City;
                    vm.StationOwnedBy = item.ServiceStation.OwnedBy;
                    vm.StationState = item.ServiceStation.State;
                    vm.StationStreetAddress = item.ServiceStation.StreetAddress;
                    vm.StationZipCode = item.ServiceStation.ZipCode;
                }

                ListofViewModel.Add(vm);
            }
            return ListofViewModel;
        }

    }
}
