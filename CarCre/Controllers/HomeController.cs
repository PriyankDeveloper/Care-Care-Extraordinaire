using CarCare.BusinessLogic;
using CarCare.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DotNet.Highcharts.Enums;

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

            Highcharts columnChart = new Highcharts("columnchart");

            List<Summary> sm = new List<Summary>();
            var allReppots = BusinessInterface.GetAllServiceRecords().ToList();


            foreach (var type in allReppots.GroupBy(i => i.ServiceTypeId))
            {
                var serviceType = type.First().ServiceType.ServiceName;
                var serviceTypeCount = type.Count();

                sm.Add(new Summary() { y = serviceTypeCount, name = serviceType });
            }

            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Pie,
                PlotBackgroundColor = null,
                PlotBorderWidth = 1,
                PlotShadow = false
            });

            columnChart.SetTitle(new Title()
            {
                Text = "Service Reports"
            });

            columnChart.SetPlotOptions(new PlotOptions() {
                Pie = new PlotOptionsPie() {
                    AllowPointSelect = true,
                    Cursor = Cursors.Pointer,
                    DataLabels = new PlotOptionsPieDataLabels() {               
                        Enabled = false,
                        Formatter = @"function() { return ''+ this.name +': '+ this.y }"
                    },
                    ShowInLegend = true,
                 
                }
            });
            columnChart.SetSeries(new Series
             {
                 Type = ChartTypes.Pie,
                 Name = "Service Report",
                 Data = new Data(sm.ToArray())
             });
           
            ct.highCharts = columnChart;

            return View(ct);
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

public class Summary
{
    public double y { get; set; }
    public string name { get; set; }
}