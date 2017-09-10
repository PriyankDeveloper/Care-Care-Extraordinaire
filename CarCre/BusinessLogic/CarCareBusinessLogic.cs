using CarCare.CarCareDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarCare.Models;
using System.Data.Entity;

namespace CarCare.BusinessLogic
{
    public class CarCareBusinessLogic : IBusinessInterface
    {
        private CarCareEntities carCareEntities = new CarCareEntities();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IQueryable<CarCareDatabase.User> GetAllUsers()
        {
            return carCareEntities.Users.AsQueryable();
        }

        public bool isValidUser(Models.User model)
        {
          bool isExistingUser = carCareEntities.Users.Any(i=>i.UserName == model.UserName && i.UserPassword == model.UserPassword);

          return isExistingUser;
        }


        public CarCareDatabase.User SaveUser(CarCareDatabase.User user)
        {
            var existingUser = carCareEntities.Users.FirstOrDefault(i => i.UserName == user.UserName && i.UserPassword == user.UserPassword);

            if(existingUser != null)
            {
                carCareEntities.Entry(existingUser).CurrentValues.SetValues(user);
                carCareEntities.SaveChanges();
            }
            else
            {
                carCareEntities.Users.Attach(existingUser);
                carCareEntities.Entry(existingUser).State = EntityState.Modified;
                carCareEntities.SaveChanges();
            }

            return existingUser;
        }
    }
}