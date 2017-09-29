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
    }
}