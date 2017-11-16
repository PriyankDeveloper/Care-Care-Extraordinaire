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
    public class VehicleSummaryController : Controller
    {
        private IBusinessInterface BusinessInterface;


        public VehicleSummaryController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }

        public ActionResult Index()
        {
            System.Collections.Specialized.NameValueCollection formHelper;
            formHelper = Request.Form;
            String[] formKeyHelper = formHelper.AllKeys;

            Boolean formCheck = formKeyHelper.Contains("Vehicle Dropdown");

            if (!(formCheck))
            {
                long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
                var serviceRecords = BusinessInterface.GetAllServiceRecords(userId).Where(i => i.ServiceStationId != null).ToList();

                serviceRecords = serviceRecords.ToList();

                //var viewModel = MapViewModel(serviceRecords);



                var allVehicle = BusinessInterface.GetAllVehicles(userId).ToList();
                var allServiceStation = BusinessInterface.GetAllServiceStations().ToList();
                var RepairRecords = BusinessInterface.GetAllRepairRecords(userId).Where(i => i.RepairStationId != null).ToList();

                //var vehicleSummaryRecords = BusinessInterface.GetRecordsSummary(userId).ToList();
                //var viewModel = MapViewModel(vehicleSummaryRecords);

                var viewModel = MapViewModel(RepairRecords, serviceRecords);

                Boolean check1 = helperUpdateMethod(RepairRecords, allVehicle, allServiceStation);
                return View(viewModel);
            }
            else
            {
                return (UpdatePageInfo());
            }

            
        }



        //private List<VehicleSummaryViewModel> MapViewModel(List<CarCareDatabase.VehicleSummary> dbModel)
        private List<VehicleSummaryViewModel> MapViewModel(List<CarCareDatabase.RepairRecord> rModel, List<CarCareDatabase.ServiceRecord> sModel)
        {
            List<VehicleSummaryViewModel> ListofViewModel = new List<VehicleSummaryViewModel>();

            foreach (var item in rModel)
            {
                VehicleSummaryViewModel vm = new VehicleSummaryViewModel();

                if (item.Vehicle != null)
                {
                    vm.OwnerId = item.Vehicle.OwnerId;
                    vm.VechicleDealer = item.Vehicle.VechicleDealer;
                    vm.VechicleYear = item.Vehicle.VechicleYear;
                    vm.VehicleMark = item.Vehicle.VehicleMark;
                    vm.VehicleModel = item.Vehicle.VehicleModel;
                    vm.VINNumber = item.Vehicle.VINNumber;
                }

                
                    vm.RepairId = item.RepairId;
                    vm.RepairDate = item.RepairDate;
                    vm.RepairCost = item.RepairCost;
                    vm.RepairStationId = item.RepairStationId;
                    vm.RepairShortDesc = item.RepairShortDesc;
                    vm.RepairCompleteDate = item.RepairCompleteDate;
                    vm.RepairDetails = item.RepairDetails;
                    vm.RepairStatus = item.RepairStatus;


                vm.CompletedDate = null;
                vm.LastModifiedDate = null;
                vm.ServiceCost = null;
                vm.ServiceDate = null;
                vm.ServiceId = null;
                vm.ServiceStationId = null;
                vm.ServiceTypeId = null;
                vm.ServiceNote = "";

                if (item.ServiceStation != null)
                {
                //    vm.VehicleId = item.VehicleId;
                    vm.StationCity = item.ServiceStation.City;
                    vm.StationOwnedBy = item.ServiceStation.OwnedBy;
                    vm.StationState = item.ServiceStation.State;
                    vm.StationStreetAddress = item.ServiceStation.StreetAddress;
                    vm.StationZipCode = item.ServiceStation.ZipCode;
                }

                ListofViewModel.Add(vm);
            }


            var serviceTypes = BusinessInterface.GetAllServiceTypes().ToList();
            List<CarCareDatabase.ServiceType> temp;

            foreach (var item in sModel)
            {
                VehicleSummaryViewModel vm = new VehicleSummaryViewModel();
                if (item.Vehicle != null)
                {
                    vm.OwnerId = item.Vehicle.OwnerId;
                    vm.VechicleDealer = item.Vehicle.VechicleDealer;
                    vm.VechicleYear = item.Vehicle.VechicleYear;
                    vm.VehicleMark = item.Vehicle.VehicleMark;
                    vm.VehicleModel = item.Vehicle.VehicleModel;
                    vm.VINNumber = item.Vehicle.VINNumber;
                }


                    vm.CompletedDate = item.CompletedDate;
                    vm.LastModifiedDate = item.LastModifiedDate;
                    vm.ServiceCost = item.ServiceCost;
                    vm.ServiceDate = Convert.ToDateTime(item.ServiceDate);
                    vm.ServiceId = item.ServiceId;
                    vm.ServiceStationId = Convert.ToInt64(item.ServiceStationId);
                    vm.ServiceTypeId = item.ServiceTypeId;
                    vm.ServiceNote = item.ServiceNote;

                temp = serviceTypes.Where(i => i.ServiceTypeId == item.ServiceTypeId).ToList();
                vm.ServiceName = temp.FirstOrDefault().ServiceName;

                vm.RepairId = null;
                vm.RepairDate = null;
                vm.RepairCost = null;
                vm.RepairStationId = null;
                vm.RepairShortDesc = "";
                vm.RepairCompleteDate = null;
                vm.RepairDetails = "";
                vm.RepairStatus = "";

                if (item.ServiceStation != null)
                {
                //    vm.VehicleId = item.VehicleId;
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

        //might not need VSV
        [HttpPost]
        //public ActionResult UpdatePageInfo(List<VehicleSummaryViewModel> VSV, string returnurl)
        //public ActionResult UpdatePageInfo(VehicleSummaryViewModel VSV, FormCollection form)
        public ActionResult UpdatePageInfo()
        //public ActionResult UpdatePageInfo(string returnurl)
        {
            //string strVehicleValue = form["Vehicle Dropdown"].ToString();
            string strVehicleValue = Request.Form["Vehicle Dropdown"].ToString();
            List<VehicleSummaryViewModel> viewModel;

            //Request.Form.Remove("Vehicle Dropdown - can't remove, read-only

            //string strVehicleValue = VSV.SelectedVehicle;

            //foreach (var vm in VSV)
            //foreach (var vm in viewModel)
            //{

            //    viewModel.Remove(vm);

            //}

            //long resetUserId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            //var resetServiceRecords = BusinessInterface.GetAllServiceRecords(resetUserId).Where(i => i.ServiceStationId != null).ToList();

            //resetServiceRecords = resetServiceRecords.ToList();

            ////var viewModel = MapViewModel(serviceRecords);



            //var resetAllVehicle = BusinessInterface.GetAllVehicles(resetUserId).ToList();
            //var resetAllServiceStation = BusinessInterface.GetAllServiceStations().ToList();
            //var resetRepairRecords = BusinessInterface.GetAllRepairRecords(resetUserId).Where(i => i.RepairStationId != null).ToList();

            ////var vehicleSummaryRecords = BusinessInterface.GetRecordsSummary(userId).ToList();
            ////var viewModel = MapViewModel(vehicleSummaryRecords);

            //viewModel = MapViewModel(resetRepairRecords, resetServiceRecords);

            //Boolean check1 = helperUpdateMethod(resetRepairRecords, resetAllVehicle, resetAllServiceStation);




            if (!(strVehicleValue.Equals("ALL")))
            {
                long VId = Convert.ToInt64(strVehicleValue);

                long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
                var allVehicle = BusinessInterface.GetAllVehicles(userId).ToList();
                var allServiceStation = BusinessInterface.GetAllServiceStations().ToList();
                var RepairRecords = BusinessInterface.GetAllRepairRecords(userId).Where(i => i.RepairStationId != null).ToList();
                var serviceRecords = BusinessInterface.GetAllServiceRecords(userId).Where(i => i.ServiceStationId != null).ToList();

                allVehicle = allVehicle.Where(i => i.VehicleId == VId).ToList();
                //allVehicle = allVehicle.Where(i => i.VINNumber == strVehicleValue).ToList();


                //List<long> carIdList = new List<long>();
                //allVehicle.ForEach(car => carIdList.Add(car.VehicleId));
                //long VId = carIdList.FirstOrDefault();

                RepairRecords = RepairRecords.Where(i => i.VehicleId == VId).ToList();
                serviceRecords = serviceRecords.Where(i => i.VehicleId == VId).ToList();

                viewModel = MapViewModel(RepairRecords, serviceRecords);

                ////foreach (var vm in VSV)
                //foreach (var vm in viewModel)
                //{
                //    if (vm.VehicleId != VId)
                //    {
                //        viewModel.Remove(vm);
                //    }
                //}

                //Boolean check2 = helperUpdateMethod(RepairRecords, allVehicle, allServiceStation);

                //reset vehicle list
                var resetVehicles = BusinessInterface.GetAllVehicles(userId).ToList();

                Boolean check2 = helperUpdateMethod(RepairRecords, resetVehicles, allServiceStation);
            }
            else
            {
                long resetUserId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
                var resetServiceRecords = BusinessInterface.GetAllServiceRecords(resetUserId).Where(i => i.ServiceStationId != null).ToList();

                resetServiceRecords = resetServiceRecords.ToList();

                //var viewModel = MapViewModel(serviceRecords);



                var resetAllVehicle = BusinessInterface.GetAllVehicles(resetUserId).ToList();
                var resetAllServiceStation = BusinessInterface.GetAllServiceStations().ToList();
                var resetRepairRecords = BusinessInterface.GetAllRepairRecords(resetUserId).Where(i => i.RepairStationId != null).ToList();

                //var vehicleSummaryRecords = BusinessInterface.GetRecordsSummary(userId).ToList();
                //var viewModel = MapViewModel(vehicleSummaryRecords);

                viewModel = MapViewModel(resetRepairRecords, resetServiceRecords);

                Boolean check1 = helperUpdateMethod(resetRepairRecords, resetAllVehicle, resetAllServiceStation);
            }

            //return Redirect(returnurl);

            return View(viewModel);
        }

        private Boolean helperUpdateMethod(List<CarCareDatabase.RepairRecord> rModel, List<Models.VehicleViewModel> vModel, List<Models.ServiceStationViewModel> sModel)
        {
            List<SelectListItem> vList = new List<SelectListItem>();
            //List<SelectListItem> sList = new List<SelectListItem>();
            //List<SelectListItem> rList = new List<SelectListItem>();

            vList.Add(new SelectListItem
            {
                Text = "ALL",
                Value = "ALL"
            });

            foreach (var vehicle in vModel)
            {
                vList.Add(new SelectListItem
                {
                    Text = vehicle.VINNumber,
                    Value = vehicle.VehicleId.ToString()
                });
            }
            ViewBag.Vehicles = vList;

            //foreach (var station in sModel)
            //{
            //    sList.Add(new SelectListItem
            //    {
            //        Text = station.StreetAddress,
            //        Value = station.ServiceStationId.ToString()
            //    });
            //}

            //ViewBag.ServiceStations = sList;

            //foreach (var repair in rModel)
            //{
            //    rList.Add(new SelectListItem
            //    {
            //        Text = repair.RepairStationId.ToString(),
            //        Value = repair.RepairStationId.ToString()
            //    });
            //}

            //ViewBag.RepairStations = rList;

            return true;
        }



    }
}
