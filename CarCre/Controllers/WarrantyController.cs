﻿using System;
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
            var warrantyList = BusinessInterface.GetAllWarrantyRecords();

            return View(warrantyList);
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
        public ActionResult EditWarranty(long warrantyId)
        {
            WarrantyViewModel warrantyRecord = BusinessInterface.GetAllWarrantyRecords().FirstOrDefault(i => i.WarrantyId == warrantyId);
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

            return PartialView("EditWarranty", warrantyRecord);

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

            if ( dest.WarrantyStartDate == null )
                dest.WarrantyStartDate = DateTime.Now;
            var modelData = BusinessInterface.SaveWarrantyRecord(dest);
            BusinessInterface.SaveWarrantyRecord(dest);
            return Redirect("Index");
        }

        //Delete Warranty Record
        public ActionResult DeleteWarranty(long warrantyId)
        {
            BusinessInterface.DeleteWarrantyRecord(warrantyId);
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
                    //OwnerId = item.Vehicle.OwnerId
                });
            }

            return ListofViewModel;
        }
    }
}
