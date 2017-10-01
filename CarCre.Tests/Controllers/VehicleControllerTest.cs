using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarCare.Controllers;
using CarCare.BusinessLogic;
using CarCare.Unity;
using System.Linq;
using System;

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
            var newVehicle = useTest.SaveVehicle(new CarCare.CarCareDatabase.Vehicle()
            {
                VehicleMark = "Honda",
                VehicleModel = "Civic",
                VechicleYear = "2017",
                VechicleDealer = "Dealer",
                OwnerId = 1,
                VehicleId = 0,
                VINNumber = "ASDF-1236-ASTD"
            });

            // Act

            var vehicle = useTest.SaveVehicle(new CarCare.CarCareDatabase.Vehicle()
            {
                VehicleMark = "Toyota",
                VehicleModel = "Camry",
                VechicleYear = "2017",
                OwnerId = 1,
                VechicleDealer = "Test",
                VehicleId = 2,
                VINNumber = "ASDF-1236-ASTD"
            });

            // Assert
            Assert.AreEqual(vehicle.VehicleId, newVehicle.VehicleId);
            Assert.AreEqual(vehicle.VehicleMark,"Toyota");
            Assert.AreEqual(vehicle.VehicleModel,"Camry");

        }

        [TestMethod]
        public void DeleteExistingVehicle()
        {
            // Arrange
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();
            var newVehicle = useTest.SaveVehicle(new CarCare.CarCareDatabase.Vehicle()
            {
                VehicleMark = "Mazda",
                VehicleModel = "Mazda3",
                VechicleYear = "2017",
                VechicleDealer = "Dealer",
                OwnerId = 1,
                VehicleId = 0,
                VINNumber = "ASDF-1236-ASTD"
            });

            // Act
            useTest.DeleteVehicle(Convert.ToInt32(newVehicle.VehicleId));

            // Assert
            var checkForVehicle = useTest.GetAllVehicles().FirstOrDefault(i => i.VehicleId == newVehicle.VehicleId);
            Assert.IsTrue(checkForVehicle == null);

        }

    }
}
