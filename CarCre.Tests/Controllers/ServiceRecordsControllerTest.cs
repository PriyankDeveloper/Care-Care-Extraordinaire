﻿using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarCare.Controllers;
using CarCare.BusinessLogic;
using CarCare.Unity;
using System.Linq;

namespace CarCre.Tests.Controllers
{
    [TestClass]
    public class ServiceRecordsControllerTest
    {
        IBusinessInterface businessInterface = new CarCareBusinessLogic();

        [TestInitialize]
        public void InitTest()
        {
            //  Bootstrapper.Initialise();
        }

        public void AddNewServiceType()
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
    }
}
