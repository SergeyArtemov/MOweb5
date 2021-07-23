using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace MOweb.Models.ViewModels
{
    public class OrderViews
    {
        public static List<OrderView> ordViewList = new List<OrderView>() { };

        public class OrderView : Orders.Order
        {
            public long key { get; set; }
            public long user { get; set; }
            public long? AccountId { get; set; }
            public OrderView(Orders.Order ord) //, OrderView ovv) //: base(customerID, host)
            {

                //OrderView ov;

                //if (ovv == null)
                //    ov = new OrderView();
                //else ov = ovv;

                if (ord != null)
                {

                    this.Logo = ord.Logo;
                    this.OrderNo = ord.OrderNo;
                    this.Host = ord.Host;
                    this.OrderType = ord.OrderType;
                    this.OutLine = ord.OutLine;

                    this.State = ord.State;
                    this.StateDescr = ord.StateDescr;
                    this.OrderDate = ord.OrderDate;
                    this.EstimatedDeliveryDate = ord.EstimatedDeliveryDate;
                    this.OrderAmount = ord.OrderAmount;
                    this.CashlessAmount = ord.CashlessAmount;
                    this.Campaign = ord.Campaign;
                    this.Driver = ord.Driver;
                    this.CarNum = ord.CarNum;
                    this.CountItems = ord.CountItems;

                    this.ListPriceAgent = ord.ListPriceAgent;
                    this.CancelReasons = ord.CancelReasons;
                    this.InDispatchReasons = ord.InDispatchReasons;
                    this.History = ord.History;
                    this.OpenWithParams = ord.OpenWithParams;
                    this.CurrentUser = ord.CurrentUser;

                    // Добавил 10.03.2020
                    this.Currency = ord.Currency;
                    this.PartialCancelReasons = ord.PartialCancelReasons;
                    this.RobotComment = ord.RobotComment;
                    this.CustomerComment = ord.CustomerComment;
                    this.EmplComment = ord.EmplComment;
                    this.ForOperComment = ord.ForOperComment;

                    this.PassDataCorrect = ord.PassDataCorrect;
                    this.NeedCheckPassData = ord.NeedCheckPassData;
                    this.OrderList = ord.OrderList;
                    this.OrderForm = ord.OrderForm;

                    this.Customer = new Orders.CustomerInfo();
                    this.Customer.Email = ord.Customer.Email;
                    this.Customer.ID = ord.Customer.ID;
                    this.Customer.Name = ord.Customer.Name;
                    this.Customer.Phone = ord.Customer.Phone;
                    this.Customer.Surname = ord.Customer.Surname;


                    this.Payment = new Orders.PaymentInfo();
                    this.Payment.Description = ord.Payment.Description;
                    this.Payment.PaymentType = ord.Payment.PaymentType;


                    this.Shipping = new Orders.ShippingInfo();
                    this.Shipping.ShippingAmount = ord.Shipping.ShippingAmount;
                    this.Shipping.ShippingDateFrom = ord.Shipping.ShippingDateFrom;
                    this.Shipping.ShippingDateTo = ord.Shipping.ShippingDateTo;
                    this.Shipping.ShippingService = ord.Shipping.ShippingService;
                    this.Shipping.Description = ord.Shipping.Description;


                    this.Delivery = new Orders.DeliveryInfo();
                    this.Delivery.DeliveryAddrDelivId = ord.Delivery.DeliveryAddrDelivId;
                    this.Delivery.DeliveryAddrId = ord.Delivery.DeliveryAddrId;
                    this.Delivery.DeliveryAddrRouteId = ord.Delivery.DeliveryAddrRouteId;
                    this.Delivery.DeliveryApartment = ord.Delivery.DeliveryApartment;
                    this.Delivery.DeliveryCity = ord.Delivery.DeliveryCity;
                    this.Delivery.DeliveryCorps = ord.Delivery.DeliveryCorps;
                    this.Delivery.DeliveryEntrance = ord.Delivery.DeliveryEntrance;
                    this.Delivery.DeliveryFloor = ord.Delivery.DeliveryFloor;
                    this.Delivery.DeliveryHouse = ord.Delivery.DeliveryHouse;
                    this.Delivery.DeliveryHub = ord.Delivery.DeliveryHub;
                    this.Delivery.DeliveryId = ord.Delivery.DeliveryId;
                    this.Delivery.DeliveryIntercom = ord.Delivery.DeliveryIntercom;
                    this.Delivery.DeliveryIsOffice = ord.Delivery.DeliveryIsOffice;
                    this.Delivery.DeliveryKLADR = ord.Delivery.DeliveryKLADR;

                    this.Delivery.DeliveryName = ord.Delivery.DeliveryName;
                    this.Delivery.DeliveryPhone = ord.Delivery.DeliveryPhone;
                    this.Delivery.DeliveryPostCode = ord.Delivery.DeliveryPostCode;
                    this.Delivery.DeliveryPunkt = ord.Delivery.DeliveryPunkt;
                    this.Delivery.DeliveryRaion = ord.Delivery.DeliveryRaion;
                    this.Delivery.DeliveryRegion = ord.Delivery.DeliveryRegion;
                    this.Delivery.DeliverySearchAddr = ord.Delivery.DeliverySearchAddr;

                    this.Delivery.DeliveryStreet = ord.Delivery.DeliveryStreet;
                    this.Delivery.DeliverySurname = ord.Delivery.DeliverySurname;



                    this.OrderItems = new List<Orders.ItemInfo>();
                    //op.OrderItems = new List<ItemInfo>();
                    foreach (var item in ord.OrderItems) OrderItems.Add(item);
                }


                //key = rnd.Next();// this.Order();// base(orderno, host);
                //this.FillPassp();
            }

            public OrderView() //: base()
            {
                //key = rnd.Next();// this.Order();// base(orderno, host);
                //this.FillPassp();
            }


            public static OrderView findOrd(int? orderno, int? host, long key)
            {

                //if (order.Payment.PaymentType == OrdersMapper.initialOrderList.Find(ord1 => ord1.refOrd == order).Payment.PaymentType)

                return ordViewList.Find(ord1 => (ord1.OrderNo == orderno) && (ord1.Host == host) && (ord1.key == key)); //.initialPasspList.//.initialOrderList

                //return //passpViewList[0];
            }

            public Orders.Order getCloneOrd() //, OrderView ovv) //: base(customerID, host)
            {

                Orders.Order ord = new Orders.Order();
                //OrderView ov;

                //if (ovv == null)
                //    ov = new OrderView();
                //else ov = ovv;


                ord.Logo = this.Logo;
                ord.OrderNo = this.OrderNo;
                ord.Host = this.Host;
                ord.OrderType = this.OrderType;
                ord.OutLine = this.OutLine;

                ord.State = this.State;
                ord.StateDescr = this.StateDescr;
                ord.OrderDate = this.OrderDate;
                ord.EstimatedDeliveryDate = this.EstimatedDeliveryDate;
                ord.OrderAmount = this.OrderAmount;
                ord.CashlessAmount = this.CashlessAmount;
                ord.Campaign = this.Campaign;
                ord.Driver = this.Driver;
                ord.CarNum = this.CarNum;
                ord.CountItems = this.CountItems;

                ord.ListPriceAgent = this.ListPriceAgent;
                ord.CancelReasons = this.CancelReasons;
                ord.InDispatchReasons = this.InDispatchReasons;
                ord.History = this.History;
                ord.OpenWithParams = this.OpenWithParams;
                ord.CurrentUser = this.CurrentUser;


                // Добавил 10.03.2020
                ord.Currency = this.Currency;
                ord.PartialCancelReasons = this.PartialCancelReasons;
                ord.RobotComment = this.RobotComment;
                ord.CustomerComment = this.CustomerComment;
                ord.EmplComment = this.EmplComment;
                ord.ForOperComment = this.ForOperComment;

                ord.PassDataCorrect = this.PassDataCorrect;
                ord.NeedCheckPassData = this.NeedCheckPassData;
                ord.OrderList = this.OrderList;
                ord.OrderForm = this.OrderForm;

                ord.Customer = new Orders.CustomerInfo();
                ord.Customer.Email = this.Customer.Email;
                ord.Customer.ID = this.Customer.ID;
                ord.Customer.Name = this.Customer.Name;
                ord.Customer.Phone = this.Customer.Phone;
                ord.Customer.Surname = this.Customer.Surname;


                ord.Payment = new Orders.PaymentInfo();
                ord.Payment.Description = this.Payment.Description;
                ord.Payment.PaymentType = this.Payment.PaymentType;


                ord.Shipping = new Orders.ShippingInfo();
                ord.Shipping.ShippingAmount = this.Shipping.ShippingAmount;
                ord.Shipping.ShippingDateFrom = this.Shipping.ShippingDateFrom;
                ord.Shipping.ShippingDateTo = this.Shipping.ShippingDateTo;
                ord.Shipping.ShippingService = this.Shipping.ShippingService;
                ord.Shipping.Description = this.Shipping.Description;


                ord.Delivery = new Orders.DeliveryInfo();
                ord.Delivery.DeliveryAddrDelivId = this.Delivery.DeliveryAddrDelivId;
                ord.Delivery.DeliveryAddrId = this.Delivery.DeliveryAddrId;
                ord.Delivery.DeliveryAddrRouteId = this.Delivery.DeliveryAddrRouteId;
                ord.Delivery.DeliveryApartment = this.Delivery.DeliveryApartment;
                ord.Delivery.DeliveryCity = this.Delivery.DeliveryCity;
                ord.Delivery.DeliveryCorps = this.Delivery.DeliveryCorps;
                ord.Delivery.DeliveryEntrance = this.Delivery.DeliveryEntrance;
                ord.Delivery.DeliveryFloor = this.Delivery.DeliveryFloor;
                ord.Delivery.DeliveryHouse = this.Delivery.DeliveryHouse;
                ord.Delivery.DeliveryHub = this.Delivery.DeliveryHub;
                ord.Delivery.DeliveryId = this.Delivery.DeliveryId;
                ord.Delivery.DeliveryIntercom = this.Delivery.DeliveryIntercom;
                ord.Delivery.DeliveryIsOffice = this.Delivery.DeliveryIsOffice;
                ord.Delivery.DeliveryKLADR = this.Delivery.DeliveryKLADR;

                ord.Delivery.DeliveryName = this.Delivery.DeliveryName;
                ord.Delivery.DeliveryPhone = this.Delivery.DeliveryPhone;
                ord.Delivery.DeliveryPostCode = this.Delivery.DeliveryPostCode;
                ord.Delivery.DeliveryPunkt = this.Delivery.DeliveryPunkt;
                ord.Delivery.DeliveryRaion = this.Delivery.DeliveryRaion;
                ord.Delivery.DeliveryRegion = this.Delivery.DeliveryRegion;
                ord.Delivery.DeliverySearchAddr = this.Delivery.DeliverySearchAddr;

                ord.Delivery.DeliveryStreet = this.Delivery.DeliveryStreet;
                ord.Delivery.DeliverySurname = this.Delivery.DeliverySurname;


                ord.OrderItems = new List<Orders.ItemInfo>();
                foreach (var item in this.OrderItems) ord.OrderItems.Add(item);

                return ord;
            }

            public static string checkDeviationOrd(int orderno, int host, int key) //, OrderView ovv) //: base(customerID, host)
            {

                //return ordViewList.Find(ord1 => (ord1.OrderNo == orderno) && (ord1.Host == host) && (ord1.key == key));

                //Orders.Order ord = new Orders.Order();
                //OrderView ov;

                //if (ovv == null)
                //    ov = new OrderView();
                //else ov = ovv;
                string str = "";
                string nw = "<br>";//Environment.NewLine;

                OrderMap OM = OrdersMapper.initialOrderList.Find(ord1 => (ord1.OrderNo == orderno) && (ord1.Host == host) && (ord1.Key == key));//.initialOrderList(ord1 => (ord1.OrderNo == orderno) && (ord1.Host == host) && (ord1.key == key));
                OrderView OV = ordViewList.Find(ord1 => (ord1.OrderNo == orderno) && (ord1.Host == host) && (ord1.key == key));

                //Order ordMapper = ordViewList(ord1 => (ord1.OrderNo == orderno) && (ord1.Host == host) && (ord1.key == key));

                //initialOrderList(ord1 => (ord1.OrderNo == orderno) && (ord1.Host == host) && (ord1.key == key));


                if (OM.Customer.Email == null) OM.Customer.Email = "";
                if (OM.Customer.Name == null) OM.Customer.Name = "";
                if (OM.Customer.Surname == null) OM.Customer.Surname = "";
                if (OM.Customer.Phone == null) OM.Customer.Phone = "";
                if (OM.Payment.Description == null) OM.Payment.Description = "";
                if (OM.Delivery.DeliveryApartment == null) OM.Delivery.DeliveryApartment = "";
                if (OM.Delivery.DeliveryCity == null || OM.Delivery.DeliveryCity.Contains("Москва")) OM.Delivery.DeliveryCity = "";
                if (OM.Delivery.DeliveryCorps == null) OM.Delivery.DeliveryCorps = "";
                if (OM.Delivery.DeliveryEntrance == null) OM.Delivery.DeliveryEntrance = "";
                if (OM.Delivery.DeliveryFloor == null) OM.Delivery.DeliveryFloor = "";
                if (OM.Delivery.DeliveryHouse == null) OM.Delivery.DeliveryHouse = "";
                if (OM.Delivery.DeliveryHub == null) OM.Delivery.DeliveryHub = "";
                if (OM.Delivery.DeliveryIntercom == null) OM.Delivery.DeliveryIntercom = "";
                if (OM.Delivery.DeliveryKLADR == null) OM.Delivery.DeliveryKLADR = "";
                if (OM.Delivery.DeliveryName == null) OM.Delivery.DeliveryName = "";
                if (OM.Delivery.DeliveryPhone == null) OM.Delivery.DeliveryPhone = "";
                if (OM.Delivery.DeliveryPostCode == null) OM.Delivery.DeliveryPostCode = "";
                if (OM.Delivery.DeliveryPunkt == null) OM.Delivery.DeliveryPunkt = "";
                if (OM.Delivery.DeliveryRaion == null) OM.Delivery.DeliveryRaion = "";
                if (OM.Delivery.DeliveryRegion == null) OM.Delivery.DeliveryRegion = "";
                if (OM.Delivery.DeliveryStreet == null) OM.Delivery.DeliveryStreet = "";
                if (OM.Delivery.DeliverySurname == null) OM.Delivery.DeliverySurname = "";

                if (OV.Customer.Email == null) OV.Customer.Email = "";
                if (OV.Customer.Name == null) OV.Customer.Name = "";
                if (OV.Customer.Surname == null) OV.Customer.Surname = "";
                if (OV.Customer.Phone == null) OV.Customer.Phone = "";
                if (OV.Payment.Description == null) OV.Payment.Description = "";
                if (OV.Delivery.DeliveryApartment == null) OV.Delivery.DeliveryApartment = "";
                if (OV.Delivery.DeliveryCity == null || OV.Delivery.DeliveryCity.Contains("Москва")) OV.Delivery.DeliveryCity = "";
                if (OV.Delivery.DeliveryCorps == null) OV.Delivery.DeliveryCorps = "";
                if (OV.Delivery.DeliveryEntrance == null) OV.Delivery.DeliveryEntrance = "";
                if (OV.Delivery.DeliveryFloor == null) OV.Delivery.DeliveryFloor = "";
                if (OV.Delivery.DeliveryHouse == null) OV.Delivery.DeliveryHouse = "";
                if (OV.Delivery.DeliveryHub == null) OV.Delivery.DeliveryHub = "";
                if (OV.Delivery.DeliveryIntercom == null) OV.Delivery.DeliveryIntercom = "";
                if (OV.Delivery.DeliveryKLADR == null) OV.Delivery.DeliveryKLADR = "";
                if (OV.Delivery.DeliveryName == null) OV.Delivery.DeliveryName = "";
                if (OV.Delivery.DeliveryPhone == null) OV.Delivery.DeliveryPhone = "";
                if (OV.Delivery.DeliveryPostCode == null) OV.Delivery.DeliveryPostCode = "";
                if (OV.Delivery.DeliveryPunkt == null) OV.Delivery.DeliveryPunkt = "";
                if (OV.Delivery.DeliveryRaion == null) OV.Delivery.DeliveryRaion = "";
                if (OV.Delivery.DeliveryRegion == null) OV.Delivery.DeliveryRegion = "";
                if (OV.Delivery.DeliveryStreet == null) OV.Delivery.DeliveryStreet = "";
                if (OV.Delivery.DeliverySurname == null) OV.Delivery.DeliverySurname = "";


                if (OM.State != OV.State) str = str + "СТАТУС ЗАКАЗА не удалось изменить!!!" + nw;
                if (OM.OrderAmount != OV.OrderAmount) str = str + "СТОИМОСТЬ ЗАКАЗА не удалось изменить!!!" + nw;
                if (OM.CashlessAmount != OV.CashlessAmount) str = str + "СУММУ БН оплаты не удалось изменить!!!" + nw;
                if (OM.Customer.Email != OV.Customer.Email) str = str + "EMAIL заказчика не удалось изменить!!!" + nw;
                if (OM.Customer.Name != OV.Customer.Name) str = str + "NAME заказчика не удалось изменить!!!" + nw;
                if (OM.Customer.Surname != OV.Customer.Surname) str = str + "SURNAME заказчика не удалось изменить!!!" + nw;
                if (OM.Customer.Phone != OV.Customer.Phone) str = str + "PHONE заказчика не удалось изменить!!!" + nw;
                if (OM.Payment.PaymentType != OV.Payment.PaymentType) str = str + "ТИП ОПЛАТЫ не удалось изменить!!!" + nw;
                if (OM.Payment.Description != OV.Payment.Description) str = str + "СПОСОБ ОПЛАТЫ не удалось изменить!!!" + nw;
                if (OM.Shipping.ShippingAmount != OV.Shipping.ShippingAmount) str = str + "СТОИМОСТЬ ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Shipping.ShippingDateFrom != OV.Shipping.ShippingDateFrom) str = str + "ИНТЕРВАЛ ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Shipping.ShippingDateTo != OV.Shipping.ShippingDateTo) str = str + "ИНТЕРВАЛ ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Shipping.ShippingService != OV.Shipping.ShippingService) str = str + "АГЕНТА ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryAddrDelivId != OV.Delivery.DeliveryAddrDelivId) str = str + "DeliveryAddrDelivId ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryAddrId != OV.Delivery.DeliveryAddrId) str = str + "DeliveryAddr ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryAddrRouteId != OV.Delivery.DeliveryAddrRouteId) str = str + "DeliveryAddrRoute ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryApartment != OV.Delivery.DeliveryApartment) str = str + "КВАРТИРУ ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryCity != OV.Delivery.DeliveryCity) str = str + "ГОРОД ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryCorps != OV.Delivery.DeliveryCorps) str = str + "КОРПУС ДОМА ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryEntrance != OV.Delivery.DeliveryEntrance) str = str + "ПОДЪЕЗД ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryFloor != OV.Delivery.DeliveryFloor) str = str + "ЭТАЖ ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryHouse != OV.Delivery.DeliveryHouse) str = str + "ДОМ ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryHub != OV.Delivery.DeliveryHub) str = str + "ХАБ ДОСТАВКИ не удалось изменить!!!" + nw;
                //if (OM.Delivery.DeliveryId              != OV.Delivery.DeliveryId          ) str = str + "DeliveryId ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryIntercom != OV.Delivery.DeliveryIntercom) str = str + "КОД ДОМОФОНА ДОСТАВКИ не удалось изменить!!!" + nw;
                //if (OM.Delivery.DeliveryIsOffice        != OV.Delivery.DeliveryIsOffice    ) str = str + "ПАРА ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryKLADR != OV.Delivery.DeliveryKLADR) str = str + "КЛАДР ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryName != OV.Delivery.DeliveryName) str = str + "ИМЯ для ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryPhone != OV.Delivery.DeliveryPhone) str = str + "ТЕЛЕФОН ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryPostCode != OV.Delivery.DeliveryPostCode) str = str + "ИНДЕКС ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryPunkt != OV.Delivery.DeliveryPunkt) str = str + "ПУНКТ ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryRaion != OV.Delivery.DeliveryRaion) str = str + "РАЙОН ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryRegion != OV.Delivery.DeliveryRegion) str = str + "РЕГИОН ДОСТАВКИ не удалось изменить!!!" + nw;
                //if (OM.Delivery.DeliverySearchAddr      != OV.Delivery.DeliverySearchAddr  ) str = str + "АДРЕС ПОИСКА ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliveryStreet != OV.Delivery.DeliveryStreet) str = str + "УЛИЦУ ДОСТАВКИ не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliverySurname != OV.Delivery.DeliverySurname) str = str + "ФАМИЛИЮ для ДОСТАВКИ не удалось изменить!!!" + nw;

                // Добавил 10.03.2020
                if (OM.Currency != OV.Currency) str = str + "ТИП ВАЛЮТЫ не удалось изменить!!!" + nw;
                if (OM.PartialCancelReasons != OV.PartialCancelReasons) str = str + "ПРИЧИНУ ОТМЕНЫ не удалось изменить!!!" + nw;
                if (OM.CustomerComment != OV.CustomerComment) str = str + "КОММЕНТАРИИ ОПЕРАТОРА не удалось изменить!!!" + nw;
                if (OM.EmplComment != OV.EmplComment) str = str + "КОММЕНТАРИИ ЗАКАЗЧИКА не удалось изменить!!!" + nw;
                if (OM.Delivery.DeliverySurname != OV.Delivery.DeliverySurname) str = str + "ФАМИЛИЮ для ДОСТАВКИ не удалось изменить!!!" + nw;

                return str;
            }


        }

        public static void ToDoServerLog()
        {
            // Логирование. Потом перенести в отдельную функцию.

            string strQuery = String.Format($"CREATE TABLE #moweb_Insert_LogOfLists"
                                             + $"(TypeOfList int"
                                             + $",ParamInt1 int"
                                             + $",ParamInt2 int"
                                             + $",ParamJson varchar(max)"
                                             + $",ParamXML xml"
                                             + $",ParamStr varchar(8000)"
                                             + $",ActionType int"
                                             + $",UserID int"
                                             + $",[User] varchar(100)"
                                             + $",HostPoint varchar(100));");

            string S;

            OrderViews.OrderView OVlog;

            foreach (OrderView ord in OrderViews.ordViewList)
            {
                // Создаем краткий объект для логирования.
                OVlog = new OrderViews.OrderView();
                OVlog.OrderNo = ord.OrderNo;
                OVlog.Host = ord.Host;

                S = "\'OrderNo=" + ord.OrderNo.ToString() + ";" + "Host=" + ord.Host.ToString() + "\'"; // scv.Serialize<OrderView>(OVlog);

                strQuery = strQuery + String.Format($"insert into #moweb_Insert_LogOfLists(TypeOfList,ParamInt1,ParamInt2,ParamJson,ParamXML,ParamStr,ActionType,UserID,[User],HostPoint)"
                                                    + $"select "
                                                    + $"1"
                                                    + $",{ord.OrderNo}"
                                                    + $",{ord.Host}"
                                                    + $",NULL"
                                                    + $",NULL"
                                                    + $",{S}"
                                                    + $",1"
                                                    + $",1"
                                                    + $",\'user1\'"
                                                    + $",\'host1\'"
                                                    + $";"
                                                    );

            };

            strQuery = strQuery + String.Format($"exec [dbo].[moweb_Insert_LogOfLists];");

            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand(strQuery, connection))
                {
                    var res = command1.ExecuteNonQuery();//.ExecuteReader();  РАСКОММЕНТИТЬ !!!

                }
                connection.Close();
            }
        }

        public static void UpdOrderInViewList(Orders.Order order, int key) 
            {
            OrderView OV = ordViewList.Find(ord1 => (ord1.OrderNo == order.OrderNo) && (ord1.Host == order.Host) && (ord1.key == (int) key));
            ordViewList.Remove(OV);

            OrderView OV2 = new OrderView(order);
            OV2.key = key;
            ordViewList.Add(OV2);

            // Логирование.
            ToDoServerLog();


        }
    }
}
