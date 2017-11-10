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
    public class RepairController : Controller
    {
        private IBusinessInterface BusinessInterface;

        public RepairController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }
        

        // GET: Repair
        public ActionResult Index()
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var repairRecords = BusinessInterface.GetAllRepairRecords(userId);
            var repair = repairRecords.ToList();
            var viewModel = MapViewModel(repair);

            return View(viewModel);
        }

        //Add new Repair Record
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
            return PartialView("AddRepair", new RepairRecordViewModel());
        }

        //Edit Repair Record
        public ActionResult EditRepair(long repairId)
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var repairRecords = BusinessInterface.GetAllRepairRecords(userId);
            var repairRecord = repairRecords.FirstOrDefault(i => i.RepairId == repairId);
            
            var viewModel = new RepairRecordViewModel();
            if ( repairRecord != null)
            {
                viewModel = MapViewModel(new List<CarCareDatabase.RepairRecord> { repairRecord }).FirstOrDefault();
            }
            

            ViewBag.RepairDate = viewModel.RepairDate != null
                                    ? viewModel.RepairDate.ToString("yyyy-MM-dd")
                                    : DateTime.Now.ToString("yyyy-MM-dd");

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
            
            return PartialView("AddRepair", viewModel);

        }

        //Save Repair Record
        [HttpPost]
        public ActionResult SaveRepair(RepairRecordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<RepairRecordViewModel, CarCareDatabase.RepairRecord>();
                });

                IMapper mapper = config.CreateMapper();
                var source = new RepairRecordViewModel();
                var dest = mapper.Map<RepairRecordViewModel, CarCareDatabase.RepairRecord>(model);

                dest.RepairDate = dest.RepairDate.HasValue ? dest.RepairDate : DateTime.Now;
                var modelData = BusinessInterface.SaveRepairRecord(dest);
            }
            
            return Redirect("Index");
        }

        //Delete Repair Record
        public ActionResult DeleteRepair(long repairId)
        {
            BusinessInterface.DeleteRepairRecord(repairId);
            return Redirect("Index");
        }
        
        private List<RepairRecordViewModel> MapViewModel(List<CarCareDatabase.RepairRecord> dbModel)
        {
            List<RepairRecordViewModel> ListofViewModel = new List<RepairRecordViewModel>();
            
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
                RepairRecordViewModel vm = new RepairRecordViewModel();
                vm.RepairId = item.RepairId;
                vm.RepairShortDesc = item.RepairShortDesc;
                vm.RepairDate = Convert.ToDateTime(item.RepairDate);
                vm.RepairStatus = item.RepairStatus;
                vm.RepairStationId = item.RepairStationId;
                vm.RepairCompleteDate = item.RepairCompleteDate;
                vm.RepairCost = item.RepairCost;
                vm.RepairDetails = item.RepairDetails;
                vm.Vehicles = vList;
                vm.OwnerId = item.Vehicle.OwnerId;
                vm.VechicleDealer = item.Vehicle.VechicleDealer;
                vm.VechicleYear = item.Vehicle.VechicleYear;
                vm.VehicleId = item.VehicleId;
                vm.VehicleMark = item.Vehicle.VehicleMark;
                vm.VehicleModel = item.Vehicle.VehicleModel;
                vm.VINNumber = item.Vehicle.VINNumber;
                vm.StationCity = item.ServiceStation.City;
                vm.StationOwnedBy = item.ServiceStation.OwnedBy;
                vm.StationState = item.ServiceStation.State;
                vm.StationStreetAddress = item.ServiceStation.StreetAddress;
                vm.StationZipCode = item.ServiceStation.ZipCode;

                ListofViewModel.Add(vm);
            }
            return ListofViewModel;
        }
    }
}