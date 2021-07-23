using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOweb.Models;
using System.Data;

namespace MOweb.Controllers
{
    public class CallCheckController : Controller
    {
        public string GetDataForGraph()
        {
            //Грузим данные из базы
            DataSet ds1 = GetData.GetDS_For_Graph();

            string[] arr_n = new string[ds1.Tables[0].Columns.Count];

            for (int i1 = 0; i1 < ds1.Tables[0].Columns.Count; i1++) { arr_n[i1] = ""; };

            foreach (DataRow arr in ds1.Tables[0].Rows)
            {
                for (int i1 = 0; i1 < ds1.Tables[0].Columns.Count; i1++)
                    arr_n[i1] = arr_n[i1] + arr.ItemArray[i1].ToString().Replace("NULL", "").Replace(":", ".") + ",";
            }

            for (int i1 = 0; i1 < ds1.Tables[0].Columns.Count; i1++)
            {
                arr_n[i1] = arr_n[i1] + ",";
                arr_n[i1] = arr_n[i1].Replace(",,", "");
            };

            return (arr_n[0] + "|" + arr_n[1] + "|" + arr_n[2] + "|" + arr_n[3] + "|" + arr_n[4] + "|" + arr_n[5]);
        }

        [HttpPost]
        public string GetDataAutoCall()
        {
            //Грузим данные из базы
            DataSet ds1 = GetData.GetDS();

            string rez = "";
            string tmp = "";

            for (int i = 0; i < ds1.Tables.Count; i++)
            {
                foreach (DataRow arr in ds1.Tables[i].Rows)
                {
                    for (int i1 = 0; i1 < ds1.Tables[i].Columns.Count; i1++)
                    {
                        tmp = arr.ItemArray[i1].ToString();
                        if ((i == 0) && (i1 == 5)) { tmp = GetData.SecToStr(tmp); }
                        if ((i == 0) && (i1 == 9)) { if (tmp != "0") tmp = "1"; }
                        if ((i == 0) && (i1 == 10)) { if (tmp != "0") tmp = "1"; }
                        if ((i == 1) && (i1 == 5)) { tmp = GetData.SecToStr(tmp); }
                        if ((i == 1) && (i1 == 6)) { if (tmp != "0") tmp = "1"; }
                        if ((i == 2) && (i1 == 0)) { tmp = GetData.SecToStr(tmp); }
                        if ((i == 2) && (i1 == 1)) { tmp = GetData.SecToStr(tmp); }
                        rez = rez + tmp + ",";
                    }
                }
                if ((rez.Length > 0) && (rez.Substring(rez.Length - 1, 1) == ",")) rez = rez.Substring(0, rez.Length - 1);
                rez = rez + "|";
                //rez = rez.Replace(",|", "|");
            }
            rez = rez + "#";
            rez = rez.Replace("|#", "");

            return (rez);
        }

        public ActionResult CallCheck()
        {
            return View("~/Views/CallCheck/CallCheck.cshtml");
        }

        public ActionResult CallCheckGraph()
        {
            return View("~/Views/CallCheck/CallCheckGraph.cshtml");
        }

    }
}