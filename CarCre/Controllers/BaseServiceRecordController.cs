
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
            var serviceRecords = BusinessInterface.GetAllServiceRecords();
            if (serviceTypeId != -1) {
                serviceRecords = serviceRecords.Where(i => i.ServiceTypeId == serviceTypeId).ToList();
            } else { 
                serviceRecords = serviceRecords.ToList();
            }
            var viewModel = MapViewModel(serviceRecords);
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
        public ActionResult AddNewRecord(ServiceRecordViewModel serviceRecordViewModel)
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
            return PartialView("AddServiceRecord", serviceRecordViewModel);
        }

        //Edit OilChange ServiceRecord
        public ActionResult EditRecord(long serviceId)
        {
            var serviceRecord = BusinessInterface.GetAllServiceRecords().FirstOrDefault(i => i.ServiceId == serviceId);

            var viewModel = new ServiceRecordViewModel();
            if (serviceRecord != null)
            {
                viewModel = MapViewModel(new List<CarCareDatabase.ServiceRecord> { serviceRecord }).FirstOrDefault();
            }

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