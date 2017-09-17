using CarCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Controllers
{
    public class VehicleController : Controller
    {
        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveVehicle(Vehicle vehicle)
        {

            return Json(true,JsonRequestBehavior.AllowGet);
        }

    }
}