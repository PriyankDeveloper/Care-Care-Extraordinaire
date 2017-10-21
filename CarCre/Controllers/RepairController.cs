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
            var repairList = BusinessInterface.GetAllRepairRecords();
            
            return View(repairList);
        }

        //Add new Repair Record
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
            return PartialView("AddRepair", new RepairRecordViewModel());
        }

        //Edit Repair Record
        public ActionResult EditRepair(long repairId)
        {
            RepairRecordViewModel repairRecord = BusinessInterface.GetAllRepairRecords().FirstOrDefault(i => i.RepairId == repairId);
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

            return PartialView("EditRepair", repairRecord);
            
        }

        //Save Repair Record
        [HttpPost]
        public ActionResult SaveRepair(RepairRecordViewModel model)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<RepairRecordViewModel, CarCareDatabase.RepairRecord>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new RepairRecordViewModel();
            var dest = mapper.Map<RepairRecordViewModel, CarCareDatabase.RepairRecord>(model);
            
            dest.RepairDate = DateTime.Now;
            var modelData = BusinessInterface.SaveRepairRecord(dest);
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

            foreach (var item in dbModel)
            {
                ListofViewModel.Add(new RepairRecordViewModel()
                {
                    RepairId = item.RepairId,
                    RepairShortDesc = item.RepairShortDesc,
                    RepairDate = item.RepairDate,
                    RepairStatus = item.RepairStatus,
                    RepairStationId = item.RepairStationId,
                    RepairCompleteDate = item.RepairCompleteDate,
                    RepairCost = item.RepairCost,
                    RepairDetails = item.RepairDetails,

                    OwnerId = item.Vehicle.OwnerId,
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