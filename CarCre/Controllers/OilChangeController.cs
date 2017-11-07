using AutoMapper;
using CarCare.BusinessLogic;
using CarCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CarCare.Controllers
{
    public class OilChangeController : BaseServiceRecordController
    {

        public OilChangeController(IBusinessInterface businessInterface)
            : base(businessInterface, 1)
        {

        }

        public ActionResult Index()
        {
            return base.BaseIndex();
        }

        public ActionResult Add()
        {
            var serviceRecordViewModel = new ServiceRecordViewModel();
            serviceRecordViewModel.ControllerName = "OilChange";
            return base.AddNewRecord(serviceRecordViewModel);
        }

        public ActionResult Edit(long id)
        {
            return base.EditRecord(id);
        }
    }
}
