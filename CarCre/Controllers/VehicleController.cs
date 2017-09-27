using CarCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Controllers
{
    public class VehicleController : Controller
    {
        // GET: Vehicle
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SaveVehicle(Vehicle vehicle)
        {

            return Json(true,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTodoLists(string sidx, string sord, int page, int rows)  //Gets the todo Lists.
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            Vehicle vh = new Vehicle()
            {
                OwnerId = 1,
                VechicleDealer = "Test",
                VechicleYear = "2017",
                VehicleId = 12,
                VehicleMark = "Honda",
                VehicleModel = "Accord",
                VINNumber = "ASDF-WER-ERWER"
            };

            int totalRecords = 1;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            var jsonData = new
            {
                total = 1,
                page = 1,
                records = totalRecords,
                rows = vh
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // TODO:insert a new row to the grid logic here
        [HttpPost]
        public string Create([Bind(Exclude = "Id")] Vehicle objTodo)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    //db.TodoLists.Add(objTodo);
                    //db.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfull";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }
        public string Edit(Vehicle objTodo)
        {
            string msg;
            try
            {
                if (ModelState.IsValid)
                {
                    //db.Entry(objTodo).State = EntityState.Modified;
                    //db.SaveChanges();
                    msg = "Saved Successfully";
                }
                else
                {
                    msg = "Validation data not successfull";
                }
            }
            catch (Exception ex)
            {
                msg = "Error occured:" + ex.Message;
            }
            return msg;
        }

        public string Delete(int Id)
        {
            //TodoList todolist = db.TodoLists.Find(Id);
            //db.TodoLists.Remove(todolist);
            //db.SaveChanges();
            return "Deleted successfully";
        }

    }
}