using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOweb.Models.Case;

namespace MOweb.Models.ViewModels
{
    public class CaseViews
    {
        public static List<CaseView> custViewList = new List<CaseView>() { };
    }
    public class CaseView : CaseClasses 
    {
           public int key { get; set; }
           public int AccountId { get; set; } = -1;
           public CaseView(CaseClasses case1)
           {
            this.Id                     =case1.Id;                  ;
            this.AuthorTxt              =case1.AuthorTxt            ;
            this.Author                 =case1.Author               ;
            this.User                   =case1.User                 ;
            this.CreateDate             =case1.CreateDate           ;
            this.Caption                =case1.Caption              ;
            this.OrderTypeFullName      =case1.OrderTypeFullName    ;
            this.OrderNo                =case1.OrderNo              ;
            this.Host                   =case1.Host                 ;
            this.Message                =case1.Message              ;
            this.WorkTime               =case1.WorkTime             ;
            this.CaseCustomerId         =case1.CaseCustomerId       ;
            this.CaseReasonId           =case1.CaseReasonId         ;
            this.CaseStateId            =case1.CaseStateId          ;
            this.CaseRecallId           =case1.CaseRecallId         ;
            this.ChangeDate             =case1.ChangeDate           ;
            this.Source                 =case1.Source               ;
            this.Ok                     =case1.Ok                   ;
            this.HowToResponse          =case1.HowToResponse        ;
            this.deleted                =case1.deleted              ;
            this.SendedAutoEMAil        =case1.SendedAutoEMAil      ;
            this.GuiltyDept             =case1.GuiltyDept           ;
            this.Reason                 =case1.Reason               ;
            this.CaseState              =case1.CaseState            ;
            this.StateDate              =case1.StateDate            ;
            this.dtable                 =case1.dtable               ;
            this.SourceName             =case1.SourceName           ;
            this.ReasonName1            =case1.ReasonName1          ;
            this.ReasonName2            =case1.ReasonName2          ;
            this.ReasonName3            =case1.ReasonName3          ;


           }

    }
}




/*
 amespace MOweb.Models.ViewModels
{
    public static class CustomerViews
    {
        public static List<CustView> custViewList = new List<CustView>() { };

        //public static CustView custView;

        public static void addCustView(Customer cust, int key)  // в рамках конкретного key (ключ рабочей сессии)
        {

            custViewList.Add(new CustView(cust, key));

            return;
        }

        public static CustView findCustView(int customerid, int host, int key)  // Ищем в рамках конкретного key (ключ рабочей сессии)
        {

            return custViewList.Find(listCust1 => ((listCust1.Customerid == customerid) && (listCust1.Host == host) && (listCust1.key == key)));

        }

        public static void removeCustView(int customerid, int host, int key)  // Ищем в рамках конкретного key (ключ рабочей сессии)
        {

            custViewList.RemoveAll(listCust1 => ((listCust1.Customerid == customerid) && (listCust1.Host == host) && (listCust1.key == key)));
            return;

        }

        public static void updateCustView(Customer cust, int key)  // Ищем в рамках конкретного key (ключ рабочей сессии)
        {

            removeCustView((int)cust.Customerid, (int)cust.Host, key);
            addCustView(cust, key);

            return;

        }


        public class CustView : Customer
        {
            public int key { get; set; }
            public int AccountId { get; set; } = -1;
            public CustView(Customer cust, int key) //: base(customerID, host)
            {
                this.Customerid = cust.Customerid;
                this.Host = cust.Host;
                this.FirstName = cust.FirstName;
                this.LastName = cust.LastName;
                this.Surname = cust.Surname;
                this.Mobile = cust.Mobile;
                this.Phone = cust.Phone;
                this.Email = cust.Email;
                this.Changed = cust.Changed;
                this.FullName = cust.FullName;
                this.CallOCallMobile = cust.CallOCallMobile;
                this.CallOCallPhone = cust.CallOCallPhone;
                this.Region = cust.Region;
                this.PrefContact = cust.PrefContact;
                this.PromoCall = cust.PromoCall;
                this.PromoEmail = cust.PromoEmail;
                this.SpamDay = cust.SpamDay;
                this.SpamWeek = cust.SpamWeek;
                this.SpamSpecial = cust.SpamSpecial;
                this.SpamPartner = cust.SpamPartner;
                this.SpamSMS = cust.SpamSMS;
                this.UserName = cust.UserName;
                this.ValidEmailWeb = cust.ValidEmailWeb;
                this.ValidEmailCC = cust.ValidEmailCC;
                this.Profitability = cust.Profitability;
                this.OrderCount = cust.OrderCount;
                this.NewSegment = cust.NewSegment;
                this.deliveredOrdersSum = cust.deliveredOrdersSum;
                this.Upsell = cust.Upsell;
                this.Birthday = cust.Birthday;
                this.AutoShipping = cust.AutoShipping;
                this.FirstOrder = cust.FirstOrder;
                this.LastConfirmWorkId = cust.LastConfirmWorkId;
                this.LastShippingWorkId = cust.LastShippingWorkId;
                this.EmailForNotify = cust.EmailForNotify;
                this.LastShippedOrder = cust.LastShippedOrder;
                this.LastConfirmOrder = cust.LastConfirmOrder;
                this.Sex = cust.Sex;
                this.ConfirmDescription = cust.ConfirmDescription;
                this.IncomingDescription = cust.IncomingDescription;
                this.ShippingDescription = cust.ShippingDescription;
                this.EmailType = cust.EmailType;
                this.CallOCallIncomingPhone = cust.CallOCallIncomingPhone;
                this.DeliveryINN = cust.DeliveryINN;
                this.NotSendToRobot = cust.NotSendToRobot;
                this.NotSendToSDEK = cust.NotSendToSDEK;
                this.idAuto = cust.idAuto;
                this.key = key;



                //key = rnd.Next();// this.Order();// base(orderno, host);
                //this.FillPassp();
            }

            public CustView() //: base()
            {
                //key = rnd.Next();// this.Order();// base(orderno, host);
                //this.FillPassp();
            }


        }
    }
  
 */
