using CarCare.CarCareDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarCare.Models;
using System.Data.Entity;
using CarCare.Unity;

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

        public bool isValidUser(Models.User model)
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
    }
}