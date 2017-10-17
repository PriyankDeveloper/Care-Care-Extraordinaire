using CarCare.BusinessLogic;
using CarCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Controllers
{
    public class SumerizationController : BaseServiceRecordController
    {
        public SumerizationController(IBusinessInterface businessInterface)
            :base(businessInterface, 8)
        {

        }

        public ActionResult Index()
        {
            return base.BaseIndex();
        }

        public ActionResult Add()
        {
            var serviceRecordViewModel = new ServiceRecordViewModel();
            serviceRecordViewModel.ControllerName = "Sumerization";
            return base.AddNewRecord(serviceRecordViewModel);
        }

        public ActionResult Edit(long id)
        {
            return base.EditRecord(id);
        }
    }
}