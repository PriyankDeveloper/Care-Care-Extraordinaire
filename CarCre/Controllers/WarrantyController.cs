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
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var warrantyRecords = BusinessInterface.GetAllWarrantyRecords(userId);
            var warranty = warrantyRecords.ToList();
            var viewModel = MapViewModel(warranty);

            return View(viewModel);
        }

        //Add new Warranty Record
        public ActionResult AddNewRecord()
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var allVehicle = BusinessInterface.GetAllVehicles(userId).ToList();

            List<SelectListItem> vList = new List<SelectListItem>();

            foreach (var vehicle in allVehicle)
            {
                vList.Add(new SelectListItem
                {
                    Text = vehicle.VINNumber,
                    Value = vehicle.VehicleId.ToString()
                });
            }
            ViewBag.Vehicles = vList;
            
            return PartialView("AddWarranty", new WarrantyViewModel());
        }

        //Edit Warranty Record
        public ActionResult EditWarranty(long warrantyId)
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var warrantyRecords = BusinessInterface.GetAllWarrantyRecords(userId);
            var warrantyRecord = warrantyRecords.FirstOrDefault(i => i.WarrantyId == warrantyId);

            var viewModel = MapViewModel(new List<CarCareDatabase.Warranty> { warrantyRecord });

            var allVehicle = BusinessInterface.GetAllVehicles(userId).ToList();

            List<SelectListItem> vList = new List<SelectListItem>();

            foreach (var vehicle in allVehicle)
            {
                vList.Add(new SelectListItem
                {
                    Text = vehicle.VINNumber,
                    Value = vehicle.VehicleId.ToString()
                });
            }
            ViewBag.Vehicles = vList;
            
            return PartialView("EditWarranty", viewModel.FirstOrDefault());

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

            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var allVehicle = BusinessInterface.GetAllVehicles(userId).ToList();

            List<SelectListItem> vList = new List<SelectListItem>();

            foreach (var vehicle in allVehicle)
            {
                vList.Add(new SelectListItem
                {
                    Text = vehicle.VINNumber,
                    Value = vehicle.VehicleId.ToString()
                });
            }


            foreach (var item in dbModel)
            {
                WarrantyViewModel vm = new WarrantyViewModel();

                vm.WarrantyId = item.WarrantyId;
                vm.WarrantyStartDate = item.WarrantyStartDate;
                vm.WarrantyExpirationDate = item.WarrantyExpirationDate;
                vm.WarrantyCost = item.WarrantyCost;
                vm.WarrantyCoverage = item.WarrantyCoverage;
                vm.VehicleId = item.VehicleId;
                vm.Vehicles = vList;
                vm.OwnerId = item.Vehicle.OwnerId;
                vm.WarrantyProvider = item.WarrantyProvider;
                
                ListofViewModel.Add(vm);
            }
            return ListofViewModel;
        }
    }
}
