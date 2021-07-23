using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOweb.Models;
using MOweb.Infrastructure;
using AppClasses;
using System.Data;


namespace MOweb.Controllers
{
    public class HomeController : Controller
    {
        List<Orders.Order> OD = OrdersMapper.OrderListCreate("").ListOrders;
        CustomerFinder.CustomerFind CF = new CustomerFinder.CustomerFind();

    public IActionResult Index()
        {

            string app_id = HttpContext.Session.GetJson<string>("AppId");

            if (app_id == null)
            {
                LoginFrm lf = new LoginFrm();
                return View("LoginForm", lf);
            }
            else
            {
                // >> 20200618 kaa
                //CF.ctrl = this;
                ///return View("CustomerFind", CF);
                // << 20200618 kaa
                return View("~/Views/Main.cshtml"); 
            }
        }

        public ActionResult LoginForm(LoginFrm lf)
        {
            if (HttpContext.Session.GetJson<string>("AppId") == null)
            {
                AppInstance app = AppInstanceList.CheckUserPwd(lf.login, lf.password);
                if (app == null)
                {
                    lf.password = null;
                    lf.ErrMsg = "Неправильный идентификатор или пароль";
                    return View("LoginForm", lf);
                }
                else //удачный вход
                {
                    if (app.CurrentUser.NeedPassChanges > 0) //Но надо сменить пароль
                    {
                        ChangePasswordForm cpf = new ChangePasswordForm();
                        cpf.AppId = app.Id;
                        // cpf.ErrMsg = "Необходимо сленить пароль";
                        return View("ChangePasswordForm", cpf);
                    }
                    else
                    {
                        //AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId")))
                        HttpContext.Session.SetJson("AppId", app.Id);
                        // >> 20200618 kaa
                        //CF.ctrl = this;
                        //ViewBag.Flag = 1;
                        //return View("CustomerFind", CF);
                        // << 20200618 kaa
                        ViewBag.AppId = app.Id;
                        ViewBag.AccountId = app.CurrentUser.AccountId;
                        return View("~/Views/Main.cshtml");
                    }

                }
            }
            else return View("~/Views/Main.cshtml");
        } //LoginForm

        public ActionResult LoginForm1(LoginFrm lf)
        {
            AppInstance app = AppInstanceList.CheckUserPwd(lf.login, lf.password);
            if (app == null)
            {
                lf.password = null;
                lf.ErrMsg = "Неправильный идентификатор или пароль";
                return View("LoginForm", lf);
            }
            else //удачный вход
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

                DataSet ds2 = GetData.Get_user_uic(lf.login);
                ViewBag.Login = lf.login + "|" + ds2.Tables[0].Rows[0].ItemArray[0].ToString();

                return View("~/Views/WorkSheduler/WorkSheduler.cshtml");

            }
        } //LoginForm

        public ActionResult ChangePasswordForm(ChangePasswordForm cpf)
        {
            AppInstance App = AppInstanceList.GetItemsById(cpf.AppId);

            if (String.Compare(App.CurrentUser.md5_pass, cpf.old_password, true) != 0)
//            if (App.CurrentUser.md5_pass != cpf.old_password)
            {
                cpf.ErrMsg = "Неправильный старый пароль. Повторите попытку";
                return View("ChangePasswordForm", cpf);
            }
            else
            {
                if (App.CurrentUser.ChangePwd(cpf.new_password))
                {
                    HttpContext.Session.SetJson("AppId", App.Id);
                    App.NeedConnectToCollOColl = 1;
                    CF.ctrl = this;
                    //return View("CustomerFind", CF);
                    

                    return View("~/Views/Main.cshtml");

                }
            }
            return View("ChangePasswordForm", cpf);
        }

        public void ResetNeededConnectToCallOColl(int AppId)
        {
            AppInstance App = AppInstanceList.GetItemsById(AppId);
            App.NeedConnectToCollOColl = 0;
        }

        public string MakeCall(string Phone, string Line, int Customer = 0, int Host= 3,
            int TypeClientCard = 1)
        {
            ModelBase mb = new ModelBase();
            mb.ctrl = this;
            mb.App.MakeCall(Phone, Line, Customer, Host, TypeClientCard);

            return "";
        }

        public string ExistsCardForCall()
        {
            ModelBase mb = new ModelBase();
            mb.ctrl = this;
            if (mb.App != null)
            {
                if (mb.App.ExistsCardForCall() == 1)
                {
                    string s = mb.App.CustomerCardParamList[0].OrderNo.ToString() +
                        "|" + mb.App.CustomerCardParamList[0].Host.ToString() +
                        "|" + mb.App.CustomerCardParamList[0].Customer.ToString();
                    mb.App.CustomerCardParamList.RemoveAt(0);
                    return s;
                }
                else { return ""; }

            }
            else { return ""; }
        }


        public ActionResult ShowOrderByPhone(string Phone, string Line, int IdApp)
        {
            HttpContext.Session.SetJson("AppId", IdApp);
            string ParamStr = "Phone=" + Phone + ",Line=" + Line;

            ModelBase mb = new ModelBase();
            mb.ctrl = this;
            CustomerCardParam ccp = new CustomerCardParam();

            mb.App.GetCardFor(ParamStr, ref ccp);

            if (ccp.Customer != 0)
            {
                string fragment = "CustomerId=" + ccp.Customer +
                    "&Host=" + ccp.Host + "&OrderNo=0";

                return RedirectToAction("ShowCustomerOrders", "Order",
                 new
                 {
                     CustomerId = ccp.Customer,
                     Host = ccp.Host,
                     OrderNo = 0
                 });
            }
            return null;
        }

        public ActionResult RedirectTest()
        {
            return Redirect("~/Order/ShowOrderHost?OrderNo=1369743&HostId=1");

           // '@Url.Action("ShowOrderHost", "Order")' + '?' + 'OrderNo=@customerinfo.CI_Order' + '&HostId=@customerinfo.CI_Host'">

        }



        public ActionResult ShowCardForCall()
        {
            ModelBase mb = new ModelBase();
            mb.ctrl = this;

            return View("CustomerFind", CF);
        }
        






        public ActionResult ShowOrderList(int customer, int host, DateTime datefrom, DateTime dateto)
        {
            Console.WriteLine(customer + " " + host + " " + datefrom + " " + dateto);
            return View();
        }

        public ActionResult ShowId(string id)
        {
            Console.WriteLine("id-00000000000000000000000 " + id);
            return View("contact");
        }

        public ActionResult ShowModal()
        {
            Console.WriteLine("id-00000000000000000000000 ");
            return View("modal");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Test2()
        {
            return View();
        }

        public ActionResult Test3()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        /*
        public ActionResult FindCustomer(string mobile, int customer, int order, string email)
        {
            Console.WriteLine(mobile + " " + customer + " " + order + " " + email);

            if (mobile != "" ^ customer != 0 ^ order != 0 ^ email != "")
            {
                CF.FindMobile = mobile;
                CF.FindCustomer = customer;
                CF.FindOrder = order;
                CF.FindEmail = email;
                //return View("Index", OD);
                CF.FindCustomers(customer);
            }
            return View("CustomerFind", CF);
        }
        */
        public ActionResult FindCustomerEmail(string email)
        {
            CF.ctrl = this;
            if (email != null)
            {
                CF.FindEmail = email;
                CF.FindCustomerEmail(email);
            }
            return View("CustomerFind", CF);
        }

        [HttpPost]
        public ActionResult FindCustomerId(int customer)
        {
            CF.ctrl = this;
            if (customer != 0)
            {
                CF.FindCustomer = customer;
                CF.FindCustomerId(customer);
            }
            return View("CustomerFind", CF);
        }

        [HttpPost]
        public ActionResult FindCustomerPhone(string phone)
        {
            CF.ctrl = this;
            if (phone != null)
            {
                CF.FindMobile= phone;
                CF.FindCustomerPhone(phone);
            }
            return View("CustomerFind", CF);
        }

        [HttpPost]
        public ActionResult FindCustomerOrder(int order)
        {
            CF.ctrl = this;
            if (order != 0)
            {
                CF.FindOrder = order;
                CF.FindCustomerOrder(order);
            }
            return View("CustomerFind", CF);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult ShowAreaEditor()
        {
            return View("~/Views/AreaEditor/AreaEditor.cshtml");
        }
    }
}
