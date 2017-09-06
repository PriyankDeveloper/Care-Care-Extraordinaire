using CarCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Controllers
{
    public class AccountController: Controller
    {
        public ActionResult Index()
        {
            return View(new User());
        }

        public ActionResult LogIn(User model)
        {
            var validUser = new User() {
                UserName = "Test",
                Password = "Test"
            };
        
            if(model.UserName == validUser.UserName && model.Password == validUser.Password)
            {
                ViewBag.Message = "Valid User";
                return View("Home/Index");
            }
            else
            {
                ViewBag.Message = "Invalid User";
                return View(new User());
            }
        }

        public ActionResult RegisterUser()
        {
            return View(new User());
        }

        public JsonResult SaveNewUser(User newUser)
        {
            return Json("",JsonRequestBehavior.AllowGet);
        }
    }
}