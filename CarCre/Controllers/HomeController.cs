using CarCare.BusinessLogic;
using CarCare.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarCare.Controllers
{
    public class HomeController : Controller
    {
        private IBusinessInterface BusinessInterface;

        public HomeController(IBusinessInterface businessInterface)
        {
            BusinessInterface = businessInterface;
        }

        public ActionResult Index()
        {
            Charts ct = new Charts();

            var allVehicles = BusinessInterface.GetAllVehicles().ToList();         

            Highcharts chart = new Highcharts("Chart")
                .SetXAxis(new XAxis
                {
                    Categories = allVehicles.Select(i => i.VINNumber).ToArray()

                })
                .SetSeries(new Series
                {
                    Data = new Data(allVehicles.Select(i=>i.VechicleYear).Distinct().ToArray())
                });
            ct.highCharts = chart;
            return View(ct);
        }

        [HttpGet]
        public JsonResult GetChartData()
        {

            var a = BusinessInterface.GetAllVehicles().GroupBy(item => item.VechicleYear)
        .Select(group => new { Customer = group.Key, Items = group.ToList() })
        .ToList();

        return Json(a, JsonRequestBehavior.AllowGet);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}