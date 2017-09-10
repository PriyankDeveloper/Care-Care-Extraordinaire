using AutoMapper;
using CarCare.BusinessLogic;
using CarCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using CarCare.CarCareDatabase;
using System.Configuration;

namespace CarCare.Controllers
{
    public class AccountController: Controller
    {
        private IBusinessInterface BusinessInterface;

        public AccountController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }

        public ActionResult Index()
        {
            return View(new Models.User());
        }

        public ActionResult LogIn(Models.User model)
        {
            var isExistingUser = BusinessInterface.GetAllUsers().Any(i=>i.UserName == model.UserName
                                 && i.UserPassword == model.UserPassword);

            if(isExistingUser)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult LogOut()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SaveNewUser(Models.User newUser)
        {
            ViewBag.Message = "Valid User";
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        public ActionResult UpdateExistingUser(Models.User newUser)
        {
            ViewBag.Message = "Valid User";
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}