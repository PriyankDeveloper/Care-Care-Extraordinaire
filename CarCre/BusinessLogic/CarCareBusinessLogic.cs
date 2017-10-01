using CarCare.CarCareDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarCare.Models;
using System.Data.Entity;
using CarCare.Unity;
using AutoMapper;

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

            return existingUser;
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

        public List<Models.VehicleViewModel> GetAllVehicles()
        {
            var vehicles = carCareEntities.Vehicles.ToList();

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

        public List<CarCareDatabase.ServiceRecord> GetAllServiceRecords()
        {
            return carCareEntities.ServiceRecords.ToList();
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

        public void DeleteServiceRecord(int serviceId)
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

        public List<CarCareDatabase.ServiceStation> GetAllServiceStations()
        {
            return carCareEntities.ServiceStations.ToList();
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

    }
}