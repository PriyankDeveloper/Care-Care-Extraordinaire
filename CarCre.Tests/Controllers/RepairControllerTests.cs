using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarCare.Controllers;
using CarCare.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCare.Controllers.Tests
{
    [TestClass()]
    public class RepairControllerTests
    {
        [TestMethod()]
        public void AddNewRepairRecordTest()
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
                //VehicleId = 10,
                VINNumber = "111-22-3333"
            });

            var newServiceStation = useTest.SaveServiceStation(new CarCare.CarCareDatabase.ServiceStation()
            {
                //ServiceStationId = 1,
                City = "Mira Mesa",
                State = "CA",
                OwnedBy = "AOwner",
                ZipCode = 92126,
                StreetAddress = "123 XYZ Road"
            });

            // create new repair record
            var newRepairRecord = useTest.SaveRepairRecord(new CarCare.CarCareDatabase.RepairRecord()
            {
                //RepairId = 100,
                RepairDate = DateTime.Now,
                RepairCompleteDate = DateTime.Now,
                RepairStationId = newServiceStation.ServiceStationId,
                RepairCost = 750,
                VehicleId = newVehicle.VehicleId,
                RepairShortDesc = "brake issue",
                RepairStatus = "completed",
                RepairDetails = "unit test"
            });

            // Assert
            Assert.IsTrue(newRepairRecord.RepairCost == 750);
            Assert.AreEqual(newRepairRecord.VehicleId, newVehicle.VehicleId);
            Assert.AreEqual(newRepairRecord.RepairShortDesc, "brake issue");
            Assert.AreEqual(newRepairRecord.RepairStatus, "completed");
            Assert.AreEqual(newRepairRecord.RepairDetails, "unit test");

            // delete created records
            useTest.DeleteRepairRecord(newRepairRecord.RepairId);
            useTest.DeleteServiceStation((int)newServiceStation.ServiceStationId);
            useTest.DeleteVehicle((int)newVehicle.VehicleId);
        }

        [TestMethod()]
        public void EditRepairRecordTest()
        {
            // Initialize
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();
            CarCareDatabase.RepairRecord repairRecord = useTest.GetAllRepairRecords(1)[0];
            CarCareDatabase.RepairRecord updatedRepairRecord = repairRecord;

            // update repair record
            updatedRepairRecord.RepairCost = 2000;
            updatedRepairRecord.RepairShortDesc = "new brake issue";
            updatedRepairRecord.RepairStatus = "pending";
            updatedRepairRecord.RepairDetails = "edit unit test";

            var curRepairRecord = useTest.SaveRepairRecord(updatedRepairRecord);

            // Assert
            Assert.IsTrue(curRepairRecord.RepairCost == 2000);
            Assert.AreEqual(curRepairRecord.RepairShortDesc, "new brake issue");
            Assert.AreEqual(curRepairRecord.RepairStatus, "pending");
            Assert.AreEqual(curRepairRecord.RepairDetails, "edit unit test");
            
            // reset values
            useTest.SaveRepairRecord(repairRecord);
        }
    }
}