using CarCare.BusinessLogic;
using CarCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace CarCare.Controllers
{
    public class WinterizationController : BaseServiceRecordController
    {
        public WinterizationController(IBusinessInterface businessInterface)
            :base(businessInterface, 7)
        {

        }

        public ActionResult Index()
        {
            return base.BaseIndex();
        }

        public ActionResult Add()
        {
            var serviceRecordViewModel = new ServiceRecordViewModel();
            serviceRecordViewModel.ControllerName = "Winterization";
            return base.AddNewRecord(serviceRecordViewModel);
        }

        public ActionResult Edit(long id)
        {
            return base.EditRecord(id);
        }
    }
}