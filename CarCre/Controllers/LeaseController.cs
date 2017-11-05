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
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var leaseRecords = BusinessInterface.GetAllLeaseRecords(userId);
            var lease = leaseRecords.ToList();
            var viewModel = MapViewModel(lease);

            return View(viewModel);
            
        }

        //Add new Lease Record
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
            
            return PartialView("AddLease", new LeaseRecordViewModel());
        }

        //Edit Lease Record
        public ActionResult EditLease(long leaseId)
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var leaseRecords = BusinessInterface.GetAllLeaseRecords(userId);
            var leaseRecord = leaseRecords.FirstOrDefault(i => i.LeaseId == leaseId);

            var viewModel = MapViewModel(new List<CarCareDatabase.LeaseRecord> { leaseRecord });

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
            
            return PartialView("EditLease", viewModel.FirstOrDefault());

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
                LeaseRecordViewModel vm = new LeaseRecordViewModel();
                vm.LeaseId = item.LeaseId;
                vm.VehicleId = item.VehicleId;
                vm.Vehicles = vList;
                vm.LeaseStartDate = item.LeaseStartDate;
                vm.LeaseTerm = item.LeaseTerm;
                vm.MonthlyPayment = item.MonthlyPayment;
                vm.Company = item.Company;
                vm.MoneyFactor = item.MoneyFactor;
                vm.MilesDrivenPerYear = item.MilesDrivenPerYear;
                vm.AcquistionFee = item.AcquistionFee;
                vm.SecurityDeposit = item.SecurityDeposit;
                vm.LeaseNotes = item.LeaseNotes;
                
                ListofViewModel.Add(vm);
            }
            return ListofViewModel;
            
        }

    }
}