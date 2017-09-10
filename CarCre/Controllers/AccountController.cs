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
    public class AccountController: Controller
    {
        private IBusinessInterface BusinessInterface;

        public AccountController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }

        public ActionResult Index()
        {
            return View(new User());
        }

        public ActionResult LogIn(User model)
        {
            bool isValidUser = BusinessInterface.isValidUser(model);

            var isExistingUser = BusinessInterface.GetAllUsers().Any(i=>i.UserName == model.UserName
                                 && i.UserPassword == model.UserPassword);

            if(isExistingUser)
            {
                ViewBag.Message = "Valid User";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid User";
                return RedirectToAction("Index");
            }
        }

        public ActionResult LogOut()
        {
            ViewBag.Message = "LogOut";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SaveNewUser(User newUser)
        {
          
          
            ViewBag.Message = "Valid User";
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}