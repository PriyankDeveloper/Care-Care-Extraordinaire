using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarCare;
using CarCare.Controllers;

namespace CarCre.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        [TestMethod]
        public void SuccessfullLogIn()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.LogIn(new CarCare.Models.User() { UserName = "Test", Password = "Test" }) as ViewResult;

            // Assert
            Assert.AreEqual("Valid User", result.ViewBag.Message);
        }

        [TestMethod]
        public void UnSuccessfullLogIn()
        {
            // Arrange
            AccountController controller = new AccountController();

            // Act
            ViewResult result = controller.LogIn(new CarCare.Models.User() { UserName = "Test12", Password = "Test" }) as ViewResult;

            // Assert
            Assert.AreEqual("Invalid User", result.ViewBag.Message);
        }

    }
}
