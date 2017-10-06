﻿using AutoMapper;
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
           var oilChange = BusinessInterface.GetAllServiceRecords().Where(i => i.ServiceTypeId == 1).ToList();
           var viewModel = MapViewModel(oilChange);
           return View(viewModel);
        }

        public ActionResult AddNewRecord()
        {
            return PartialView("AddOilChange",new ServiceRecordViewModel());
        }

        //Edit OilChange ServiceRecord
        public ActionResult EditOilChange(long serviceId)
        {
           var serviceRecord = BusinessInterface.GetAllServiceRecords().FirstOrDefault(i => i.ServiceId == serviceId);
        
            var viewModel = MapViewModel(new List<CarCareDatabase.ServiceRecord> { serviceRecord});

            return PartialView("EditOilChange", viewModel.FirstOrDefault());
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

            foreach(var item in dbModel)
            {
                ListofViewModel.Add(new ServiceRecordViewModel() {
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