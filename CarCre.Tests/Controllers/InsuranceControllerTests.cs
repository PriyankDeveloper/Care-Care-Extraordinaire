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
    public class InsuranceControllerTests
    {

        [TestMethod()]
        public void AddNewInsuranceRecordTest()
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
            
            // create new insurance record
            var newInsuranceRecord = useTest.SaveInsuranceRecord(new CarCare.CarCareDatabase.Insurance()
            {
                PolicyNumber = "N100-20-3000",
                InsuranceStartDate = DateTime.Now,
                InsuranceCost = 750,
                VehicleId = newVehicle.VehicleId,
                InsuranceProvider = "Acompany",
                InsuranceCoverage = "liability",
            });

            // Assert
            Assert.IsTrue(newInsuranceRecord.PolicyNumber == "N100-20-3000");
            Assert.AreEqual(newInsuranceRecord.InsuranceCost, 750);
            Assert.AreEqual(newInsuranceRecord.InsuranceProvider, "Acompany");
            Assert.AreEqual(newInsuranceRecord.InsuranceCoverage, "liability");

            // delete created records
            useTest.DeleteInsuranceRecord(newInsuranceRecord.InsuranceId);
            useTest.DeleteVehicle(Convert.ToInt32(newVehicle.VehicleId));
        }

        [TestMethod()]
        public void EditInsuranceRecordTest()
        {
            // Initialize
            CarCareBusinessLogic useTest = new CarCareBusinessLogic();
            CarCare.CarCareDatabase.Insurance insuranceRecord = useTest.GetAllInsuranceRecords(1)[0];
            CarCare.CarCareDatabase.Insurance updatedInsuranceRecord = insuranceRecord;


            // update insurance record
            updatedInsuranceRecord.PolicyNumber = "new_N100-20-3000";
            updatedInsuranceRecord.InsuranceCost = 2000;
            updatedInsuranceRecord.InsuranceProvider = "Bcompany";
            updatedInsuranceRecord.InsuranceCoverage = "Edit unit test";

            var curInsuranceRecord = useTest.SaveInsuranceRecord(updatedInsuranceRecord);

            // Assert
            Assert.IsTrue(curInsuranceRecord.PolicyNumber == "new_N100-20-3000");
            Assert.AreEqual(curInsuranceRecord.InsuranceCost, 2000);
            Assert.AreEqual(curInsuranceRecord.InsuranceProvider, "Bcompany");
            Assert.AreEqual(curInsuranceRecord.InsuranceCoverage, "Edit unit test");

            // reset values
            useTest.SaveInsuranceRecord(insuranceRecord);
        }
    }
}
