using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOweb.Models;
using System.IO;
using ClosedXML.Excel;
using Newtonsoft.Json;


namespace MOweb.Controllers
{
    public class MamsyMonitorController : Controller
    {
        public IActionResult MamsyMonitor()
        {
            return View("~/Views/Other/MamsyMonitor.cshtml");
        }

        [HttpPost]
        public string GetMamsyMonitoringValues(string cmp, DateTime fromDate, DateTime toDate, int onlyErrors, int indId)
        {
            return JsonConvert.SerializeObject(Mamsy.GetMamsyMonitoringValuesDataSet(cmp, fromDate, toDate, onlyErrors, indId));
        }

        [HttpPost]
        public string GetMamsyParams()
        {
            return Mamsy.GetMamsyParams();
        }

        [HttpPost]
        public string GetMamsyMonitoringValuesState(int monitoringState)
        {
            return JsonConvert.SerializeObject(Mamsy.GetMamsyMonitoringValuesDataSet_State(monitoringState));
        }

        [HttpPost]
        public string GetMamsyMonitoringValuesItems(int paramId)
        {
            return JsonConvert.SerializeObject(Mamsy.GetMamsyMonitoringValuesDataSet_Items(paramId));
        }

        [HttpPost]
        public IActionResult LoadExcel_gp1(string cmp, DateTime fromDate, DateTime toDate, int onlyErrors, int indId)
        {
            DataSet ds = Mamsy.GetMamsyMonitoringValuesDataSet(cmp, fromDate, toDate, onlyErrors, indId);
            return GetExcelFromDataSet(ds);
        }

        [HttpPost]
        public IActionResult LoadExcel_gp2(int monitoringState)
        {
            DataSet ds = Mamsy.GetMamsyMonitoringValuesDataSet_State(monitoringState);
            return GetExcelFromDataSet(ds);
        }

        [HttpPost]
        public IActionResult LoadExcel_gp3(int paramId)
        {
            DataSet ds = Mamsy.GetMamsyMonitoringValuesDataSet_Items(paramId);
            return GetExcelFromDataSet(ds);
        }

        private IActionResult GetExcelFromDataSet(DataSet ds)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("MSY");
                var currentRow = 1;

                int colNm = 1;
                foreach (var col in ds.Tables[1].Columns)
                {
                    worksheet.Cell(currentRow, colNm).Value = col.ToString();
                    worksheet.Cell(currentRow, colNm).Style.Fill.BackgroundColor = XLColor.Vanilla;
                    colNm++;
                }
                currentRow++;
                foreach (var row in ds.Tables[1].Rows)
                {
                    colNm = 1;
                    foreach (var cell in ((System.Data.DataRow)row).ItemArray)
                    {
                        worksheet.Cell(currentRow, colNm).Value = cell;
                        colNm++;
                    }
                    currentRow++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "msy.xlsx");
                }
            }
        }
    }
}