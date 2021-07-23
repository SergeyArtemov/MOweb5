using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//namespace MOweb.Models.Case
//{
//   public class CaseMapper
//   {
//   }
//}

using MOweb.Models.Cust2;


namespace MOweb.Models
{
    public static class CustomerMapper
    {
        public static List<CustMap> custMapperList = new List<CustMap>() { };

        //public static CustView custView;


        public static void addCustView(MOweb.Models.Cust2.Customer cust, int key)  // в рамках конкретного key (ключ рабочей сессии)
        {

            custMapperList.Add(new CustMap(cust, key));

            return;
        }

        public static CustMap findCustView(int customerid, int host, int key)  // Ищем в рамках конкретного key (ключ рабочей сессии)
        {

            return custMapperList.Find(listCust1 => ((listCust1.Customerid == customerid) && (listCust1.Host == host) && (listCust1.key == key)));

        }

        public static void removeCustView(int customerid, int host, int key)  // Ищем в рамках конкретного key (ключ рабочей сессии)
        {

            custMapperList.RemoveAll(listCust1 => ((listCust1.Customerid == customerid) && (listCust1.Host == host) && (listCust1.key == key)));
            return;

        }

        public static void updateCustView(MOweb.Models.Cust2.Customer cust, int key)  // Ищем в рамках конкретного key (ключ рабочей сессии)
        {

            removeCustView((int)cust.Customerid, (int)cust.Host, key);
            addCustView(cust, key);

            return;

        }
    }

        public class CustMap : MOweb.Models.Cust2.Customer
        {
            public int key { get; set; }
            public int AccountId { get; set; } = -1;
            public CustMap(MOweb.Models.Cust2.Customer cust, int key) //: base(customerID, host)
            {
                Customerid = cust.Customerid;
                Host = cust.Host;
                FirstName = cust.FirstName;
                LastName = cust.LastName;
                Surname = cust.Surname;
                Mobile = cust.Mobile;
                Phone = cust.Phone;
                Email = cust.Email;
                Changed = cust.Changed;
                FullName = cust.FullName;
                CallOCallMobile = cust.CallOCallMobile;
                CallOCallPhone = cust.CallOCallPhone;
                Region = cust.Region;
                PrefContact = cust.PrefContact;
                PromoCall = cust.PromoCall;
                PromoEmail = cust.PromoEmail;
                SpamDay = cust.SpamDay;
                SpamWeek = cust.SpamWeek;
                SpamSpecial = cust.SpamSpecial;
                SpamPartner = cust.SpamPartner;
                SpamSMS = cust.SpamSMS;
                UserName = cust.UserName;
                ValidEmailWeb = cust.ValidEmailWeb;
                ValidEmailCC = cust.ValidEmailCC;
                Profitability = cust.Profitability;
                OrderCount = cust.OrderCount;
                NewSegment = cust.NewSegment;
                deliveredOrdersSum = cust.deliveredOrdersSum;
                Upsell = cust.Upsell;
                Birthday = cust.Birthday;
                AutoShipping = cust.AutoShipping;
                FirstOrder = cust.FirstOrder;
                LastConfirmWorkId = cust.LastConfirmWorkId;
                LastShippingWorkId = cust.LastShippingWorkId;
                EmailForNotify = cust.EmailForNotify;
                LastShippedOrder = cust.LastShippedOrder;
                LastConfirmOrder = cust.LastConfirmOrder;
                Sex = cust.Sex;
                ConfirmDescription = cust.ConfirmDescription;
                IncomingDescription = cust.IncomingDescription;
                ShippingDescription = cust.ShippingDescription;
                EmailType = cust.EmailType;
                CallOCallIncomingPhone = cust.CallOCallIncomingPhone;
                DeliveryINN = cust.DeliveryINN;
                NotSendToRobot = cust.NotSendToRobot;
                NotSendToSDEK = cust.NotSendToSDEK;
                idAuto = cust.idAuto;
                this.key = key;



                //key = rnd.Next();// this.Order();// base(orderno, host);
                //this.FillPassp();
            }

            public CustMap() //: base()
            {
                //key = rnd.Next();// this.Order();// base(orderno, host);
                //this.FillPassp();
            }


        }
    
}


