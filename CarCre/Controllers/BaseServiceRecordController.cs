
using AutoMapper;
using CarCare.BusinessLogic;
using CarCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CarCare.Controllers
{
    public class BaseServiceRecordController : Controller
    {
        protected IBusinessInterface BusinessInterface;
        protected int serviceTypeId = -1;

        public BaseServiceRecordController(IBusinessInterface businessInterface, int id)
        {
            BusinessInterface = businessInterface;
            serviceTypeId = id;
        }

        public ActionResult BaseIndex()
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var serviceRecords = BusinessInterface.GetAllServiceRecords(userId);
            if (serviceTypeId != -1) {
                serviceRecords = serviceRecords.Where(i => i.ServiceTypeId == serviceTypeId).ToList();
            } else { 
                serviceRecords = serviceRecords.ToList();
            }
            var viewModel = MapViewModel(serviceRecords);
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

            return View(viewModel);
        }

        //Add new Record
        public ActionResult AddNewRecord(ServiceRecordViewModel serviceRecordViewModel)
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
            return PartialView("AddServiceRecord", serviceRecordViewModel);
        }

        //Edit OilChange ServiceRecord
        public ActionResult EditRecord(long serviceId)
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var serviceRecords = BusinessInterface.GetAllServiceRecords(userId);
            var serviceRecord = serviceRecords.FirstOrDefault(i => i.ServiceId == serviceId);

            var viewModel = new ServiceRecordViewModel();
            if (serviceRecord != null)
            {
                viewModel = MapViewModel(new List<CarCareDatabase.ServiceRecord> { serviceRecord }).FirstOrDefault();
            }

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

            return PartialView("AddServiceRecord", viewModel);
        }

        [HttpPost]
        public ActionResult Save(ServiceRecordViewModel model)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<ServiceRecordViewModel, CarCareDatabase.ServiceRecord>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new ServiceRecordViewModel();
            var dest = mapper.Map<ServiceRecordViewModel, CarCareDatabase.ServiceRecord>(model);

            dest.LastModifiedDate = DateTime.Now;
            dest.ServiceTypeId = serviceTypeId;
            dest.ServiceDate = dest.ServiceDate.HasValue ? dest.ServiceDate : DateTime.Now;
            var modelData = BusinessInterface.SaveServiceRecord(dest);
            return Redirect("Index");
        }

        public ActionResult Delete(long serviceId)
        {
            BusinessInterface.DeleteServiceRecord(serviceId);
            return Redirect("Index");
        }

        private List<ServiceRecordViewModel> MapViewModel(List<CarCareDatabase.ServiceRecord> dbModel)
        {
            List<ServiceRecordViewModel> ListofViewModel = new List<ServiceRecordViewModel>();

            foreach (var item in dbModel)
            {
                ServiceRecordViewModel vm = new ServiceRecordViewModel();
                vm.CompletedDate = item.CompletedDate;
                vm.LastModifiedDate = item.LastModifiedDate;
                vm.OwnerId = item.Vehicle.OwnerId;
                vm.ServiceCost = item.ServiceCost;
                vm.ServiceDate = item.ServiceDate;
                vm.ServiceId = item.ServiceId;
                vm.ServiceStationId = item.ServiceStationId;
                vm.ServiceTypeId = item.ServiceTypeId;
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