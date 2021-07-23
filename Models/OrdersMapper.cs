using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orders;
using System.Data.SqlClient;
using MOweb.Models.ViewModels;
using System.Net;

namespace MOweb.Models
{

    public class OrderProcessRecord
    {
        public OrderProcessRecord(int OrderNo, int Host, Object OrdList) { this.OrderNo = OrderNo; this.Host = Host; this.OrdList = OrdList; }
        private int OrderNo { get; set; }
        private int Host { get; set; }
        private Object OrdList;
    }

    public class OrderMap : Order
    {
        public Order refOrd { get; set; }
        public long Key { get; set; }
        public long user { get; set; }
        public long? AccountId { get; set; }
        public OrderMap(int orderno, int host, Order order) : base()
        {
            refOrd = order;// this.Order();// base(orderno, host);
            this.FillOrder(orderno, host, this);
        }
        public OrderMap(Order order, long Key, long user, long? AccountId)
        {
            //this.FillOrder(order.OrderNo, order.Host,this/*не нужен*/);
            OrdersMapper.getOrderMap(order, this);
            this.Key = Key;
            this.user = user;
            this.AccountId = AccountId;
        }

        public OrderMap()
        {

        }

    }



    public static class OrdersMapper
    {
        public class ordToUpdRealtime
        {
            public long OrderNo;
            public int Host;
            public long State;
            public string StateCode;
            public long id_processing;
            public ordToUpdRealtime(long OrderNo, int Host, long State, string StateCode, long id_processing)
            {
                this.OrderNo = OrderNo;
                this.Host = Host;
                this.State = State;
                this.StateCode = StateCode;
                this.id_processing = id_processing;
            }
        }


        public static Random rnd = new Random();

        public static List<OrderProcessRecord> OrderProcess = new List<OrderProcessRecord>() { };  // Список для коллективной обработки заказов

        public static OrderList OrdList2;

        public static List<OrderMap> initialOrderList = new List<OrderMap>() { };   // Список заказов, полученных из БД для последующего отслеживания изменений по заказам.

        //asa 27.07.2019
        private static string sqlConnectMO;
        private static string sqlConnectGate;


        public static string SqlConnectMO { get => sqlConnectMO; set => sqlConnectMO = value; }
        public static string SqlConnectGate { get => sqlConnectGate; set => sqlConnectGate = value; }
        //asa 27.07.2019

        // Метод - Запрос заказа в базе и добавление его в список отслеживаемых в Mapper(-e) заказов. Для последующего отслеживания изменений по этому заказу. 
        public static Order findOrder(int orderno, int host, long rnd, long user, long? AccountId)
        {

            Order ord;
            OrderViews.OrderView ov;

            // Создаем объект Order и наполняем его из БД
            ord = new Order(orderno, host);
            ord.FillOrder(ord.OrderNo, ord.Host, ord);

            // Создаём объект OrderMap и наполняем его из объекта ord
            OrderMap om = new OrderMap(ord, 0, 0, 0);//rnd, user, AccountId); //16.04.2021

            // Удаляем старый объект
            initialOrderList.RemoveAll(ord1 => ord1.OrderNo == om.OrderNo && ord1.Host == om.Host );  // 16.04.2021
            // Записываем этот OrderMap в initialOrderList.  (для транзакционной работы по заказам)
            initialOrderList.Add(om);

            // Создали объект OrderView на базе объекта OrderMap
            ov = new OrderViews.OrderView(om);
            ov.key = rnd;

            // Удаляем старый объект
            OrderViews.ordViewList.RemoveAll(ord1 => ord1.OrderNo == om.OrderNo && ord1.Host == om.Host);  // 16.04.2021
            // Добавли объект в список отслеживаемых объектов (для отслеживания бизнес-сессий по заказам)
            OrderViews.ordViewList.Add(ov);//PasspList.Add(psp1map);              

            //PassportViews.passpViewList.Add(pspview);                                

            ToDoServerLog();  // Логируем коллекции мепперов

            // Добавли ПД в список отслеживаемых ПД (для транзакционной работы) 
            //initialOrderList.Add(new OrderMap(orderno, host, ord)); // Потом надо сделать клонирование. Для быстроты сделали двойное обращение к базе. 

            return ord;
        }

        // Поиск Заказа в Мэппере по ключу-сесии
        public static Order findOrderLocal(int? orderid, int? host, long key)
        {
            // Ищем начальные Заказ по ключу текущей сессиию 
            OrderMap om;
            om = initialOrderList.Find(ord1 => (ord1.Key == key && ord1.OrderNo == orderid && ord1.Host == host));

            return OrdersMapper.getOrder(om);
        }

        public static OrderMap getOrderMap(Order ord, OrderMap opp)
        {

            OrderMap op;


            if (opp == null)
                op = new OrderMap();
            else op = opp;

            if (ord != null)
            {
                op.Logo = ord.Logo;
                op.OrderNo = ord.OrderNo;
                op.Host = ord.Host;
                op.OrderType = ord.OrderType;
                op.OutLine = ord.OutLine;
                op.State = ord.State;
                op.StateDescr = ord.StateDescr;
                op.OrderDate = ord.OrderDate;
                op.EstimatedDeliveryDate = ord.EstimatedDeliveryDate;
                op.OrderAmount = ord.OrderAmount;
                op.CashlessAmount = ord.CashlessAmount;
                op.Campaign = ord.Campaign;
                op.Driver = ord.Driver;
                op.CarNum = ord.CarNum;
                op.CountItems = ord.CountItems;

                op.ListPriceAgent = ord.ListPriceAgent;
                op.CancelReasons = ord.CancelReasons;
                op.InDispatchReasons = ord.InDispatchReasons;
                op.History = ord.History;
                op.OpenWithParams = ord.OpenWithParams;
                op.CurrentUser = ord.CurrentUser;

                // Добавил 10.03.2020
                op.Currency = ord.Currency;
                op.PartialCancelReasons = ord.PartialCancelReasons;
                op.RobotComment = ord.RobotComment;
                op.CustomerComment = ord.CustomerComment;
                op.EmplComment = ord.EmplComment;
                op.ForOperComment = ord.ForOperComment;


                op.PassDataCorrect = ord.PassDataCorrect;
                op.NeedCheckPassData = ord.NeedCheckPassData;
                op.OrderList = ord.OrderList;
                op.OrderForm = ord.OrderForm;

                //CurrentUser = 0;
                //Logo = "";
                //OrderNo = 0;
                //Host = 0;
                ////Currency = "";   НЕТ
                //OrderType = 0;
                //OutLine = 0;
                //State = 0;
                //StateDescr = "";
                //OrderDate = "";
                //EstimatedDeliveryDate = "";
                //OrderAmount = 0;
                //CashlessAmount = 0;
                //Campaign = "";
                //Driver = "";
                //CarNum = "";
                //CountItems = 0;
                //ListPriceAgent = "";
                //CancelReasons = "";
                //InDispatchReasons = "";
                ////PartialCancelReasons = "";  НЕТ
                //History = "";
                //OpenWithParams = "";
                ////RobotComment = "";
                ////CustomerComment = "";
                ////EmplComment = "";
                ////ForOperComment = "";
                //Customer = new CustomerInfo();
                //Payment = new PaymentInfo();
                //Shipping = new ShippingInfo();
                //Delivery = new DeliveryInfo();
                //OrderItems = new List<ItemInfo> { };// { new ItemInfo() };

                op.Customer = new CustomerInfo();
                op.Customer.Email = ord.Customer.Email;
                op.Customer.ID = ord.Customer.ID;
                op.Customer.Name = ord.Customer.Name;
                op.Customer.Phone = ord.Customer.Phone;
                op.Customer.Surname = ord.Customer.Surname;


                op.Payment = new PaymentInfo();
                op.Payment.Description = ord.Payment.Description;
                op.Payment.PaymentType = ord.Payment.PaymentType;


                op.Shipping = new ShippingInfo();
                op.Shipping.ShippingAmount = ord.Shipping.ShippingAmount;
                op.Shipping.ShippingDateFrom = ord.Shipping.ShippingDateFrom;
                op.Shipping.ShippingDateTo = ord.Shipping.ShippingDateTo;
                op.Shipping.ShippingService = ord.Shipping.ShippingService;
                op.Shipping.Description = ord.Shipping.Description;


                op.Delivery = new DeliveryInfo();
                op.Delivery.DeliveryAddrDelivId = ord.Delivery.DeliveryAddrDelivId;
                op.Delivery.DeliveryAddrId = ord.Delivery.DeliveryAddrId;
                op.Delivery.DeliveryAddrRouteId = ord.Delivery.DeliveryAddrRouteId;
                op.Delivery.DeliveryApartment = ord.Delivery.DeliveryApartment;
                op.Delivery.DeliveryCity = ord.Delivery.DeliveryCity;
                op.Delivery.DeliveryCorps = ord.Delivery.DeliveryCorps;
                op.Delivery.DeliveryEntrance = ord.Delivery.DeliveryEntrance;
                op.Delivery.DeliveryFloor = ord.Delivery.DeliveryFloor;
                op.Delivery.DeliveryHouse = ord.Delivery.DeliveryHouse;
                op.Delivery.DeliveryHub = ord.Delivery.DeliveryHub;
                op.Delivery.DeliveryId = ord.Delivery.DeliveryId;
                op.Delivery.DeliveryIntercom = ord.Delivery.DeliveryIntercom;
                op.Delivery.DeliveryIsOffice = ord.Delivery.DeliveryIsOffice;
                op.Delivery.DeliveryKLADR = ord.Delivery.DeliveryKLADR;

                op.Delivery.DeliveryName = ord.Delivery.DeliveryName;
                op.Delivery.DeliveryPhone = ord.Delivery.DeliveryPhone;
                op.Delivery.DeliveryPostCode = ord.Delivery.DeliveryPostCode;
                op.Delivery.DeliveryPunkt = ord.Delivery.DeliveryPunkt;
                op.Delivery.DeliveryRaion = ord.Delivery.DeliveryRaion;
                op.Delivery.DeliveryRegion = ord.Delivery.DeliveryRegion;
                op.Delivery.DeliverySearchAddr = ord.Delivery.DeliverySearchAddr;

                op.Delivery.DeliveryStreet = ord.Delivery.DeliveryStreet;
                op.Delivery.DeliverySurname = ord.Delivery.DeliverySurname;

                op.Delivery.DeliveryStreet = ord.Delivery.DeliveryStreet;
                op.Delivery.DeliverySurname = ord.Delivery.DeliverySurname;



                op.OrderItems = new List<ItemInfo>();
                foreach (var item in ord.OrderItems) op.OrderItems.Add(item);
            }
            else op = null;

            return op;

        }

        public static Order getOrder(OrderMap op)
        {

            Order ord = new Order();

            if (op != null)
            {
                ord.Logo = op.Logo;
                ord.OrderNo = op.OrderNo;
                ord.Host = op.Host;
                ord.OrderType = op.OrderType;
                ord.OutLine = op.OutLine;

                ord.State = op.State;
                ord.StateDescr = op.StateDescr;
                ord.OrderDate = op.OrderDate;
                ord.EstimatedDeliveryDate = op.EstimatedDeliveryDate;
                ord.OrderAmount = op.OrderAmount;
                ord.CashlessAmount = op.CashlessAmount;
                ord.Campaign = op.Campaign;
                ord.Driver = op.Driver;
                ord.CarNum = op.CarNum;
                ord.CountItems = op.CountItems;

                ord.Customer = new CustomerInfo();
                ord.Customer.Email = op.Customer.Email;
                ord.Customer.ID = op.Customer.ID;
                ord.Customer.Name = op.Customer.Name;
                ord.Customer.Phone = op.Customer.Phone;
                ord.Customer.Surname = op.Customer.Surname;

                ord.ListPriceAgent = op.ListPriceAgent;
                ord.CancelReasons = op.CancelReasons;
                ord.InDispatchReasons = op.InDispatchReasons;
                ord.History = op.History;
                ord.OpenWithParams = op.OpenWithParams;
                ord.CurrentUser = op.CurrentUser;

                // Добавил 10.03.2020
                ord.Currency = op.Currency;
                ord.PartialCancelReasons = op.PartialCancelReasons;
                ord.RobotComment = op.RobotComment;
                ord.CustomerComment = op.CustomerComment;
                ord.EmplComment = op.EmplComment;
                ord.ForOperComment = op.ForOperComment;


                ord.PassDataCorrect = op.PassDataCorrect;
                ord.NeedCheckPassData = op.NeedCheckPassData;
                ord.OrderList = op.OrderList;
                ord.OrderForm = op.OrderForm;


                ord.Payment = new PaymentInfo();
                ord.Payment.Description = op.Payment.Description;
                ord.Payment.PaymentType = op.Payment.PaymentType;


                ord.Shipping = new ShippingInfo();
                ord.Shipping.ShippingAmount = op.Shipping.ShippingAmount;
                ord.Shipping.ShippingDateFrom = op.Shipping.ShippingDateFrom;
                ord.Shipping.ShippingDateTo = op.Shipping.ShippingDateTo;
                ord.Shipping.ShippingService = op.Shipping.ShippingService;
                ord.Shipping.Description = op.Shipping.Description;


                ord.Delivery = new DeliveryInfo();
                ord.Delivery.DeliveryAddrDelivId = op.Delivery.DeliveryAddrDelivId;
                ord.Delivery.DeliveryAddrId = op.Delivery.DeliveryAddrId;
                ord.Delivery.DeliveryAddrRouteId = op.Delivery.DeliveryAddrRouteId;
                ord.Delivery.DeliveryApartment = op.Delivery.DeliveryApartment;
                ord.Delivery.DeliveryCity = op.Delivery.DeliveryCity;
                ord.Delivery.DeliveryCorps = op.Delivery.DeliveryCorps;
                ord.Delivery.DeliveryEntrance = op.Delivery.DeliveryEntrance;
                ord.Delivery.DeliveryFloor = op.Delivery.DeliveryFloor;
                ord.Delivery.DeliveryHouse = op.Delivery.DeliveryHouse;
                ord.Delivery.DeliveryHub = op.Delivery.DeliveryHub;
                ord.Delivery.DeliveryId = op.Delivery.DeliveryId;
                ord.Delivery.DeliveryIntercom = op.Delivery.DeliveryIntercom;
                ord.Delivery.DeliveryIsOffice = op.Delivery.DeliveryIsOffice;
                ord.Delivery.DeliveryKLADR = op.Delivery.DeliveryKLADR;

                ord.Delivery.DeliveryName = op.Delivery.DeliveryName;
                ord.Delivery.DeliveryPhone = op.Delivery.DeliveryPhone;
                ord.Delivery.DeliveryPostCode = op.Delivery.DeliveryPostCode;
                ord.Delivery.DeliveryPunkt = op.Delivery.DeliveryPunkt;
                ord.Delivery.DeliveryRaion = op.Delivery.DeliveryRaion;
                ord.Delivery.DeliveryRegion = op.Delivery.DeliveryRegion;
                ord.Delivery.DeliverySearchAddr = op.Delivery.DeliverySearchAddr;

                ord.Delivery.DeliveryStreet = op.Delivery.DeliveryStreet;
                ord.Delivery.DeliverySurname = op.Delivery.DeliverySurname;

                ord.Delivery.DeliveryStreet = op.Delivery.DeliveryStreet;
                ord.Delivery.DeliverySurname = op.Delivery.DeliverySurname;


                ord.OrderItems = new List<ItemInfo>();
                foreach (var item in op.OrderItems) ord.OrderItems.Add(item);
            }
            else ord = null;

            return ord;

            //return null;
        }



        // Метод - Обновления данных по заказу
        // Вх.: Заказ после изменения через web-интерфейс
        // Вых.: Этот же заказ, но после попытки сохранения в базе, и после считывания фактичекого результата из базы.
        public static Order updOrder(Order order, int key, long user1, long? AccountId1)
        {
            var res = 0;
            //int key = 0;

            using (SqlConnection connection2 = new SqlConnection(OrdersMapper.SqlConnectMO))
            {

                SqlCommand command = new SqlCommand("", connection2);


                connection2.Open();


                // Изменение метода оплаты
                if (order.Payment.PaymentType == OrdersMapper.initialOrderList.Find(ord1 => ord1.OrderNo == order.OrderNo && ord1.Host == order.Host).Payment.PaymentType)
                {

                    command = new SqlCommand("exec [dbo].[cc_Order_CashChange] @Order = "
                                              + order.OrderNo
                                              + ", @Host =" + order.Host
                                              + ", @PaymentType =" + order.Payment.PaymentType
                                              + ", @Author = 3, @User = 2, @Reason = 1"
                                              , connection2);

                    res = command.ExecuteNonQuery();

                    command.Dispose();

                }


                // Изменение статуса
                if (order.State == OrdersMapper.initialOrderList.Find(ord1 => ord1.OrderNo == order.OrderNo && ord1.Host == order.Host).State)
                {
                    res = (new SqlCommand("exec [dbo].[cc_Order_Head_ChangeState] @Order = "
                                            + order.OrderNo
                                            + ", @Host =" + order.Host
                                            + ", @State =" + order.State
                                            + ", @Author = 3, @User = 2"
                                            , connection2)).ExecuteNonQuery();
                }


                // Изменение заказа
                //    res = (new SqlCommand("exec [dbo].[cc_Order_Head_ChangeState] @Order = "
                //                            + order.OrderNo
                //                            + ", @Host =" + order.Host
                //                            + ", @State =" + order.State
                //                            + ", @Author = 3, @User = 2, @Reason = 1"
                //                            , connection2)).ExecuteNonQuery();

                //connection2.Close();

                // Сохранение Delivery
                string strDeliveryKLADRRight = String.IsNullOrEmpty(order.Delivery.DeliveryKLADR) ? "" : order.Delivery.DeliveryKLADR;
                string strQuery = String.Format($"EXEC MO.[dbo].[cc_Delivery_Change] "
                                                + $"@Order = {order.OrderNo}, "
                                                + $"@Host = {order.Host}, "
                                                + $"@Customer = {order.Customer.ID}, "
                                                + $"@Name = '{order.Delivery.DeliveryName}', "
                                                + $"@Surname = '{order.Delivery.DeliverySurname}', "
                                                + $"@PostCode = '{order.Delivery.DeliveryPostCode}', "
                                                + $"@Region = '{order.Delivery.DeliveryRegion}', "
                                                + $"@Raion = '{order.Delivery.DeliveryRaion}', "
                                                + $"@City = '{order.Delivery.DeliveryCity}', "
                                                + $"@Punkt = '{order.Delivery.DeliveryPunkt}', "
                                                + $"@Street = '{order.Delivery.DeliveryStreet}', "
                                                + $"@House = '{order.Delivery.DeliveryHouse}', "
                                                + $"@Corps = '{order.Delivery.DeliveryCorps}', "
                                                + $"@Apartment = '{order.Delivery.DeliveryApartment}', "
                                                + $"@KLADR = '{order.Delivery.DeliveryKLADR}', "
                                                + $"@Phone = '{order.Delivery.DeliveryPhone}', "
                                                + $"@Author = 3, "
                                                + $"@User = {order.CurrentUser}, "
                                                + $"@AddrId = {order.Delivery.DeliveryAddrId}, "
                                                + $"@Entrance = '{order.Delivery.DeliveryEntrance}', "
                                                + $"@Intercom = '{order.Delivery.DeliveryIntercom}', "
                                                + $"@Floor = '{order.Delivery.DeliveryFloor}', "
                                                + $"@CompanyName = '', "
                                                + $"@ExtPhone = '', "
                                                + $"@NeedPass = 0, "
                                                + $"@IsOFfice = {((order.Delivery.DeliveryIsOffice == true) ? 1 : 0)}, "
                                                + $"@DeliveryKLADRRight = '{((strDeliveryKLADRRight.Length == 19) ? strDeliveryKLADRRight + "9999" : "")}', "
                                                + $"@idAddrRoute = {order.Delivery.DeliveryAddrRouteId}, "
                                                + $"@idAddrDeliv = {order.Delivery.DeliveryAddrDelivId}, "
                                                + $"@DeliveryId = {order.Delivery.DeliveryId}, "
                                                + $"@StringHub = '{order.Delivery.DeliveryHub}' "
                    );

                using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
                {
                    connection.Open();
                    using (SqlCommand command1 = new SqlCommand(strQuery, connection))
                    {
                        var reader = command1.ExecuteReader();  //ОТКЛЮЧИТЬ НА ВРЕМЯ ДЕМОНСТРАЦИИ!!! asa
                        reader.Close();  //ОТКЛЮЧИТЬ НА ВРЕМЯ ДЕМОНСТРАЦИИ!!! asa
                    }
                    connection.Close();
                }

                connection2.Close();
            }


            // Сохранение Shipping
            string strQueryShipping = String.Format($"EXEC MO.[dbo].[cc_Order_ShippingChange_OnlyAgent] "
                                                + $"@OrderNo              = {order.OrderNo}, "
                                                + $"@Host               = {order.Host}, "
                                                + $"@ShipAdent         = {order.Shipping.ShippingService}, "
                                                + $"@Author             = {user1}, "
                                                + $"@User               = 2 "
                    //+ $"@Region             = null, "   // !!!
                    //+ $"@AgentNO            = null, "  //!!!
                    //+ $"@Summ               = '{order.Shipping.ShippingAmount}', "
                    //+ $"@NeedReturn         = '{order.Delivery.DeliveryCity}', "  //!!!
                    //+ $"@ShippingDateFrom   = '{order.Shipping.ShippingDateFrom}', "
                    //+ $"@ShippingDateTo     = '{order.Shipping.ShippingDateTo}', "

                    );

            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand(strQueryShipping, connection))
                {
                    var reader = command1.ExecuteReader();  //ОТКЛЮЧИТЬ НА ВРЕМЯ ДЕМОНСТРАЦИИ!!! asa
                    reader.Close();  //ОТКЛЮЧИТЬ НА ВРЕМЯ ДЕМОНСТРАЦИИ!!! asa
                }
                connection.Close();
            }


            //key = (int)initialOrderList.Find(ord1 => ord1.OrderNo == order.OrderNo && ord1.Host == order.Host && ord1.Key == order.).Key;  //(ord1 => ord1.refOrd == order);
            long user = (int)initialOrderList.Find(ord1 => ord1.OrderNo == order.OrderNo && ord1.Host == order.Host && ord1.Key == key).user;
            long? AccountId = (int)initialOrderList.Find(ord1 => ord1.OrderNo == order.OrderNo && ord1.Host == order.Host && ord1.Key == key).AccountId;

            initialOrderList.RemoveAll(ord1 => ord1.OrderNo == order.OrderNo && ord1.Host == order.Host && ord1.Key == key);  //(ord1 => ord1.refOrd == order);

            Order ord;
            OrderViews.OrderView ov;

            // Создаем новый объект заказ и наполняем его из БД
            ord = new Order(order.OrderNo, order.Host);
            ord.FillOrder(ord.OrderNo, ord.Host, ord);

            // Создаём объект OrderMap
            OrderMap om = new OrderMap(ord, 0, 0, 0);//key/*ключ идентификации ПД*/, user, AccountId);  16.04.2021


            initialOrderList.Add(om);  // Записываем этот OrderMap в initialOrderList. Добавили ПД в список отслеживаемых ПД (для транзакционной работы)

            ToDoServerLog();

            ov = new OrderViews.OrderView(om);           // Создали копию заказа в OrderView
            ov.key = 0;//key;  16.04.2021
            ov.user = 0;//user; 16.04.2021
            ov.AccountId = 0;//AccountId; 16.04.2021



            //
            //OrderViews.ordViewList.Add(ov);//PasspList.Add(psp1map);                // Добавли ПД в список отслеживаемых ПД (для транзакционной работы) 

            //Order ord2;
            //ord2 = new Order();
            //ord2.FillOrder(order.OrderNo, order.Host, ord2);


            return ord;
        }


        public static OrderList OrderListCreate(string Str)
        {
            //OrderList OrdList;
            //OrdList = new OrderList();

            //OrdList.GetOrderList(1234,1,"");

            OrdList2 = new OrderList(1234, 1, "");

            //var OD OrderProcessRecord;

            var OD = new OrderProcessRecord(1, 1, null);  //rdersMapper.OrderProcessRecord 

            foreach (Order Order1 in OrdList2.ListOrders)
            {
                OD = new OrderProcessRecord(Order1.OrderNo, Order1.Host, (Object)OrdList2);

                //if ((order.OrderNo == item.OrderNo) & (order.Host == item.Host))
                //{
                OrderProcess.Add(OD);
                //order.OrderItems.Add(item);
                //}
            }

            return OrdList2; ;
        }

        public static void OrderListUpdate(string Str)
        {
            using (SqlConnection connection2 = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                string QueryOrders = "insert into [dbo].[testmult] select 1001,556";

                connection2.Open();
                using (SqlCommand command = new SqlCommand(QueryOrders, connection2))
                {

                    var res = command.ExecuteNonQuery();//("insert into [dbo].[testmult] select 1001,555",

                }
            }

        }

        public static void ToDoServerLog()
        {
            // Логирование. Потом перенести в отдельную функцию.

            string strQuery = String.Format($"CREATE TABLE #moweb_Insert_LogOfLists"
                                             + $"(TypeOfList bigint"
                                             + $",ParamInt1 bigint"
                                             + $",ParamInt2 bigint"
                                             + $",ParamInt3 bigint"
                                             + $",ParamInt4 bigint"
                                             + $",ParamJson varchar(max)"
                                             + $",ParamXML xml"
                                             + $",ParamStr varchar(8000)"
                                             + $",ActionType int"
                                             + $",UserID bigint"
                                             + $",[User] varchar(100)"
                                             + $",HostPoint varchar(100));");

            string S;

            OrderMap OMlog;

            foreach (OrderMap ord in OrdersMapper.initialOrderList) //.ordViewList)
            {
                // Создаем краткий объект для логирования.
                OMlog = new OrderMap(); //OrderViews.OrderView();
                OMlog.OrderNo = ord.OrderNo;
                OMlog.Host = ord.Host;

                S = "\'OrderNo=" + ord.OrderNo.ToString() + ";" + "Host=" + ord.Host.ToString() + "\'"; // scv.Serialize<OrderView>(OVlog);

                strQuery = strQuery + String.Format($"insert into #moweb_Insert_LogOfLists(TypeOfList,ParamInt1,ParamInt2,ParamInt3,ParamInt4,ParamJson,ParamXML,ParamStr,ActionType,UserID,[User],HostPoint)"
                                                    + $"select "
                                                    + $"2"
                                                    + $",{ord.OrderNo}"
                                                    + $",{ord.Host}"
                                                    + $",{ord.user}"
                                                    + $",{ord.Key}"
                                                    + $",NULL"
                                                    + $",NULL"
                                                    + $",{S}"
                                                    + $",1"
                                                    + $",1"
                                                    + $",\'user1\'"
                                                    + $",\'"+ Dns.GetHostName()+"\'"
                                                    + $";"
                                                    );

            };


            foreach (OrderViews.OrderView ord in OrderViews.ordViewList)
            {
                // Создаем краткий объект для логирования.
                OMlog = new OrderMap(); //OrderViews.OrderView();
                OMlog.OrderNo = ord.OrderNo;
                OMlog.Host = ord.Host;

                S = "\'OrderNo=" + ord.OrderNo.ToString() + ";" + "Host=" + ord.Host.ToString() + "\'"; // scv.Serialize<OrderView>(OVlog);

                strQuery = strQuery + String.Format($"insert into #moweb_Insert_LogOfLists(TypeOfList,ParamInt1,ParamInt2,ParamInt3,ParamInt4,ParamJson,ParamXML,ParamStr,ActionType,UserID,[User],HostPoint)"
                                                    + $"select "
                                                    + $"1"
                                                    + $",{ord.OrderNo}"
                                                    + $",{ord.Host}"
                                                    + $",{ord.user}"
                                                    + $",{ord.key}"
                                                    + $",NULL"
                                                    + $",NULL"
                                                    + $",{S}"
                                                    + $",1"
                                                    + $",1"
                                                    + $",\'user1\'"
                                                    + $",\'" + Dns.GetHostName() + "\'"
                                                    + $";"
                                                    );

            };

            strQuery = strQuery + String.Format($"exec [dbo].[moweb_Insert_LogOfLists];");

            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command1 = new SqlCommand(strQuery, connection))
                {
                    var res = command1.ExecuteNonQuery();//.ExecuteReader();

                }
                connection.Close();
            }
        }

        // Наполнение/обновление RealtimeCache2. Вызывается в параллельном потоке каждые неск.секунд
        public static void updRealtimeCache2()
        {
            List<ordToUpdRealtime> ords = new List<ordToUpdRealtime>();

            long OrderNo = 0;
            int Host = 0;
            long State = 0;
            string StateCode = "";
            long id_processing = 0;

            // Получение списка номера заказов из БД
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                int col = 0;
                connection.Open();
                string sqlQuery = "exec [dbo].[cc_realtime_Fill_cache2_Order] @cacheNode = '" + Dns.GetHostName() + "'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("OrderNo");
                        OrderNo = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                        col = reader.GetOrdinal("Host");
                        Host = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                        col = reader.GetOrdinal("State");
                        State = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                        col = reader.GetOrdinal("StateCode");
                        StateCode = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("id_processing");
                        id_processing = !reader.IsDBNull(col) ? reader.GetInt64(col) : 0;

                        ords.Add(new ordToUpdRealtime(OrderNo, Host, State, StateCode, id_processing));
                    }
                    reader.Close();


                    if (ords.Count > 0)
                    {
                        sqlQuery = "exec [dbo].[cc_realtime_Fill_cache2_Confirm_Order] @cacheNode = '" + Dns.GetHostName()
                                          + "' , @id_processing=" + ords.Select(or => or.id_processing).Max();
                        using (SqlCommand command2 = new SqlCommand(sqlQuery, connection))
                        {
                            reader = command2.ExecuteReader();

                            reader.Close();

                        }
                        connection.Close();


                        // Вызов для каждого заказ его наполнения в список Mapper-а
                        foreach (ordToUpdRealtime ord in ords)
                        {
                            findOrder((int)ord.OrderNo, ord.Host, 0, 0, 0);
                        }

                    }

                    }
            }
        }
    }
}
