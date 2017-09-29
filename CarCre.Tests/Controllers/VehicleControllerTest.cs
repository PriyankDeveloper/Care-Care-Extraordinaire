using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarCare.Controllers;
using CarCare.BusinessLogic;
using CarCare.Unity;


namespace CarCre.Tests.Controllers
{
   [TestClass]
   public class VehicleControllerTest
    {
        IBusinessInterface businessInterface = new CarCareBusinessLogic();

        [TestInitialize]
        public void InitTest()
        {
            //  Bootstrapper.Initialise();
        }


        [TestMethod]
        public void RegisterNewVehicle()
        {
            // Arrange
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();

            // Act
            var newVehicle = useTest.SaveVehicle(new CarCare.CarCareDatabase.Vehicle()
            {
               VehicleMark = "Honda",
               VehicleModel = "Accord",
               VechicleYear = "2017",
               VechicleDealer = "Dealer",
               OwnerId = 1,
               VehicleId = 0,
               VINNumber = "ASDF-1236-ASTD"
            });

            // Assert
            Assert.IsTrue(newVehicle.VehicleId > 0);
        }

        [TestMethod]
        public void UpdateExistingVehicle()
        {
            // Arrange
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();

            // Act
            var vehicle = useTest.SaveVehicle(new CarCare.CarCareDatabase.Vehicle()
            {
                VehicleMark = "Toyota",
                VehicleModel = "camry",
                VechicleYear = "2017",
                OwnerId = 1,
                VehicleId = 1,
                VINNumber = "ASDF-1236-ASTD"
            });

            // Assert
            Assert.Equals(vehicle.VehicleId, 1);
            Assert.AreEqual(vehicle.VehicleMark,"Toyota");
            Assert.AreEqual(vehicle.VehicleModel,"camry");

        }

    }
}
