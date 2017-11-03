using CarCare.BusinessLogic;
using CarCare.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DotNet.Highcharts.Enums;
using System;

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
 
            ct.highChartsPie = SetPieChart();
            ct.highCharts3D = Set3DChart();

            return View(ct);
        }


        private Highcharts Set3DChart()
        {
            Highcharts barChart = new Highcharts("barChart");

            List<Summary> sm = new List<Summary>();
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var serviceRecords = BusinessInterface.GetAllServiceRecords(userId);
            var allReppots = serviceRecords.ToList();

            List<Point> count = new List<Point>();
            DateTime dt = DateTime.Now;

            for(int i=0;i<10;i++)
            {
                
                switch(i)
                {
                    case 0:
                        var co = allReppots.Count(c => c.ServiceDate.Value.Date == dt.Date);
                        count.Add(new Point() { Y = co, Color = System.Drawing.Color.Red});
                        break;
                    case 1:
                        var co1 = allReppots.Count(c => c.ServiceDate.Value.Date == dt.Date.AddDays(-1));
                        count.Add(new Point() { Y = co1, Color = System.Drawing.Color.Blue });
                        break;
                    case 2:
                        var co2 = allReppots.Count(c => c.ServiceDate.Value.Date == dt.Date.AddDays(-2));
                        count.Add(new Point() { Y = co2, Color = System.Drawing.Color.DarkKhaki });
                        break;
                    case 3:
                        var co3 = allReppots.Count(c => c.ServiceDate.Value.Date == dt.Date.AddDays(-3));
                        count.Add(new Point() { Y = co3, Color = System.Drawing.Color.DarkOrange });
                        break;
                    case 4:
                        var co4 = allReppots.Count(c => c.ServiceDate.Value.Date == dt.Date.AddDays(-4));
                        count.Add(new Point() { Y = co4, Color = System.Drawing.Color.DarkViolet });
                        break;
                    case 5:
                        var co5 = allReppots.Count(c => c.ServiceDate.Value.Date == dt.Date.AddDays(-5));
                        count.Add(new Point() { Y = co5, Color = System.Drawing.Color.Gainsboro });
                        break;
                    case 6:
                        var co6 = allReppots.Count(c => c.ServiceDate.Value.Date == dt.Date.AddDays(-6));
                        count.Add(new Point() { Y = co6, Color = System.Drawing.Color.PaleTurquoise });
                        break;
                    case 7:
                        var co7 = allReppots.Count(c => c.ServiceDate.Value.Date == dt.Date.AddDays(-7));
                        count.Add(new Point() { Y = co7, Color = System.Drawing.Color.Indigo });
                        break;
                    case 8:
                        var co8 = allReppots.Count(c => c.ServiceDate.Value.Date == dt.Date.AddDays(-8));
                        count.Add(new Point() { Y = co8, Color = System.Drawing.Color.LightYellow });
                        break;
                    case 9:
                        var co9 = allReppots.Count(c => c.ServiceDate.Value.Date == dt.Date.AddDays(-9));
                        count.Add(new Point() { Y = co9, Color = System.Drawing.Color.MediumOrchid });
                        break;
                    case 10:
                        var co10 = allReppots.Count(c => c.ServiceDate.Value.Date == dt.Date.AddDays(-10));
                        count.Add(new Point() { Y = co10, Color = System.Drawing.Color.Navy });
                        break;
                }             
            }

            barChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Column, 
                Options3d = new ChartOptions3d() {
                    Enabled = true,
                    Alpha = 10,
                    Beta = 25,
                    Depth = 70
                }
            });

            barChart.SetTitle(new Title()
            {
                Text = "Services perform in last 10 days"
            });

            barChart.SetPlotOptions(new PlotOptions() {
                Column = new PlotOptionsColumn() {
                    Depth = 25
                }
            });

            barChart.SetXAxis(new XAxis
            {
                Categories = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" }
            });

            barChart.SetYAxis(new YAxis
            {
               Title = null
            });

            Data data = new Data(count.ToArray());
           barChart.SetSeries(new Series
            {
                Type = ChartTypes.Column,
                Name = "Services Perform",
                Data = data
            });

            return barChart;

        }

        private Highcharts SetPieChart()
        {
            Highcharts pieChart = new Highcharts("pieChart");

            List<Summary> sm = new List<Summary>();
            long userId = BusinessInterface.getUserIdFromCookie(HttpContext.Request.Cookies);
            var serviceRecords = BusinessInterface.GetAllServiceRecords(userId);
            var allReppots = serviceRecords.ToList();


            foreach (var type in allReppots.GroupBy(i => i.ServiceTypeId))
            {
                var serviceType = type.First().ServiceType.ServiceName;
                var serviceTypeCount = type.Count();

                sm.Add(new Summary() { y = serviceTypeCount, name = serviceType });
            }

            pieChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Pie,
                PlotBackgroundColor = null,
                PlotBorderWidth = 1,
                PlotShadow = false
            });

            pieChart.SetTitle(new Title()
            {
                Text = "Service Reports"
            });

            pieChart.SetPlotOptions(new PlotOptions()
            {
                Pie = new PlotOptionsPie()
                {
                    AllowPointSelect = true,
                    Cursor = Cursors.Pointer,
                    DataLabels = new PlotOptionsPieDataLabels()
                    {
                        Enabled = false,
                        Formatter = @"function() { return ''+ this.name +': '+ this.y }"
                    },
                    ShowInLegend = true,

                }
            });
            pieChart.SetSeries(new Series
            {
                Type = ChartTypes.Pie,
                Name = "Service Report",
                Data = new Data(sm.ToArray())
            });

            return pieChart;

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