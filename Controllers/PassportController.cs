using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MOweb.Models;
using MOweb.Models.ViewModels;
using MOweb.Infrastructure;
using Newtonsoft.Json;
using MOweb.Controllers;
using AppClasses;

namespace MOweb.Controllers
{

    public class SessPassport // сессия по работе с паспортом 
    {
        public int? customerid { get; set; }
        public int? host { get; set; }
        public int? key { get; set; }

        public SessPassport(int? c, int? h, int? k)
        {
            customerid = c;
            host = h;
            key = k;
        }
        
    }

    //HttpContext.Sessio//.Session.SetJson();
    public class PassportController : Controller
    {
        
        public IActionResult Index(int customerid, int host)
        {


            //Test t777 = new Test();
            //t777.t1 = "121";

            //string json111 = JsonConvert.SerializeObject(new Test());
            //Test ttt = JsonConvert.DeserializeObject<Test>(json111);

            // Получаем ПД для редактирования
            //Passport psp0 = PassportsMapper.getServerPassp();

            // Пытаемся считать из сеанса запись по идентификатору Passp
            SessPassport sp = HttpContext.Session.GetJson<SessPassport>("Passp");
            Passport psp0;
            PassportViews.PasspView psp2;

            // Для работы с пользователями
            ModelBase mb = new ModelBase();
            mb.ctrl = this;

            if (sp == null)
            {
                // Получаем ПД для редактирования
                psp0 = PassportsMapper.getServerPassp(customerid, host);
                // если  sp == null, т.е. в сеансе нет записи с идентификатором Passp, то записываем в сеанс новую запись Passp
                HttpContext.Session.SetJson("Passp", new SessPassport(psp0.Customerid, psp0.Host, psp0.Key));
                psp2 = PassportViews.findPassp(psp0.Customerid, psp0.Host, psp0.Key);
                psp2.AccountId = mb.App.CurrentUser.AccountId;//для работы с пользователями
            }
            else
            {
                // Запрашиваем ПД, сохраненные в Mapper(е) т.к. сеанс не был завершен и данные в Mapper должны были остаться.
                // При этом данные из Mapper по конкретным  ПД удаляются только при завершении сеанса.
                psp0 = PassportsMapper.findPassp(sp.customerid, sp.host, sp.key);
                psp2 = PassportViews.findPassp(psp0.Customerid, psp0.Host, psp0.Key);
            }

            //SessPassport sp11 = HttpContext.Session.GetJson<SessPassport>("Passp");

            //psp0.PageForShow = 1;
            //psp0.PageNext = 1;
            //PassportViews.psp1view.PageForShow = 1;
            //PassportViews.psp1view.PageNext = 1;
            //PassportViews.psp1view.Customerid = psp0.Customerid;
            //PassportViews.psp1view.Host = psp0.Host;

            // Создаём объект PasspView (это по сути ViewModel для объекта Паспорт) на основе psp0, т.е. на основе объекта полученного из Mapper(а)  
            //PassportViews.PasspView psp2 = PassportViews.findPassp(psp0.Customerid, psp0.Host, psp0.Key);
            psp2.PageForShow = 1;
            psp2.PageNext = 1;
            psp2.Customerid = psp0.Customerid;
            psp2.Host = psp0.Host;

            @ViewBag.PageForShowVB = 1;
            //return Redirect("~/Passport/List?Page=1&customerid="+ psp0.Customerid + "&host="+ psp0.Host);
            return View("Passport", psp2);
        }
        public ViewResult List(int Page, int customerid, int host, int key)
        {

            //PassportViews.PasspView psp20 = PassportViews.findPassp(Customerid, Host);

           // @ViewBag.PageForShowVB = psp2.PageNext;//Page;
            //psp.FirstName = FirstName;
            //psp.PageForShow = Page;
            //PassportViews.psp1view.PageForShow = Page;


            PassportViews.PasspView psp2;

            if (customerid != 0)
            {
                psp2 = PassportViews.findPassp(customerid, host, key);
                psp2.PageForShow = psp2.PageNext;
                @ViewBag.PageForShowVB = psp2.PageNext;
                psp2.PageNext = Page;
            }
            else
            {
                psp2 = new PassportViews.PasspView(new Passport(421470349, 1));
                psp2.PageForShow = psp2.PageNext; 
                @ViewBag.PageForShowVB = psp2.PageNext;
                psp2.PageNext = Page;

            }
            return View("Passport", psp2);

        }


        [HttpPost]
        public IActionResult RsvpForm(PassportViews.PasspView passp)
        {
            //if (ModelState.IsValid)
            //{
            //    Repository.AddResponse(guestResponse);
            //   return View("Thanks", guestResponse);
            //}
            //else
            //{
            //PassportsMapper.psp1.LastName = passp.LastName;

            //PassportViews.PasspView psp1view = null;

            SessPassport sp = HttpContext.Session.GetJson<SessPassport>("Passp"); // можно в принципе пердавать и через объект модели представления

            PassportViews.PasspView psp1view = null;

            psp1view = PassportViews.findPassp(passp.Customerid,passp.Host,sp.key);

            /*
            psp1view.Mobile = passp.Mobile;
            psp1view.Phone = passp.Phone;
            psp1view.Name = passp.Name;
            psp1view.Email = passp.Email;
  
            psp1view.Customerid = passp.Customerid;
            psp1view.Host = passp.Host;
            psp1view.Author = passp.Author;
            psp1view.User = passp.User;
            psp1view.CancelReason = passp.CancelReason;
            */

            if (passp.PageForShow == 1)
            {

            psp1view.Surname = passp.Surname;
            psp1view.Name = passp.Name;
            psp1view.Patronimic = passp.Patronimic;
            
            psp1view.PassRange = passp.PassRange;
            psp1view.PassNumber = passp.PassNumber;
            psp1view.PassIssueDate = passp.PassIssueDate;
            psp1view.Birthday = passp.Birthday;
            psp1view.DeliveryINN = passp.DeliveryINN;
            
            psp1view.PassDepartmentCode = passp.PassDepartmentCode;
            psp1view.PassDepartmentAddr = passp.PassDepartmentAddr;
            psp1view.PassRegisteredAddress = passp.PassRegisteredAddress;
            }


            //PassportViews.psp1view.DocType                = passp.DocType                  ;
            //PassportViews.psp1view.SurnameRF              = passp.SurnameRF                ;
            //PassportViews.psp1view.NameRF                 = passp.NameRF                   ;
            //PassportViews.psp1view.PatronymicRF           = passp.PatronymicRF             ;
            //PassportViews.psp1view.PassRangeRF            = passp.PassRangeRF              ;
            //PassportViews.psp1view.PassNumberRF           = passp.PassNumberRF             ;
            //PassportViews.psp1view.PassDepartmentAddrRF   = passp.PassDepartmentAddrRF     ;
            //PassportViews.psp1view.PassIssueDateRF        = passp.PassIssueDateRF          ;
            //PassportViews.psp1view.BirthdayRF             = passp.BirthdayRF               ;


            if (passp.PageForShow == 2)
            {
                psp1view.SurnameNerez = passp.SurnameNerez;
                psp1view.NameNerez = passp.NameNerez;
                psp1view.PatronymicNerez = passp.PatronymicNerez;
                psp1view.PassRangeNerez = passp.PassRangeNerez;
                psp1view.PassNumberNerez = passp.PassNumberNerez;
                psp1view.PassDepartmentAddrNerez = passp.PassDepartmentAddrNerez;
                psp1view.PassIssueDateNerez = passp.PassIssueDateNerez;
            }
            //PassportViews.psp1view.BirthdayNerez           = passp.BirthdayNerez            ;
                                                                                         
            if (passp.PageForShow == 3)
            {
                psp1view.SurnameZagr = passp.SurnameZagr;
                psp1view.NameZagr = passp.NameZagr;
                psp1view.PatronymicZagr = passp.PatronymicZagr;
                psp1view.PassRangeZagr = passp.PassRangeZagr;
                psp1view.PassNumberZagr = passp.PassNumberZagr;
                psp1view.PassDepartmentAddrZagr = passp.PassDepartmentAddrZagr;
                psp1view.PassIssueDateZagr = passp.PassIssueDateZagr;
                psp1view.BirthdayZagr = passp.BirthdayZagr;
            }
            //PassportViews.psp1view. BirthdayZagr           = passp.BirthdayZagr             ;

            //PassportViews.psp1view.SurnameUd              = passp.SurnameUd                ;
            //PassportViews.psp1view.NameUd                 = passp.NameUd                   ;
            //PassportViews.psp1view.PatronymicUd           = passp.PatronymicUd             ;
            //PassportViews.psp1view.PassNumberUd           = passp.PassNumberUd             ;
            //PassportViews.psp1view.PassDepartmentAddrUd   = passp.PassDepartmentAddrUd     ;
            //PassportViews.psp1view.PassIssueDateUd        = passp.PassIssueDateUd          ;
            //PassportViews.psp1view.BirthdayUd             = passp.BirthdayUd               ;

            if (passp.PageForShow == 4)
            {
                psp1view.CancelReason = passp.CancelReason;
            }  

            if (passp.PageForShow == 5)
            {
                psp1view.CustomerPostCode           = passp.CustomerPostCode;
                psp1view.CustomerCity               = passp.CustomerCity;
                psp1view.CustomerAdressStreet       = passp.CustomerAdressStreet;
                psp1view.CustomerAdressHouse        = passp.CustomerAdressHouse;
                psp1view.CustomerAdressCorps        = passp.CustomerAdressCorps;
                psp1view.CustomerAdressApartment    = passp.CustomerAdressApartment;
            }

            psp1view.PageForShow = passp.PageForShow;
            psp1view.PageNext = passp.PageNext;

            //Теперь меняем в passpViewList
            PassportViews.passpViewList.Remove(PassportViews.passpViewList.Find(passp1 => (passp1.Key == sp.key && passp1.Customerid == passp.Customerid && passp1.Host == passp.Host)));
            PassportViews.passpViewList.Add(psp1view);
            //passp.Phone = "89898989";
            return Redirect("~/Passport/List?Page="+ psp1view.PageNext.ToString() + "&customerid=" + psp1view.Customerid + "&host=" + psp1view.Host + "&key=" + psp1view.Key);  // https://localhost:44306/Passport/List?Page=1
            //return View("Passport", PassportsMapper.psp1);
            //}

        }

        public IActionResult RsvpFormOK(PassportViews.PasspView passp)
        {
            //if (ModelState.IsValid)
            //{
            //    Repository.AddResponse(guestResponse);
            //   return View("Thanks", guestResponse);
            //}
            //else
            //{
            //PassportsMapper.psp1.LastName = passp.LastName;

            /*
                PassportsMapper.psp1map.Customerid              = psp1view.Customerid;
                PassportsMapper.psp1map.Host                    = psp1view.Host;
                PassportsMapper.psp1map.Author                  = psp1view.Author;
                PassportsMapper.psp1map.User                    = psp1view.User;
                                
                PassportsMapper.psp1map.Surname                 = psp1view.Surname;
                PassportsMapper.psp1map.Name                    = psp1view.Name;
                PassportsMapper.psp1map.Patronimic              = psp1view.Patronimic;
                                
                PassportsMapper.psp1map.PassRange               = psp1view.PassRange;
                PassportsMapper.psp1map.PassNumber              = psp1view.PassNumber;
                PassportsMapper.psp1map.PassIssueDate           = psp1view.PassIssueDate;
                PassportsMapper.psp1map.Birthday                = psp1view.Birthday;
                PassportsMapper.psp1map.DeliveryINN             = psp1view.DeliveryINN;
                                
                PassportsMapper.psp1map.PassDepartmentCode      = psp1view.PassDepartmentCode;
                PassportsMapper.psp1map.PassDepartmentAddr      = psp1view.PassDepartmentAddr;
                PassportsMapper.psp1map.PassRegisteredAddress   = psp1view.PassRegisteredAddress;
                                
                PassportsMapper.psp1map.SurnameNerez            = psp1view.SurnameNerez ;
                PassportsMapper.psp1map.NameNerez               = psp1view.NameNerez;
                PassportsMapper.psp1map.PatronymicNerez         = psp1view.PatronymicNerez ;
                PassportsMapper.psp1map.PassRangeNerez          = psp1view.PassRangeNerez ;
                PassportsMapper.psp1map.PassNumberNerez         = psp1view.PassNumberNerez ;
                PassportsMapper.psp1map.PassDepartmentAddrNerez = psp1view.PassDepartmentAddrNerez ;
                PassportsMapper.psp1map.PassIssueDateNerez      = psp1view.PassIssueDateNerez ;
                                
                PassportsMapper.psp1map.SurnameZagr             = psp1view.SurnameZagr ;
                PassportsMapper.psp1map.NameZagr                = psp1view.NameZagr ;
                PassportsMapper.psp1map.PatronymicZagr          = psp1view.PatronymicZagr ;
                PassportsMapper.psp1map.PassRangeZagr           = psp1view.PassRangeZagr ;
                PassportsMapper.psp1map.PassNumberZagr          = psp1view.PassNumberZagr ;
                PassportsMapper.psp1map.PassDepartmentAddrZagr  = psp1view.PassDepartmentAddrZagr ;
                PassportsMapper.psp1map.PassIssueDateZagr       = psp1view.PassIssueDateZagr ;
                PassportsMapper.psp1map.BirthdayZagr            = psp1view.BirthdayZagr ;
                                
                PassportsMapper.psp1map.CancelReason            = psp1view.CancelReason ;
                                
                PassportsMapper.psp1map.CustomerPostCode        = psp1view.CustomerPostCode ;
                PassportsMapper.psp1map.CustomerCity            = psp1view.CustomerCity ;
                PassportsMapper.psp1map.CustomerAdressStreet    = psp1view.CustomerAdressStreet;
                PassportsMapper.psp1map.CustomerAdressHouse     = psp1view.CustomerAdressHouse ;
                PassportsMapper.psp1map.CustomerAdressCorps     = psp1view.CustomerAdressCorps;
                PassportsMapper.psp1map.CustomerAdressApartment = psp1view.CustomerAdressApartment;
*/



            //passp.Phone = "89898989";
            //return View("~/Home/CustomerFind");  // https://localhost:44306/Passport/List?Page=1
            SessPassport sp11 = HttpContext.Session.GetJson<SessPassport>("Passp");

            // Удаляем текущий контекст сессии
            //Session.Remove("Passp");

            //SessPassport sp = HttpContext.Session.GetJson<SessPassport>("Passp");

            //sp.host = sp11.host;
            passp.Key = sp11.key;

            //Теперь меняем в passpViewList
            PassportViews.passpViewList.Remove(PassportViews.passpViewList.Find(passp1 => (passp1.Key == passp.Key && passp1.Customerid == passp.Customerid && passp1.Host == passp.Host)));
            PassportViews.passpViewList.Add(passp);

            Passport passp33 = PassportsMapper.savePassp(passp);

            //Теперь меняем в passpViewList уже по факту сохранения на сервере
            PassportViews.passpViewList.Remove(PassportViews.passpViewList.Find(passp1 => (passp1.Key == passp33.Key && passp1.Customerid == passp33.Customerid && passp1.Host == passp33.Host)));
            PassportViews.passpViewList.Add(new PassportViews.PasspView(passp33));

            //return Redirect("~/Home");  // https://localhost:44306/Passport/List?Page=1
            return Redirect("~/Passport/List?Page=" + passp.PageNext.ToString() + "&customerid=" + passp.Customerid + "&host=" + passp.Host + "&key=" + passp.Key);
            //return "Данные сохранены!";
            //}

        }

        public IActionResult RsvpFormOKOK(PassportViews.PasspView passp)
        {
            //if (ModelState.IsValid)
            //{
            //    Repository.AddResponse(guestResponse);
            //   return View("Thanks", guestResponse);
            //}
            //else
            //{
            //PassportsMapper.psp1.LastName = passp.LastName;

            /*
                PassportsMapper.psp1map.Customerid              = psp1view.Customerid;
                PassportsMapper.psp1map.Host                    = psp1view.Host;
                PassportsMapper.psp1map.Author                  = psp1view.Author;
                PassportsMapper.psp1map.User                    = psp1view.User;
                                
                PassportsMapper.psp1map.Surname                 = psp1view.Surname;
                PassportsMapper.psp1map.Name                    = psp1view.Name;
                PassportsMapper.psp1map.Patronimic              = psp1view.Patronimic;
                                
                PassportsMapper.psp1map.PassRange               = psp1view.PassRange;
                PassportsMapper.psp1map.PassNumber              = psp1view.PassNumber;
                PassportsMapper.psp1map.PassIssueDate           = psp1view.PassIssueDate;
                PassportsMapper.psp1map.Birthday                = psp1view.Birthday;
                PassportsMapper.psp1map.DeliveryINN             = psp1view.DeliveryINN;
                                
                PassportsMapper.psp1map.PassDepartmentCode      = psp1view.PassDepartmentCode;
                PassportsMapper.psp1map.PassDepartmentAddr      = psp1view.PassDepartmentAddr;
                PassportsMapper.psp1map.PassRegisteredAddress   = psp1view.PassRegisteredAddress;
                                
                PassportsMapper.psp1map.SurnameNerez            = psp1view.SurnameNerez ;
                PassportsMapper.psp1map.NameNerez               = psp1view.NameNerez;
                PassportsMapper.psp1map.PatronymicNerez         = psp1view.PatronymicNerez ;
                PassportsMapper.psp1map.PassRangeNerez          = psp1view.PassRangeNerez ;
                PassportsMapper.psp1map.PassNumberNerez         = psp1view.PassNumberNerez ;
                PassportsMapper.psp1map.PassDepartmentAddrNerez = psp1view.PassDepartmentAddrNerez ;
                PassportsMapper.psp1map.PassIssueDateNerez      = psp1view.PassIssueDateNerez ;
                                
                PassportsMapper.psp1map.SurnameZagr             = psp1view.SurnameZagr ;
                PassportsMapper.psp1map.NameZagr                = psp1view.NameZagr ;
                PassportsMapper.psp1map.PatronymicZagr          = psp1view.PatronymicZagr ;
                PassportsMapper.psp1map.PassRangeZagr           = psp1view.PassRangeZagr ;
                PassportsMapper.psp1map.PassNumberZagr          = psp1view.PassNumberZagr ;
                PassportsMapper.psp1map.PassDepartmentAddrZagr  = psp1view.PassDepartmentAddrZagr ;
                PassportsMapper.psp1map.PassIssueDateZagr       = psp1view.PassIssueDateZagr ;
                PassportsMapper.psp1map.BirthdayZagr            = psp1view.BirthdayZagr ;
                                
                PassportsMapper.psp1map.CancelReason            = psp1view.CancelReason ;
                                
                PassportsMapper.psp1map.CustomerPostCode        = psp1view.CustomerPostCode ;
                PassportsMapper.psp1map.CustomerCity            = psp1view.CustomerCity ;
                PassportsMapper.psp1map.CustomerAdressStreet    = psp1view.CustomerAdressStreet;
                PassportsMapper.psp1map.CustomerAdressHouse     = psp1view.CustomerAdressHouse ;
                PassportsMapper.psp1map.CustomerAdressCorps     = psp1view.CustomerAdressCorps;
                PassportsMapper.psp1map.CustomerAdressApartment = psp1view.CustomerAdressApartment;
*/



            //passp.Phone = "89898989";
            //return View("~/Home/CustomerFind");  // https://localhost:44306/Passport/List?Page=1
            SessPassport sp11 = HttpContext.Session.GetJson<SessPassport>("Passp");

            // Удаляем текущий контекст сессии
            HttpContext.Session.Remove("Passp");

            //SessPassport sp = HttpContext.Session.GetJson<SessPassport>("Passp");

            //sp.host = sp11.host;
            passp.Key = sp11.key;

            //Теперь меняем в passpViewList
            PassportViews.passpViewList.Remove(PassportViews.passpViewList.Find(passp1 => (passp1.Key == passp.Key && passp1.Customerid == passp.Customerid && passp1.Host == passp.Host)));
            //PassportViews.passpViewList.Add(passp);

            PassportsMapper.initialPasspList.Remove(PassportsMapper.initialPasspList.Find(passp1 => (passp1.Key == passp.Key && passp1.Customerid == passp.Customerid && passp1.Host == passp.Host)));

            //essPassport sp11 = HttpContext.Session.GetJson<SessPassport>("Passp");

            SessOrder so = HttpContext.Session.GetJson<SessOrder>("Order");

            return Redirect("~/Order/ShowOrderHost?"+"OrderNo="+so.ordid+"&"+"HostId="+so.host);  // https://localhost:44306/Passport/List?Page=1


            //return Redirect("~/Passport/List?Page=" + passp.PageNext.ToString() + "&customerid=" + passp.Customerid + "&host=" + passp.Host + "&key=" + passp.Key);

            //}

        }

    }

    
}

