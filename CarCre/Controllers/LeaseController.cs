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
    public class LeaseController : Controller
    {
        private IBusinessInterface BusinessInterface;

        public LeaseController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }


        // GET: Lease
        public ActionResult Index()
        {
            var leaseList = BusinessInterface.GetAllLeaseRecords();

            return View(leaseList);
        }

        //Add new Lease Record
        public ActionResult AddNewRecord()
        {
            var allVehicle = BusinessInterface.GetAllVehicles().ToList();
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

            //return PartialView("AddNewVehicle", new VehicleViewModel());
            return PartialView("AddLease", new LeaseRecordViewModel());
        }

        //Edit Lease Record
        public ActionResult EditLease(long leaseId)
        {
            LeaseRecordViewModel leaseRecord = BusinessInterface.GetAllLeaseRecords().FirstOrDefault(i => i.LeaseId == leaseId);
            var allVehicle = BusinessInterface.GetAllVehicles().ToList();
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

            return PartialView("EditLease", leaseRecord);

        }

        //Save Lease Record
        [HttpPost]
        public ActionResult SaveLease(LeaseRecordViewModel model)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<LeaseRecordViewModel, CarCareDatabase.LeaseRecord>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new LeaseRecordViewModel();
            var dest = mapper.Map<LeaseRecordViewModel, CarCareDatabase.LeaseRecord>(model);

            dest.LeaseStartDate = DateTime.Now;
            var modelData = BusinessInterface.SaveLeaseRecord(dest);
            return Redirect("Index");
        }

        //Delete Lease Record
        public ActionResult DeleteLease(long leaseId)
        {
            BusinessInterface.DeleteLeaseRecord(leaseId);
            return Redirect("Index");
        }


        private List<LeaseRecordViewModel> MapViewModel(List<CarCareDatabase.LeaseRecord> dbModel)
        {
            List<LeaseRecordViewModel> ListofViewModel = new List<LeaseRecordViewModel>();

            foreach (var item in dbModel)
            {
                ListofViewModel.Add(new LeaseRecordViewModel()
                {
                    LeaseId = item.LeaseId,
                    VehicleId = item.VehicleId,
                    LeaseStartDate = item.LeaseStartDate,
                    LeaseTerm = item.LeaseTerm,
                    MonthlyPayment = item.MonthlyPayment,
                    Company = item.Company,
                    MoneyFactor = item.MoneyFactor,
                    MilesDrivenPerYear = item.MilesDrivenPerYear,
                    AcquistionFee = item.AcquistionFee,
                    SecurityDeposit = item.SecurityDeposit,
                    LeaseNotes = item.LeaseNotes
                });
            }

            return ListofViewModel;
        }

    }
}