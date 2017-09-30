using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarCare.Controllers;
using CarCare.BusinessLogic;
using CarCare.Unity;
using System.Linq;

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
                VechicleDealer = "Test",
                VehicleId = 2,
                VINNumber = "ASDF-1236-ASTD"
            });

            // Assert
            Assert.AreEqual(vehicle.VehicleId, 2);
            Assert.AreEqual(vehicle.VehicleMark,"Toyota");
            Assert.AreEqual(vehicle.VehicleModel,"camry");

        }

        [TestMethod]
        public void DeleteExistingVehicle()
        {
            // Arrange
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();

            // Act
             useTest.DeleteVehicle(2);

            var checkForVehicle = useTest.GetAllVehicles().FirstOrDefault(i=>i.VehicleId == 2);

            // Assert
            Assert.IsTrue(checkForVehicle == null);

        }

    }
}
