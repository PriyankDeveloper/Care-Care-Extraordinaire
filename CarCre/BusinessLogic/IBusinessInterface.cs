using CarCare.CarCareDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarCare.Models;

namespace CarCare.BusinessLogic
{
   public interface IBusinessInterface : IDisposable
    {
        #region Users
        CarCareDatabase.User SaveUser(CarCare.CarCareDatabase.User user);

        IQueryable<CarCareDatabase.User> GetAllUsers();

        bool isValidUser(Models.UserViewModel model);
        #endregion

        #region Vehicle

        CarCareDatabase.Vehicle SaveVehicle(CarCare.CarCareDatabase.Vehicle vehicle);

        List<Models.VehicleViewModel> GetAllVehicles();

        void DeleteVehicle(int vehicleId);

        #endregion

        #region ServiceRecord
        CarCareDatabase.ServiceRecord SaveServiceRecord(CarCare.CarCareDatabase.ServiceRecord serviceRecord);

        List<CarCareDatabase.ServiceRecord> GetAllServiceRecords();

        void DeleteServiceRecord(long serviceRecordId);

        #endregion

        #region ServiceStation

        CarCareDatabase.ServiceStation SaveServiceStation(CarCare.CarCareDatabase.ServiceStation serviceStation);

        List<CarCareDatabase.ServiceStation> GetAllServiceStations();

        void DeleteServiceStation(int serviceStationId);

        #endregion

        #region ServiceType
        List<CarCareDatabase.ServiceType> GetAllServiceTypes();

        CarCareDatabase.ServiceType SaveServiceTypecord(CarCareDatabase.ServiceType serviceType);
        #endregion

        #region RepairRecord
        CarCareDatabase.RepairRecord SaveRepairRecord(CarCare.CarCareDatabase.RepairRecord repairRecord);

        List<Models.RepairRecordViewModel> GetAllRepairRecords();

        void DeleteRepairRecord(long repairRecordId);

        #endregion

        #region Insurance
        CarCareDatabase.Insurance SaveInsuranceRecord(CarCare.CarCareDatabase.Insurance insuranceRecord);

        List<Models.InsuranceViewModel> GetAllInsuranceRecords();

        void DeleteInsuranceRecord(long insuranceRecordId);

        #endregion

        #region WarrantyRecord
        CarCareDatabase.Warranty SaveWarrantyRecord(CarCare.CarCareDatabase.Warranty warrantyRecord);

        List<Models.WarrantyViewModel> GetAllWarrantyRecords();

        void DeleteWarrantyRecord(long warrantyRecordId);

        #endregion



    }
}
