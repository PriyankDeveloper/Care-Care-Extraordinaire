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
    public class TireReplacementController : Controller
    {
        public ActionResult TireReplacement()
        {
            return View("TireReplacement");
        }

        public ActionResult Save(Models.ServiceRecordViewModel model) {
            
            return null;
        }


	}
}