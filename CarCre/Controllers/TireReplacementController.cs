using CarCare.BusinessLogic;
using CarCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Controllers
{
    public class TireReplacementController : BaseCareCareController
    {

        public TireReplacementController(IBusinessInterface businessInterface)
            :base(businessInterface, 5)
        {

        }

        public ActionResult Index()
        {
            return base.BaseIndex();
        }

        public ActionResult Add()
        {
            var serviceRecordViewModel = new ServiceRecordViewModel();
            serviceRecordViewModel.ControllerName = "TireReplacement";
            return base.AddNewRecord(serviceRecordViewModel);
        }





    }
}