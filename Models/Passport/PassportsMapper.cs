using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOweb.Models.ViewModels;
using MOweb.Infrastructure;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using Utils.Common;
using MOweb.Infrastructure;
using Newtonsoft.Json;

namespace MOweb.Models
{
    public static class PassportsMapper
    {
        public static List<PassportMap> initialPasspList = new List<PassportMap>() { };   // Список паспортов, полученных из БД для последующего отслеживания изменений по заказам.

        public static Passport psp1;

        public static PassportMap psp1map;

        //public int sessPasport;

        //public static PassportView psp1view;

        public static Random rnd = new Random();
        // public class OrderProcessRecord
        // {
        //     public OrderProcessRecord(int OrderNo, int Host, Object OrdList) { this.OrderNo = OrderNo; this.Host = Host; this.OrdList = OrdList; }
        //     private int OrderNo { get; set; }
        //     private int Host { get; set; }
        //     private Object OrdList;
        // }

        public class SessPassport // сессия по работе с паспортом 
        {
            private int orderNo;
            private int host;
            private int key;
        }  


        public class PassportMap : Passport
            {
            //public int key { get; set; }

            //public SessPassport { get; set; }  // сессия по паспорту

            public PassportMap(Passport psp) //: base(customerID, host)
            {
                Customerid              = psp.Customerid                     ;
                Host                    = psp.Host                           ;
                Author                  = psp.Author                         ;
                User                    = psp.User                           ;
                Surname                 = psp.Surname                        ;
                Name                    = psp.Name                           ;
                Patronimic              = psp.Patronimic                     ;
                PassRange               = psp.PassRange                      ;
                PassNumber              = psp.PassNumber                     ;
                PassIssueDate           = psp.PassIssueDate                  ;
                Birthday                = psp.Birthday                       ;
                PassDepartmentCode      = psp.PassDepartmentCode             ;
                PassDepartmentAddr      = psp.PassDepartmentAddr             ;
                PassRegisteredAddress   = psp.PassRegisteredAddress          ;
                CancelReason            = psp.CancelReason                   ;
                DocType                 = psp.DocType                        ;
                SurnameRF               = psp.SurnameRF                      ;
                NameRF                  = psp.NameRF                         ;
                PatronymicRF            = psp.PatronymicRF                   ;
                PassRangeRF             = psp.PassRangeRF                    ;
                PassNumberRF            = psp.PassNumberRF                   ;
                PassDepartmentAddrRF    = psp.PassDepartmentAddrRF           ;
                PassIssueDateRF         = psp.PassIssueDateRF                ;
                BirthdayRF              = psp.BirthdayRF                     ;
                SurnameNerez            = psp.SurnameNerez                   ;
                NameNerez               = psp.NameNerez                      ;
                PatronymicNerez         = psp.PatronymicNerez                ;
                PassRangeNerez          = psp.PassRangeNerez                 ;
                PassNumberNerez         = psp.PassNumberNerez                ;
                PassDepartmentAddrNerez = psp.PassDepartmentAddrNerez        ;
                PassIssueDateNerez      = psp.PassIssueDateNerez             ;
                BirthdayNerez           = psp.BirthdayNerez                  ;
                SurnameZagr             = psp.SurnameZagr                    ;
                NameZagr                = psp.NameZagr                       ;
                PatronymicZagr          = psp.PatronymicZagr                 ;
                PassRangeZagr           = psp.PassRangeZagr                  ;
                PassNumberZagr          = psp.PassNumberZagr                 ;
                PassDepartmentAddrZagr  = psp.PassDepartmentAddrZagr         ;
                PassIssueDateZagr       = psp.PassIssueDateZagr              ;
                BirthdayZagr            = psp.BirthdayZagr                   ;
                SurnameUd               = psp.SurnameUd                      ;
                NameUd                  = psp.NameUd                         ;
                PatronymicUd            = psp.PatronymicUd                   ;
                PassNumberUd            = psp.PassNumberUd                   ;
                PassDepartmentAddrUd    = psp.PassDepartmentAddrUd           ;
                PassIssueDateUd         = psp.PassIssueDateUd                ;
                BirthdayUd              = psp.BirthdayUd                     ;
                Inn                     = psp.Inn                            ;
                HomeAdress              = psp.HomeAdress                     ;
                LegalRF                 = psp.LegalRF                        ;
                Key                     = psp.Key                            ;
                

                //key = rnd.Next();// this.Order();// base(orderno, host);
                //this.FillPassp();
            }
                       
        }

        // Получить новые Паспорт с сервера на обработку
        public static Passport getServerPassp(int Customerid, int Host )
        {
            Passport psp1 = new Passport(Customerid, Host);         
            psp1.FillPassp();                                       // запросили ПД с сервера
            psp1.Key = rnd.Next();                                  // ключь идентификации ПД

            //HttpContext.Sessio//.Session.SetJson();

            psp1map = new PassportMap(psp1);            // создали копию ПД для отслеживания транзакционной работы
            //psp1map.Key = psp1.Key;

            PassportViews.PasspView pspview = new PassportViews.PasspView(psp1);  // создали копию ПД для отслеживания бизнес-сессий
            //pspview.Key = psp1map.Key;

            initialPasspList.Add(psp1map);              // Добавли ПД в список отслеживаемых ПД (для транзакционной работы) 

            PassportViews.passpViewList.Add(pspview);   // Добавли ПД в список отслеживаемых ПД (для отслеживания бизнес-сессий) 

            return psp1;
        }

        // Получить обновленные ПД с сервера
        public static Passport updServerPassp(int? Customerid, int? Host, int? key)
        {
            // Создали обновленный объект ПД c сервера
            Passport psp1 = new Passport(Customerid, Host);
            psp1.FillPassp();
            psp1.Key = key;
            

            // Удаляем старую копию из Mapper(а)
            initialPasspList.Remove(initialPasspList.Find(passp1 => (passp1.Key == key && passp1.Customerid == Customerid && passp1.Host == Host)));
            // Добавляем обновленные с сервера значения ПД в Mapper(е)
            initialPasspList.Add(new PassportMap(psp1));


            //PassportViews.PasspView pspview = new PassportViews.PasspView(psp1);  // создали копию ПД для отслеживания бизнес-сессий

            //initialPasspList.Add(psp1map);              // Добавли ПД в список отслеживаемых ПД (для транзакционной работы) 

           //PassportViews.passpViewList.Add(pspview);   // Добавли ПД в список отслеживаемых ПД (для отслеживания бизнес-сессий) 

            return psp1;
        }

        // Поиск Паспорта в Мэппере по ключу-сесии
        public static Passport findPassp(int? customerid, int? host, int? key)
        {
            // Ищем начальные ПД по ключу текущей сессии
            return initialPasspList.Find(passp1 => (passp1.Key == key && passp1.Customerid == customerid && passp1.Host == host)); //initialPasspList[0];
        }

        // Пытаемся сохранить ПД на сервере
        public static Passport savePassp(Passport passp)
        {

            //PassportMap passp2 = initialPasspList.Find(passp1 => (passp1.Key == passp.Key && passp1.Customerid == passp.Customerid && passp1.Host == passp.Host));
            //Ищем данный паспорт в списке в PasspModelView


            PassportViews.PasspView passp2  = PassportViews.passpViewList.Find(passp1 => (passp1.Key == passp.Key && passp1.Customerid == passp.Customerid && passp1.Host == passp.Host));

            //initialPasspList.Remove(passp2); // passp1 => passp1.Key == passp.Key);

            //passp2 = new PassportMap(passp);

            //initialPasspList.Add(passp2);

            //Проверяем, что изменилось в новой версии паспорта относительно того, что сохранено в Мэппере

            //string app_id = HttpContext.Session.GetJson<string>("AppId");

            if (String.IsNullOrEmpty(passp2.PassRangeRF)) passp2.PassRangeRF = passp2.PassRange;
            if (String.IsNullOrEmpty(passp2.PassNumberRF)) passp2.PassNumberRF = passp2.PassNumber;

            //  ВРЕМЕННАЯ ЗАГЛУШКА!!!!
            var strsql =
                "exec [dbo].[PD_InsertEXECUTE] "
                                                + scv.GetParamStr("@Author", "CallCenter", 1)
                                                + ",@User = '2'"
                                                + scv.GetParamInt("@Customer",  passp2.Customerid, 0) //Value//ToString()
                                                + scv.GetParamInt("@Host",      passp2.Host, 0)
                                                + scv.GetParamStr("@CancelReason", passp2.CancelReason, 0)
                                                + scv.GetParamStr("@Surname", passp2.Surname, 0)
                                                + scv.GetParamStr("@Name", passp2.Name, 0)
                                                + scv.GetParamStr("@Patronymic", passp2.Patronimic, 0)
                                                + scv.GetParamStr("@PassRange", passp2.PassRange, 0)
                                                + scv.GetParamStr("@PassNumber", passp2.PassNumber, 0)

                                                + scv.GetParamDate("@PassIssueDate", passp2.PassIssueDate.Value, 0)
                                                + scv.GetParamDate("@Birthday", passp2.Birthday.Value, 0)
                                                + scv.GetParamStr("@PassDepartmentCode", passp2.PassDepartmentCode, 0)
                                                + scv.GetParamStr("@PassDepartmentAddr", passp2.PassDepartmentAddr, 0)
                                                + scv.GetParamStr("@PassRegisteredAddress", passp2.PassRegisteredAddress, 0)

                                                  + scv.GetParamStr("@DocType", passp2.DocType, 0)
                                                 + scv.GetParamStr("@SurnameRF", passp2.SurnameRF, 0)
                                                 + scv.GetParamStr("@NameRF", passp2.NameRF, 0)
                                                 + scv.GetParamStr("@PatronymicRF", passp2.PatronymicRF, 0)
                                                 + scv.GetParamStr("@PassRangeRF", passp2.PassRangeRF, 0)
                                                 + scv.GetParamStr("@PassNumberRF", passp2.PassNumberRF, 0)
                                                 + scv.GetParamStr("@PassDepartmentAddrRF", passp2.PassDepartmentAddrRF, 0)
                                                 + scv.GetParamDate("@PassIssueDateRF", passp2.PassIssueDate, 0)    //@PassIssueDateRF!!!
                                                + scv.GetParamDate("@BirthdayRF", passp2.BirthdayRF, 0)
  
                                                + scv.GetParamStr("@SurnameNerez", passp2.SurnameNerez, 0)
                                                 + scv.GetParamStr("@NameNerez", passp2.NameNerez, 0)
                                                 + scv.GetParamStr("@PatronymicNerez", passp2.PatronymicNerez, 0)
                                                 + scv.GetParamStr("@PassRangeNerez", passp2.PassRangeNerez, 0)
                                                 + scv.GetParamStr("@PassNumberNerez", passp2.PassNumberNerez, 0)
                                                 + scv.GetParamStr("@PassDepartmentAddrNerez", passp2.PassDepartmentAddrNerez, 0)
                                                 + scv.GetParamDate("@PassIssueDateNerez", passp2.PassIssueDateNerez, 0)  //!!!!
                                                 + scv.GetParamDate("@BirthdayNerez", passp2.BirthdayNerez, 0)            //!!!!! 

                                                 + scv.GetParamStr("@SurnameZagr", passp2.SurnameZagr, 0)
                                                 + scv.GetParamStr("@NameZagr", passp2.NameZagr, 0)
                                                 + scv.GetParamStr("@PatronymicZagr", passp2.PatronymicZagr, 0)
                                                 + scv.GetParamStr("@PassRangeZagr", passp2.PassRangeZagr, 0)
                                                 + scv.GetParamStr("@PassNumberZagr", passp2.PassNumberZagr, 0)
                                                 + scv.GetParamStr("@PassDepartmentAddrZagr", passp2.PassDepartmentAddrZagr, 0)
                                                 + scv.GetParamDate("@PassIssueDateZagr", passp2.PassIssueDateZagr, 0)
                                                 + scv.GetParamDate("@BirthdayZagr", passp2.BirthdayZagr, 0)

                                                 + scv.GetParamStr("@SurnameUd", passp2.SurnameUd, 0)
                                                 + scv.GetParamStr("@NameUd", passp2.NameUd, 0)
                                                 + scv.GetParamStr("@PatronymicUd", passp2.PatronymicUd, 0)
                                                 + scv.GetParamStr("@PassNumberUd", passp2.PassNumberUd, 0)
                                                 + scv.GetParamStr("@PassDepartmentAddrUd", passp2.PassDepartmentAddrUd, 0)
                                                 + scv.GetParamDate("@PassIssueDateUd", passp2.PassIssueDateUd, 0)
                                                 + scv.GetParamDate("@BirthdayUd", passp2.BirthdayUd, 0)

                                                 + scv.GetParamStr("@DeliveryINN", passp2.Inn, 0)

;
             
            //Пытаемся сохранить

            SqlConnection sqlConnection = new SqlConnection(OrdersMapper.SqlConnectGate);

            SqlCommand command = new SqlCommand(strsql , sqlConnection);
            

            sqlConnection.Open();
            var res = command.ExecuteNonQuery();
            command.Dispose();
            sqlConnection.Close();
            

            // Читаем результат после сохранения
            Passport passp22 = updServerPassp(passp.Customerid, passp.Host, passp.Key);

            // Проставляем во viewmodel несохранившиеся поля


            return passp22;
        }



    }
}


