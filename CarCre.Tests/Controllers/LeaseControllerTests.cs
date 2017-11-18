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
    public class LeaseControllerTests
    {
        [TestMethod()]
        public void AddNewLeaseRecordTest()
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
            
            // create new lease record
            var newLeaseRecord = useTest.SaveLeaseRecord(new CarCare.CarCareDatabase.LeaseRecord()
            {
                LeaseStartDate = DateTime.Now,
                LeaseTerm = 36,
                VehicleId = newVehicle.VehicleId,
                Company = "Acompany",
                MonthlyPayment = 250,
                LeaseNotes = "unit test"
            });

            // Assert
            Assert.IsTrue(newLeaseRecord.MonthlyPayment == 250);
            Assert.AreEqual(newLeaseRecord.VehicleId, newVehicle.VehicleId);
            Assert.AreEqual(newLeaseRecord.LeaseTerm, 36);
            Assert.AreEqual(newLeaseRecord.Company, "Acompany");
            Assert.AreEqual(newLeaseRecord.LeaseNotes, "unit test");

            // delete created records
            useTest.DeleteLeaseRecord(newLeaseRecord.LeaseId);
            useTest.DeleteVehicle((int)newVehicle.VehicleId);
        }

        [TestMethod()]
        public void EditLeaseRecordTest()
        {
            // Initialize
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();
            CarCare.CarCareDatabase.LeaseRecord leaseRecord = useTest.GetAllLeaseRecords(1)[0];
            CarCare.CarCareDatabase.LeaseRecord updatedLeaseRecord = leaseRecord;


            // update insurance record
            updatedLeaseRecord.LeaseTerm = 99;
            updatedLeaseRecord.MonthlyPayment = 2000;
            updatedLeaseRecord.Company = "Bcompany";
            updatedLeaseRecord.LeaseNotes = "Edit unit test";

            var curLeaseRecord = useTest.SaveLeaseRecord(updatedLeaseRecord);

            // Assert
            Assert.IsTrue(curLeaseRecord.LeaseTerm == 99);
            Assert.AreEqual(curLeaseRecord.MonthlyPayment, 2000);
            Assert.AreEqual(curLeaseRecord.Company, "Bcompany");
            Assert.AreEqual(curLeaseRecord.LeaseNotes, "Edit unit test");

            // reset values
            useTest.SaveLeaseRecord(leaseRecord);
        }

    }
}
