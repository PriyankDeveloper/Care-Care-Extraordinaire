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
using System.Web.Security;

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
            return View(new Models.UserViewModel());
        }

        public ActionResult LogIn(Models.UserViewModel model)
        {
            IQueryable<User> allUsers = BusinessInterface.GetAllUsers();

            var existingUsers = allUsers.Where(i=>i.UserName == model.UserName
                                 && i.UserPassword == model.UserPassword);
            Boolean isExistingUser = existingUsers.Count() > 0;

            if (isExistingUser)
            {

                ViewBag.CurrentUserName = existingUsers.First().UserName;
                long userId = existingUsers.ToList().ElementAt(0).UserId;
                FormsAuthentication.SetAuthCookie(userId.ToString(), true);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.CurrentUserName = "";
                FormsAuthentication.SignOut();
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SaveNewUser(Models.UserViewModel newUser)
        {
            CarCareDatabase.User user = new CarCareDatabase.User() {
                UserEmail = newUser.Email,
                UserName = newUser.UserName,
                UserPassword = newUser.UserPassword
            };

          var newAccountUser =  BusinessInterface.SaveUser(user);

            if(newAccountUser.UserId > 0)
            {
                FormsAuthentication.SetAuthCookie(newAccountUser.UserId.ToString(), true);
                ViewBag.Message = "Valid User";
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            ViewBag.Message = "Valid User";
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [HttpPost]
        public ActionResult UpdateExistingUser(Models.UserViewModel newUser)
        {
            CarCareDatabase.User user = new CarCareDatabase.User()
            {
                UserEmail = newUser.Email,
                UserId = newUser.UserId,
                UserName = newUser.UserName,
                UserPassword = newUser.UserPassword
            };

            var newAccountUser = BusinessInterface.SaveUser(user);

            ViewBag.Message = "Valid User";
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}