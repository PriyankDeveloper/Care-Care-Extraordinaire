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
    public class InsuranceController : Controller
    {
        private IBusinessInterface BusinessInterface;

        public InsuranceController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }


        // GET: Insurance
        public ActionResult Index()
        {
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var insuranceRecords = BusinessInterface.GetAllInsuranceRecords(userId);
            var insurance = insuranceRecords.ToList();
            var viewModel = MapViewModel(insurance);

            return View(viewModel);
        }

        //Add new Insurance Record
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
            

            return PartialView("AddInsurance", new InsuranceViewModel());
        }

        //Edit Insurance Record
        public ActionResult EditInsurance(long insuranceId)
        {            
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var insuranceRecords = BusinessInterface.GetAllInsuranceRecords(userId);
            var insuranceRecord = insuranceRecords.FirstOrDefault(i => i.InsuranceId == insuranceId);

            var viewModel = new InsuranceViewModel();
            if (insuranceRecord != null)
            {
                viewModel = MapViewModel(new List<CarCareDatabase.Insurance> { insuranceRecord }).FirstOrDefault();
            }
            
            ViewBag.InsuranceStartDate = viewModel.InsuranceStartDate != null
                                    ? viewModel.InsuranceStartDate.ToString("yyyy-MM-dd")
                                    : DateTime.Now.ToString("yyyy-MM-dd");
            

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
            
            return PartialView("AddInsurance", viewModel);

        }

        //Save Insurance Record
        [HttpPost]
        public ActionResult SaveInsurance(InsuranceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<InsuranceViewModel, CarCareDatabase.Insurance>();
                });

                IMapper mapper = config.CreateMapper();
                var source = new InsuranceViewModel();
                var dest = mapper.Map<InsuranceViewModel, CarCareDatabase.Insurance>(model);

                dest.InsuranceStartDate = dest.InsuranceStartDate.HasValue ? dest.InsuranceStartDate : DateTime.Now;

                var modelData = BusinessInterface.SaveInsuranceRecord(dest);
            }

            return Redirect("Index");
        }

        //Delete Insurance Record
        public ActionResult DeleteInsurance(long insuranceId)
        {
            BusinessInterface.DeleteInsuranceRecord(insuranceId);
            return Json("Success", JsonRequestBehavior.AllowGet);
        }


        private List<InsuranceViewModel> MapViewModel(List<CarCareDatabase.Insurance> dbModel)
        {
            List<InsuranceViewModel> ListofViewModel = new List<InsuranceViewModel>();

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
                InsuranceViewModel vm = new InsuranceViewModel();

                vm.InsuranceId = item.InsuranceId;
                vm.PolicyNumber = item.PolicyNumber;
                vm.InsuranceStartDate = Convert.ToDateTime(item.InsuranceStartDate);
                vm.InsuranceExpirationDate = item.InsuranceExpirationDate;
                vm.InsuranceCost = item.InsuranceCost;
                vm.InsuranceCoverage = item.InsuranceCoverage;
                vm.OwnerId = item.Vehicle.OwnerId;
                vm.VehicleId = item.VehicleId;
                vm.Vehicles = vList;
                vm.InsuranceProvider = item.InsuranceProvider;

                ListofViewModel.Add(vm);
            }
            return ListofViewModel;
        }
    }
}