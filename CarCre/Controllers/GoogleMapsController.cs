﻿using CarCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Controllers
{
    public class GoogleMapsController : Controller
    {
        public ActionResult SearchPlace()
        {
            return View("_GoogleMapsSearchPartialPage");
        }

    }
}