using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOweb.Models;
using MOweb.Infrastructure;
using AppClasses;
using ClosedXML.Excel;
using Newtonsoft.Json;

namespace MOweb.Controllers
{
    public class ReportsController : Controller
    {
        [HttpPost]
        public string ReportGetList()
        {
            int cu = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;
            if (cu > 0) return Main.ReportGetList(cu);
            else return "Error";
        }

        [HttpPost]
        public PartialViewResult ReportGetData(string batch)
        {
            int cu = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;
            if (cu > 0) batch += $", @User='{cu}'"; 
            else batch += $", @User=NULL";
            //return JsonConvert.SerializeObject(Main.GetDataSetFromCommand(batch));
            ViewBag.Data = Main.GetDataSetFromCommand(batch);
            return PartialView("~/Views/Other/ReportsData.cshtml");
        }

        [HttpPost]
        public IActionResult ExportToExcel(string batch)
        {
            int cu = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;
            if (cu > 0) batch += $", @User='{cu}'";
            else batch += $", @User=NULL";
            return GetExcelFromDataSet(Main.GetDataSetFromCommand(batch));
        }

        private IActionResult GetExcelFromDataSet(DataSet ds)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Report");
                var currentRow = 1;

                int colNm = 1;
                string [] colName;
                foreach (var col in ds.Tables[0].Columns)
                {
                    colName = col.ToString().Split(" - ");
                    worksheet.Cell(currentRow, colNm).Value = colName.Length > 1 ? colName[1] : colName[0];
                    worksheet.Cell(currentRow, colNm).Style.Fill.BackgroundColor = XLColor.Vanilla;
                    colNm++;
                }
                currentRow++;
                foreach (var row in ds.Tables[0].Rows)
                {
                    colNm = 1;
                    foreach (var cell in ((System.Data.DataRow)row).ItemArray)
                    {
                        worksheet.Cell(currentRow, colNm).SetValue(cell.ToString());
                        colNm++;
                    }
                    currentRow++;
                }
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report.xlsx");
                }
            }
        }
    }
}