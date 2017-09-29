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
        CarCareDatabase.User SaveUser(CarCare.CarCareDatabase.User user);

        IQueryable<CarCareDatabase.User> GetAllUsers();

        bool isValidUser(Models.UserViewModel model);

        CarCareDatabase.Vehicle SaveVehicle(CarCare.CarCareDatabase.Vehicle vehicle);

        List<Models.VehicleViewModel> GetAllVehicles();

        void DeleteVehicle(int vehicleId);
    }
}
