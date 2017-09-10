using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarCare.Controllers;
using CarCare.BusinessLogic;

namespace CarCre.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
        private IBusinessInterface businessInterface;
        [TestMethod]
        public void SuccessfullLogIn()
        {
            // Arrange
            AccountController controller = new AccountController(businessInterface);

            // Act
            ViewResult result = controller.LogIn(new CarCare.Models.User() { UserName = "Test", UserPassword = "Test" }) as ViewResult;

            // Assert
            Assert.AreEqual("Valid User", result.ViewBag.Message);
        }

        [TestMethod]
        public void UnSuccessfullLogIn()
        {
            // Arrange
            AccountController controller = new AccountController(businessInterface);

            // Act
            ViewResult result = controller.LogIn(new CarCare.Models.User() { UserName = "Test12", UserPassword = "Test" }) as ViewResult;

            // Assert
            Assert.AreEqual("Invalid User", result.ViewBag.Message);
        }

        [TestMethod]
        public void SuccessfullLogOut()
        {
            // Arrange
            AccountController controller = new AccountController(businessInterface);

            // Act
            ViewResult result = controller.LogOut() as ViewResult;

            // Assert
            Assert.AreEqual("LogOut", result.ViewBag.Message);
        }

        [TestMethod]
        public void RegisterNewUser()
        {
            // Arrange
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();

            // Act
            var newUser = useTest.SaveUser(new CarCare.CarCareDatabase.User());

            // Assert
            Assert.IsNotNull(newUser.UserId);
        }

    }
}
