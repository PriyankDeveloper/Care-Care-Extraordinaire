using CarCare.CarCareDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarCare.Models;
using System.Data.Entity;
using CarCare.Unity;
using AutoMapper;
using System.Web.Security;

namespace CarCare.BusinessLogic
{
    public class CarCareBusinessLogic : IBusinessInterface
    {
        private CarCareEntity carCareEntities = new CarCareEntity();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #region Users
        public IQueryable<CarCareDatabase.User> GetAllUsers()
        {
            Bootstrapper.Initialise();
            return carCareEntities.Users.AsQueryable();
        }

        public void DeleteUser(long userId)
        {
            Bootstrapper.Initialise();

            var existingUser = carCareEntities.Users.FirstOrDefault(i => i.UserId == userId);

            if (existingUser != null)
            {
                carCareEntities.Entry(existingUser).State = EntityState.Deleted;
                carCareEntities.SaveChanges();
            }
        }

        public bool isValidUser(Models.UserViewModel model)
        {
            bool isExistingUser = carCareEntities.Users.Any(i=>i.UserName == model.UserName && i.UserPassword == model.UserPassword);

          return isExistingUser;
        }


        public CarCareDatabase.User SaveUser(CarCareDatabase.User user)
        {
            Bootstrapper.Initialise();
            var existingUser = carCareEntities.Users.FirstOrDefault(i => i.UserName == user.UserName && i.UserPassword == user.UserPassword);

            if(existingUser != null)
            {
                user.UserId = existingUser.UserId;
                carCareEntities.Entry(existingUser).CurrentValues.SetValues(user);
                carCareEntities.SaveChanges();
            }
            else
            {
                carCareEntities.Users.Attach(user);
                carCareEntities.Entry(user).State = EntityState.Added;
                carCareEntities.SaveChanges();
            }

            return user;
        }

        #endregion

        #region Vehicle

        public void DeleteVehicle(int vehicleId)
        {
            Bootstrapper.Initialise();

            var existingVechicle = carCareEntities.Vehicles.FirstOrDefault(i => i.VehicleId == vehicleId);

            if(existingVechicle != null)
            {
                carCareEntities.Entry(existingVechicle).State = EntityState.Deleted;
                carCareEntities.SaveChanges();
            }
        }

        public List<Models.VehicleViewModel> GetAllVehicles(long userId)
        {
            var vehicles = carCareEntities.Vehicles.Where(v => v.OwnerId == userId).ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CarCareDatabase.Vehicle, Models.VehicleViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new List<CarCareDatabase.Vehicle>();
            var dest = mapper.Map<List<CarCareDatabase.Vehicle>, List<Models.VehicleViewModel>>(vehicles);

            return dest;
        }

        public CarCareDatabase.Vehicle SaveVehicle(CarCareDatabase.Vehicle vehicle)
        {

            Bootstrapper.Initialise();
            
            var existingVechicle = carCareEntities.Vehicles.FirstOrDefault(i => i.VehicleId == vehicle.VehicleId);

            if (existingVechicle != null)
            {
                carCareEntities.Entry(existingVechicle).CurrentValues.SetValues(vehicle);
                carCareEntities.SaveChanges();
            }
            else
            {
                carCareEntities.Vehicles.Attach(vehicle);
                carCareEntities.Entry(vehicle).State = EntityState.Added;
                carCareEntities.SaveChanges();
            }
            
            return vehicle;
        }

        #endregion

        #region ServiceType

        public List<CarCareDatabase.ServiceType> GetAllServiceTypes()
        {
            return carCareEntities.ServiceTypes.ToList();
        }

        public CarCareDatabase.ServiceType SaveServiceTypecord(CarCareDatabase.ServiceType serviceType)
        {

            Bootstrapper.Initialise();

            var existingServiceType = carCareEntities.ServiceTypes.FirstOrDefault(i => i.ServiceTypeId == serviceType.ServiceTypeId);

            if (existingServiceType != null)
            {
                carCareEntities.Entry(existingServiceType).CurrentValues.SetValues(serviceType);
                carCareEntities.SaveChanges();
            }
            else
            {
                carCareEntities.ServiceTypes.Attach(serviceType);
                carCareEntities.Entry(serviceType).State = EntityState.Added;
                carCareEntities.SaveChanges();
            }

            return serviceType;
        }

        #endregion

        #region ServiceRecord

        public List<CarCareDatabase.ServiceRecord> GetAllServiceRecords(long userId)
        {
            List<ServiceRecord> listRecords = carCareEntities.ServiceRecords.ToList();
            List<VehicleViewModel> carList = GetAllVehicles(userId);
            
            carList = carList.Where(i => i.OwnerId == userId).ToList();
            List<long> carIdList = new List<long>();
            carList.ForEach(car => carIdList.Add(car.VehicleId));
            listRecords = listRecords.Where(i => carIdList.Contains(i.VehicleId)).ToList();
            return listRecords;
        }

        public long getUserIdFromCookie(HttpCookieCollection cookies)
        {
            string cookieName = FormsAuthentication.FormsCookieName; //Find cookie name
            HttpCookie authCookie = cookies[cookieName]; //Get the cookie by it's name
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value); //Decrypt it
            string userIdStr = ticket.Name;
            long userId = (long)Convert.ToDouble(userIdStr);
            return userId;
        }

        public CarCareDatabase.ServiceRecord SaveServiceRecord(CarCareDatabase.ServiceRecord serviceRecord)
        {

            Bootstrapper.Initialise();

            var existingServiceRecord = carCareEntities.ServiceRecords.FirstOrDefault(i => i.ServiceId == serviceRecord.ServiceId);

            if (existingServiceRecord != null)
            {
                carCareEntities.Entry(existingServiceRecord).CurrentValues.SetValues(serviceRecord);
                carCareEntities.SaveChanges();
            }
            else
            {
                carCareEntities.ServiceRecords.Attach(serviceRecord);
                carCareEntities.Entry(serviceRecord).State = EntityState.Added;
                carCareEntities.SaveChanges();
            }

            return serviceRecord;
        }

        public void DeleteServiceRecord(long serviceId)
        {
            Bootstrapper.Initialise();

            var existingServiceRecord = carCareEntities.ServiceRecords.FirstOrDefault(i => i.ServiceId == serviceId);

            if (existingServiceRecord != null)
            {
                carCareEntities.Entry(existingServiceRecord).State = EntityState.Deleted;
                carCareEntities.SaveChanges();
            }
        }

        #endregion

        #region ServiceStations

        public List<Models.ServiceStationViewModel> GetAllServiceStations()
        {
            //return carCareEntities.ServiceStations.ToList();
            var ServiceStation = carCareEntities.ServiceStations.ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CarCareDatabase.ServiceStation, Models.ServiceStationViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new List<CarCareDatabase.ServiceStation>();
            var dest = mapper.Map<List<CarCareDatabase.ServiceStation>, List<Models.ServiceStationViewModel>>(ServiceStation);

            return dest;
        }


        public CarCareDatabase.ServiceStation SaveServiceStation(CarCareDatabase.ServiceStation serviceStation)
        {

            Bootstrapper.Initialise();

            var existingServiceStation = carCareEntities.ServiceStations.FirstOrDefault(i => i.ServiceStationId == serviceStation.ServiceStationId);

            if (existingServiceStation != null)
            {
                carCareEntities.Entry(existingServiceStation).CurrentValues.SetValues(serviceStation);
                carCareEntities.SaveChanges();
            }
            else
            {
                carCareEntities.ServiceStations.Attach(serviceStation);
                carCareEntities.Entry(serviceStation).State = EntityState.Added;
                carCareEntities.SaveChanges();
            }

            return serviceStation;
        }

        public void DeleteServiceStation(int serviceStationId)
        {
            Bootstrapper.Initialise();

            var existingServiceStation = carCareEntities.ServiceStations.FirstOrDefault(i => i.ServiceStationId == serviceStationId);

            if (existingServiceStation != null)
            {
                carCareEntities.Entry(existingServiceStation).State = EntityState.Deleted;
                carCareEntities.SaveChanges();
            }
        }



        #endregion

        #region RepairRecord

        public List<CarCareDatabase.RepairRecord> GetAllRepairRecords(long userId)
        {
            List<RepairRecord> listRecords = carCareEntities.RepairRecords.ToList();
            List<VehicleViewModel> carList = GetAllVehicles(userId);

            carList = carList.Where(i => i.OwnerId == userId).ToList();
            List<long> carIdList = new List<long>();
            carList.ForEach(car => carIdList.Add(car.VehicleId));
            listRecords = listRecords.Where(i => carIdList.Contains(i.VehicleId)).ToList();
            return listRecords;
            /*
            var repairs = carCareEntities.RepairRecords.ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CarCareDatabase.RepairRecord, Models.RepairRecordViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new List<CarCareDatabase.RepairRecord>();
            var dest = mapper.Map<List<CarCareDatabase.RepairRecord>, List<Models.RepairRecordViewModel>>(repairs);

            return dest;*/
        }

        public CarCareDatabase.RepairRecord SaveRepairRecord(CarCareDatabase.RepairRecord repairRecord)
        {

            Bootstrapper.Initialise();

            var existingRepairRecord = carCareEntities.RepairRecords.FirstOrDefault(i => i.RepairId == repairRecord.RepairId);

            if (existingRepairRecord != null)
            {
                carCareEntities.Entry(existingRepairRecord).CurrentValues.SetValues(repairRecord);
                carCareEntities.SaveChanges();
            }
            else
            {
                carCareEntities.RepairRecords.Attach(repairRecord);
                carCareEntities.Entry(repairRecord).State = EntityState.Added;
                carCareEntities.SaveChanges();
            }

            return repairRecord;
        }

        public void DeleteRepairRecord(long repairId)
        {
            Bootstrapper.Initialise();

            var existingRepairRecord = carCareEntities.RepairRecords.FirstOrDefault(i => i.RepairId == repairId);

            if (existingRepairRecord != null)
            {
                carCareEntities.Entry(existingRepairRecord).State = EntityState.Deleted;
                carCareEntities.SaveChanges();
            }

        }

        #endregion

        #region Insurance
        
        public List<CarCareDatabase.Insurance> GetAllInsuranceRecords(long userId)
        {
            List<Insurance> listRecords = carCareEntities.Insurances.ToList();
            List<VehicleViewModel> carList = GetAllVehicles(userId);

            carList = carList.Where(i => i.OwnerId == userId).ToList();
            List<long> carIdList = new List<long>();
            carList.ForEach(car => carIdList.Add(car.VehicleId));
            listRecords = listRecords.Where(i => carIdList.Contains(i.VehicleId)).ToList();
            return listRecords;
            /*
            var insurances = carCareEntities.Insurances.ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CarCareDatabase.Insurance, Models.InsuranceViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new List<CarCareDatabase.Insurance>();
            var dest = mapper.Map<List<CarCareDatabase.Insurance>, List<Models.InsuranceViewModel>>(insurances);

            return dest;*/
        }

        public CarCareDatabase.Insurance SaveInsuranceRecord(CarCareDatabase.Insurance insuranceRecord)
        {
            Bootstrapper.Initialise();

            var existingInsuranceRecord = carCareEntities.Insurances.FirstOrDefault(i => i.InsuranceId == insuranceRecord.InsuranceId);

            if (existingInsuranceRecord != null)
            {
                carCareEntities.Entry(existingInsuranceRecord).CurrentValues.SetValues(insuranceRecord);
                carCareEntities.SaveChanges();
            }
            else
            {
                carCareEntities.Insurances.Attach(insuranceRecord);
                carCareEntities.Entry(insuranceRecord).State = EntityState.Added;
                carCareEntities.SaveChanges();
            }

            return insuranceRecord;
        }

        public void DeleteInsuranceRecord(long insuranceId)
        {
            Bootstrapper.Initialise();

            var existingInsuranceRecord = carCareEntities.Insurances.FirstOrDefault(i => i.InsuranceId == insuranceId);

            if (existingInsuranceRecord != null)
            {
                carCareEntities.Entry(existingInsuranceRecord).State = EntityState.Deleted;
                carCareEntities.SaveChanges();
            }
            
        }

        #endregion

        #region Warranty

        public List<CarCareDatabase.Warranty> GetAllWarrantyRecords(long userId)
        {
            List<Warranty> listRecords = carCareEntities.Warranties.ToList();
            List<VehicleViewModel> carList = GetAllVehicles(userId);

            carList = carList.Where(i => i.OwnerId == userId).ToList();
            List<long> carIdList = new List<long>();
            carList.ForEach(car => carIdList.Add(car.VehicleId));
            listRecords = listRecords.Where(i => carIdList.Contains(i.VehicleId)).ToList();
            return listRecords;
            /*var warranties = carCareEntities.Warranties.ToList();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CarCareDatabase.Warranty, Models.WarrantyViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            var source = new List<CarCareDatabase.Warranty>();
            var dest = mapper.Map<List<CarCareDatabase.Warranty>, List<Models.WarrantyViewModel>>(warranties);

            return dest;*/
        }

        public CarCareDatabase.Warranty SaveWarrantyRecord(CarCareDatabase.Warranty warrantyRecord)
        //public void SaveWarrantyRecord(CarCareDatabase.Warranty warrantyRecord)
        {

            Bootstrapper.Initialise();

            var existingWarrantyRecord = carCareEntities.Warranties.FirstOrDefault(i => i.WarrantyId == warrantyRecord.WarrantyId);

            if (existingWarrantyRecord != null)
            {
                carCareEntities.Entry(existingWarrantyRecord).CurrentValues.SetValues(warrantyRecord);
                carCareEntities.SaveChanges();
            }
            else
            {
                carCareEntities.Warranties.Attach(warrantyRecord);
                carCareEntities.Entry(warrantyRecord).State = EntityState.Added;
                carCareEntities.SaveChanges();
            }

            //warrantyRecord = null;
            return warrantyRecord;
        }

        public void DeleteWarrantyRecord(long warrantyId)
        {
            Bootstrapper.Initialise();

            var existingWarrantyRecord = carCareEntities.Warranties.FirstOrDefault(i => i.WarrantyId == warrantyId);

            if (existingWarrantyRecord != null)
            {
                carCareEntities.Entry(existingWarrantyRecord).State = EntityState.Deleted;
                carCareEntities.SaveChanges();
            }

        }

        #endregion

        #region LeaseRecord
        
        public List<CarCareDatabase.LeaseRecord> GetAllLeaseRecords(long userId)
        {
            List<LeaseRecord> listRecords = carCareEntities.LeaseRecords.ToList();
            List<VehicleViewModel> carList = GetAllVehicles(userId);

            carList = carList.Where(i => i.OwnerId == userId).ToList();
            List<long> carIdList = new List<long>();
            carList.ForEach(car => carIdList.Add(car.VehicleId));
            listRecords = listRecords.Where(i => carIdList.Contains(i.VehicleId)).ToList();
            return listRecords;
            
        }

        public CarCareDatabase.LeaseRecord SaveLeaseRecord(CarCareDatabase.LeaseRecord leaseRecord)
        {

            Bootstrapper.Initialise();

            var existingLeaseRecord = carCareEntities.LeaseRecords.FirstOrDefault(i => i.LeaseId == leaseRecord.LeaseId);

            if (existingLeaseRecord != null)
            {
                carCareEntities.Entry(existingLeaseRecord).CurrentValues.SetValues(leaseRecord);
                carCareEntities.SaveChanges();
            }
            else
            {
                carCareEntities.LeaseRecords.Attach(leaseRecord);
                carCareEntities.Entry(leaseRecord).State = EntityState.Added;
                carCareEntities.SaveChanges();
            }

            return leaseRecord;
        }

        public void DeleteLeaseRecord(long leaseId)
        {
            Bootstrapper.Initialise();

            var existingLeaseRecord = carCareEntities.LeaseRecords.FirstOrDefault(i => i.LeaseId == leaseId);

            if (existingLeaseRecord != null)
            {
                carCareEntities.Entry(existingLeaseRecord).State = EntityState.Deleted;
                carCareEntities.SaveChanges();
            }

        }

        #endregion

    }
}