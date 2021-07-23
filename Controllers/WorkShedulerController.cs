using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using MOweb.Models;

namespace MOweb.Controllers
{
    public class WorkShedulerController : Controller
    {
        public ActionResult WorkSheduler()
        {
            //Грузим данные из базы
            DataSet ds1 = GetData.Get_name_MRP();

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

            ViewBag.Arr1 = arr_n[0];// json1.Replace("[", "").Replace("]", "").Replace(" ","");
            ViewBag.Arr2 = arr_n[1];//a2;// json2.Replace("\"","").Replace("[", "").Replace("]", "").Replace(" ","");

            ViewBag.Countrow = ds1.Tables[0].Rows.Count;

            return View("~/Views/WorkSheduler/WorkSheduler.cshtml");
        }

        [HttpPost]
        public string DataForTable(string mrpid, string mes, string god)
        {
            DataSet ds1 = GetData.Get_MRP_Work_Schedule(mrpid, mes, god);

            string rez = "";

            for (int i = 0; i < ds1.Tables.Count; i++)
            {
                foreach (DataRow arr in ds1.Tables[i].Rows)
                {
                    for (int i1 = 0; i1 < ds1.Tables[i].Columns.Count; i1++)
                        rez = rez + arr.ItemArray[i1].ToString() + "~";
                }
                if ((rez.Length > 0) && (rez.Substring(rez.Length - 1, 1) == "~")) rez = rez.Substring(0, rez.Length - 1);
                rez = rez + "|";
                //rez = rez.Replace(",|", "|");
            }
            rez = rez + "#";
            rez = rez.Replace("|#", "");

            return (rez);
        }

        [HttpPost]
        public string SaveWeekDay(string mrpid, string day, string wid, string tbeg, string tend, string tcut, string loglog)
        {
            string[] arrlog = (loglog + ' ').Split('|');
            DataSet ds1 = GetData.MRP_Insert_Day(mrpid, day, wid, tbeg, tend, tcut, arrlog[0], arrlog[1]);

            if (ds1.Tables.Count == 0)
            {
                return "Ошибка обращения к серверу";
            }

            return ds1.Tables[0].Rows[0].ItemArray[0].ToString();
        }

        [HttpPost]
        public string DeleteWeekDay(string mrpid, string day, string loglog)
        {
            string[] arrlog = (loglog + ' ').Split('|');
            DataSet ds1 = GetData.MRP_Delete_Day(mrpid, day, arrlog[0], arrlog[1]);

            if (ds1.Tables.Count == 0)
            {
                return "Ошибка обращения к серверу";
            }

            return ds1.Tables[0].Rows[0].ItemArray[0].ToString();
        }

        [HttpPost]
        public string Get_user_login(string id_for_log)
        {
            DataSet ds1 = GetData.Get_user_uic(id_for_log);

            if (ds1.Tables.Count == 0)
            {
                return "нет|";
            }

            return ds1.Tables[0].Rows[0].ItemArray[5].ToString() + "|";
        }

        [HttpPost]
        public string SaveSpecDatе(string mrpid, string dat, string wid, string tbeg, string tend, string tcut, string loglog)
        {
            string[] arrlog = (loglog + ' ').Split('|');
            DataSet ds1 = GetData.MRP_Insert_Date(mrpid, dat, wid, tbeg, tend, tcut, arrlog[0], arrlog[1]);

            if (ds1.Tables.Count == 0)
            {
                return "Ошибка обращения к серверу";
            }

            return ds1.Tables[0].Rows[0].ItemArray[0].ToString();
        }

        [HttpPost]
        public string DeleteSpecDate(string mrpid, string dat, string loglog)
        {
            string[] arrlog = (loglog + ' ').Split('|');
            DataSet ds1 = GetData.MRP_Delete_Specdate(mrpid, dat, arrlog[0], arrlog[1]);

            if (ds1.Tables.Count == 0)
            {
                return "Ошибка обращения к серверу";
            }

            return ds1.Tables[0].Rows[0].ItemArray[0].ToString();
        }

        [HttpPost]
        public string UpdateCutAll(string mrpid, string tbeg, string tend, string tcut, string loglog)
        {

            string[] arrlog = (loglog + ' ').Split('|');
            DataSet ds1 = GetData.MRP_Update_CitAlll(mrpid, tbeg, tend, tcut, arrlog[0], arrlog[1]);

            if (ds1.Tables.Count == 0)
            {
                return "Ошибка обращения к серверу";
            }

            return ds1.Tables[0].Rows[0].ItemArray[0].ToString();
        }
    }
}