using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOweb.Models;
using MOweb.Infrastructure;
using AppClasses;
using Newtonsoft.Json;
using System.Text;

namespace MOweb.Controllers
{
    public class MainController : Controller
    {
        CustomerFinder.CustomerFind CF = new CustomerFinder.CustomerFind();
        public IActionResult ToCustomerFind()
        {
            CF.ctrl = this;
            ViewBag.Flag = 1;
            return View("~/Views/Home/CustomerFind.cshtml", CF);
        }

        public IActionResult ShowReports()
        {
            return View("~/Views/Other/ReportsForm.cshtml");
        }

        public IActionResult ShowMRPPickUpForm()
        {
            return View("~/Views/Other/MRPOrderForm.cshtml");
        }

        public IActionResult ShowCaseFindForm()
        {
            return View("~/Views/Case/CaseFind.cshtml");
        }

        public IActionResult ShowWorkShedulerForm()
        {
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

            int cu = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;
            ViewBag.Login = cu.ToString();

            return View("~/Views/WorkSheduler/WorkSheduler.cshtml");
        }


        [HttpPost]
        public string GetAllARMCheckAccesses()
        {
            DataSet armAccesses = new DataSet();
            object[] values = new object[2];
            armAccesses.Tables.Add("ArmAccesses");
            armAccesses.Tables["ArmAccesses"].Columns.Add("T", typeof(int));    // Task
            armAccesses.Tables["ArmAccesses"].Columns.Add("V", typeof(int));    // Value 0 or 1 
            var tls = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).FARMType.TaskList;
            foreach (var tl in tls)
            {
                values.SetValue(tl.Item.id, 0);
                values.SetValue(tl.CurCheck ? 1 : 0, 1);
                armAccesses.Tables["ArmAccesses"].Rows.Add(values);
            }
            //foreach(var )
            return JsonConvert.SerializeObject(armAccesses);//AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).ARMCheckAccess(Task);
        }

        [HttpPost]
        public string GetMRPOrderInfo(int OrderNo)
        {
            return Main.GetdMRPOrderInfo(OrderNo);
        }

        [HttpPost]
        public string AddPickUpDate(int OrderNo, int Host, string strDate)
        {
            int cu = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;
            if (cu > 0) return Main.AddPickUpDate(OrderNo, Host, strDate, cu);
            else return "Запись не произведена. Не определён пользователь";
        }

        [HttpPost]
        public string AddPickUpDateToBaseForOrderList(string orderList, string strDate)
        {
            int cu = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;
            if (cu > 0) return Main.AddPickUpDateToBaseForOrderList(orderList, strDate, cu);
            else return "Запись не произведена. Не определён пользователь";
        }

        [HttpPost]
        public string GetMRPRouteRegistryDifference(DateTime OnDate)
        {
            return Main.GetMRPRouteRegistryDifference(OnDate);
        }

        [HttpPost]
        public string GetMRPConfirmedHungOrders()
        {
            return Main.GetMRPConfirmedHungOrders();
        }

    }
}