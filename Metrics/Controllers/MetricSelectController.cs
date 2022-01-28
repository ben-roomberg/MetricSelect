using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Metrics.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Metrics.Controllers
{
    public class MetricSelectController : Controller
    {
        static MetricSelectController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("metricselect/availablemetrics")]
        public JsonResult AvailableMetrics()
        {
            return Json(Metric.GetAvailableTest());
        }


        [Route("metricselect/getreportdata")]
        [HttpPost]
        public JsonResult GetReportData([FromBody]int[] selectedMetricIds)
        {
            //I am passing ids and retrieving the list of available metrics in case any are invalid
            //and because I want Metric.GetAvailable to determine the order
            Report report = new Report() { SelectedMetrics = Metric.GetAvailableTest().Where(m => selectedMetricIds.Contains(m.Id)) };
            DataTable data = report.GetDataTest();
            //splitting datatable into rows and columns, because Json() conversion of datatable causes self reference issues
            //and a fix with services.AddControllersWithViews().AddNewtonsoftJson with the ignore self reference option does not serialize column info
            return Json(new { data = data.Rows.Cast<DataRow>().Select(r => r.ItemArray), columns = data.Columns.Cast<DataColumn>().Select(c => c.ColumnName) });
        }
    }
}
