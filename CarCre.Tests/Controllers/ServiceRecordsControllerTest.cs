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
    public class ServiceRecordsControllerTest
    {
        IBusinessInterface businessInterface = new CarCareBusinessLogic();

        [TestInitialize]
        public void InitTest()
        {
            //  Bootstrapper.Initialise();
        }

        [TestMethod]
        public void AddNewOilChangeServiceRecord()
        {
            // Arrange
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();
            var oilChangeServiceType = useTest.SaveServiceTypecord(new CarCare.CarCareDatabase.ServiceType()
            {
                ServiceTypeId = 0,
                ServiceName = "Oil Change",
                ServiceDescription = "Oil Change"
            });

            var newVehicle = useTest.SaveVehicle(new CarCare.CarCareDatabase.Vehicle()
            {
                VehicleMark = "Honda",
                VehicleModel = "Pilot",
                VechicleYear = "2017",
                VechicleDealer = "Dealer",
                OwnerId = 1,
                VehicleId = 0,
                VINNumber = "ASDF-1236-ASTD"
            });

            var newServiceStation = useTest.SaveServiceStation(new CarCare.CarCareDatabase.ServiceStation()
            {
               ServiceStationId = 0,
               City = "Atlanta",
               State = "GA",
               OwnedBy = "Peter",
               ZipCode = 30339,
               StreetAddress = "South Atlanta Road"
            });


            // Act
            var newOilChangeRecord = useTest.SaveServiceRecord(new CarCare.CarCareDatabase.ServiceRecord()
            {
                ServiceTypeId = oilChangeServiceType.ServiceTypeId,
                LastModifiedDate = DateTime.Now,
                ServiceCost = 500,
                ServiceDate = DateTime.Now,
                ServiceId = 0,
                VehicleId = newVehicle.VehicleId,
                ServiceStationId = newServiceStation.ServiceStationId
            });

            // Assert
            Assert.IsTrue(newOilChangeRecord.ServiceId > 0);
            Assert.IsTrue(newOilChangeRecord.ServiceCost == 500);
        }
    }
}
