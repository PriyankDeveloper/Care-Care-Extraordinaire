using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarCare.Controllers;
using CarCare.BusinessLogic;
using CarCare.Unity;

namespace CarCre.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTest
    {
       IBusinessInterface businessInterface = new CarCareBusinessLogic();

        [TestInitialize]
        public void InitTest() {

          //  Bootstrapper.Initialise();

        }

        [TestMethod]
        public void SuccessfullLogIn()
        {
            // Arrange
            AccountController controller = new AccountController(businessInterface);

            // Act
            RedirectToRouteResult result = controller.LogIn(new CarCare.Models.UserViewModel() { UserName = "Test", UserPassword = "TestUser" }) as RedirectToRouteResult;

            // Assert
            Assert.IsTrue(result.RouteValues["action"].ToString() == "Index");
        }

        [TestMethod]
        public void CorretPasswordIncorrectUserName()
        {
            // Arrange
            AccountController controller = new AccountController(businessInterface);

            // Act
            RedirectToRouteResult result = controller.LogIn(new CarCare.Models.UserViewModel() { UserName = "Test12", UserPassword = "TestUser" }) as RedirectToRouteResult;

            // Assert
            Assert.IsTrue(result.RouteValues["action"].ToString() == "Index");
        }

        [TestMethod]
        public void CorrectUserNameIncorrectPassword()
        {
            // Arrange
            AccountController controller = new AccountController(businessInterface);

            // Act
            RedirectToRouteResult result = controller.LogIn(new CarCare.Models.UserViewModel() { UserName = "Test", UserPassword = "TestUser12" }) as RedirectToRouteResult;

            // Assert
            Assert.IsTrue(result.RouteValues["action"].ToString() == "Index");
        }

        [TestMethod]
        public void IncorrectUserNameIncorrectPassword()
        {
            // Arrange
            AccountController controller = new AccountController(businessInterface);

            // Act
            RedirectToRouteResult result = controller.LogIn(new CarCare.Models.UserViewModel() { UserName = "Test12", UserPassword = "TestUser12" }) as RedirectToRouteResult;

            // Assert
            Assert.IsTrue(result.RouteValues["action"].ToString() == "Index");
        }


        [TestMethod]
        public void SuccessfullLogOut()
        {
            // Arrange
            AccountController controller = new AccountController(businessInterface);

            // Act
            RedirectToRouteResult result = controller.LogOut() as RedirectToRouteResult;

            // Assert
            Assert.IsTrue(result.RouteValues["action"].ToString() == "Index");
        }

        [TestMethod]
        public void RegisterNewUser()
        {
            // Arrange
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();

            // Act
            var newUser = useTest.SaveUser(new CarCare.CarCareDatabase.User() {
                UserName = "NewUser",
                UserPassword = "NewPassword",
                UserEmail = "NewEmail@gmail.com"
            });

            // Assert
            Assert.IsNotNull(newUser.UserId);
        }

        [TestMethod]
        public void UpdateExistingUser()
        {
            // Arrange
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();
            CarCare.CarCareDatabase.User user = new CarCare.CarCareDatabase.User()
            {
                UserName = "Test",
                UserPassword = "TestUser",
                UserEmail = "Test123@gmail.com" 
            };

            // Act
            var newUser = useTest.SaveUser(user);

            // Assert
            Assert.AreEqual(newUser.UserEmail, "Test123@gmail.com");
        }

    }
}
