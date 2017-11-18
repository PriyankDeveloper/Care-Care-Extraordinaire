using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarCare.Controllers;
using CarCare.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCre.Tests.Controllers
{
    [TestClass]
    public class WarrantyControllerTests
    {
        [TestMethod()]
        public void AddNewWarrantyRecordTest()
        {
            // Initialize
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();

            var newVehicle = useTest.SaveVehicle(new CarCare.CarCareDatabase.Vehicle()
            {
                VehicleMark = "Toyata",
                VehicleModel = "Camery",
                VechicleYear = "1997",
                VechicleDealer = "ABC",
                OwnerId = 2,
                VINNumber = "111-22-3333"
            });

            // create new waranty record
            var newWarrantyRecord = useTest.SaveWarrantyRecord(new CarCare.CarCareDatabase.Warranty
            {
                PolicyNumber = "N100-20-3000",
                WarrantyStartDate = DateTime.Now,
                WarrantyExpirationDate = DateTime.Now,
                WarrantyCost = 750,
                VehicleId = newVehicle.VehicleId,
                WarrantyProvider = "Acompany",
                WarrantyCoverage = "default coverage"

            });

            // Assert
            Assert.IsTrue(newWarrantyRecord.PolicyNumber == "N100-20-3000");
            Assert.AreEqual(newWarrantyRecord.WarrantyCost, 750);
            Assert.AreEqual(newWarrantyRecord.VehicleId, newVehicle.VehicleId);
            Assert.AreEqual(newWarrantyRecord.WarrantyProvider, "Acompany");
            Assert.AreEqual(newWarrantyRecord.WarrantyCoverage, "default coverage");

            // delete created records
            useTest.DeleteWarrantyRecord(newWarrantyRecord.WarrantyId);
            useTest.DeleteVehicle((int)newVehicle.VehicleId);
        }

        [TestMethod()]
        public void EditWarrantyRecordTest()
        {
            // Initialize
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();
            CarCare.CarCareDatabase.Warranty warrantyRecord = useTest.GetAllWarrantyRecords(1)[0];
            CarCare.CarCareDatabase.Warranty updatedWarrantyRecord = warrantyRecord;


            // update insurance record
            updatedWarrantyRecord.PolicyNumber = "new_N100-20-3000";
            updatedWarrantyRecord.WarrantyCost = 2000;
            updatedWarrantyRecord.WarrantyProvider = "Bcompany";
            updatedWarrantyRecord.WarrantyCoverage = "Edit unit test";

            var curWarrantyRecord = useTest.SaveWarrantyRecord(updatedWarrantyRecord);

            // Assert
            Assert.IsTrue(curWarrantyRecord.PolicyNumber == "new_N100-20-3000");
            Assert.AreEqual(curWarrantyRecord.WarrantyCost, 2000);
            Assert.AreEqual(curWarrantyRecord.WarrantyProvider, "Bcompany");
            Assert.AreEqual(curWarrantyRecord.WarrantyCoverage, "Edit unit test");

            // reset values
            useTest.SaveWarrantyRecord(warrantyRecord);
        }
    }
}
