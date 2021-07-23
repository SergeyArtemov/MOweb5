using System;
using System.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MOweb.Infrastructure;
using MOweb.Models;
using Newtonsoft.Json;
using MOweb.Models.ViewModels;
using AppClasses;

namespace MOweb.Controllers


{

    public class SessOrder // сессия по работе с паспортом 
    {
        public int? ordid { get; set; }
        public int? host { get; set; }
        public long key { get; set; }

        public SessOrder(int? o, int? h, long k)
        {
            ordid = o;
            host = h;
            key = k;
        }

    }

    public class SessOrderList // сессия по работе со списками Заказов 
    {
        public long key { get; set; }
        public long user { get; set; }

        public long? AccountId { get; set; }

        public SessOrderList(long u, long? aid, long k)
        {
            user = u;
            key = k;
            AccountId = aid;
        }

    }

    public class OrderController : Controller
    {
        [HttpPost]
        /* Обновление модели
         * нужно для отправки всех данных со всех полей модели
         * чтобы при обновлении страницы введённые данные не пропали
         */
        public ActionResult UpdateModel(Orders.Order order)
        {
            //order.Delivery.exec_InsertDeliveryAddr = String.Format(order.Delivery.exec_InsertDeliveryAddr, order.Delivery.DeliveryApartment,
            //    order.Delivery.DeliveryFloor, order.Delivery.DeliveryEntrance, order.Delivery.DeliveryIntercom, order.Delivery.DeliveryName,
            //    order.Delivery.DeliverySurname, "", order.Delivery.DeliveryPhone);
            return View("Order", order);
        }

        [HttpPost]
        public ActionResult UpdateOrder(Orders.Order order, int NextOrder = 0, int ReloadOrder = 0)
        {
            SessOrder so;
            so = HttpContext.Session.GetJson<SessOrder>("Order");
            // Обновление модели ModelView
            OrderViews.UpdOrderInViewList(order, 0/*(int)so.key*/);  // 16.04.2021

            if (NextOrder > 0) return ShowOrderHost(NextOrder, order.Host, order.OrderList, order.OrderForm);
            else if (ReloadOrder > 0) return ShowOrders(order.Customer.ID, order.Host, order.OrderNo, order.CurrentUser, "", ReloadOrder);
            else return View("Order", order);
        }

        [HttpPost]
        // Сохраняем заказ в ModelView и в БД. А затем получаем из БД фактические данные после обновления.
        public ActionResult SaveOrder(Orders.Order order)
        {
            SessOrder so;
            so = HttpContext.Session.GetJson<SessOrder>("Order");

            // Определяем кому принадлежит текущая сессия
            int AppId = HttpContext.Session.GetJson<int>("AppId");
            int user = AppInstanceList.GetItemsById(AppId).CurrentUser.AccountId;

            // Обновление модели ModelView
            OrderViews.UpdOrderInViewList(order, 0 /*(int)so.key*/);  // 16.04.2021

            // Сбрасываем данные в БД.
            OrdersMapper.updOrder(order, /*(int)so.key, user, AppId*/0,0,0);   // 16.04.2021


            ViewBag.DiviationViewAndMapper = OrderViews.OrderView.checkDeviationOrd(order.OrderNo, order.Host, 0/* (int)so.key*/); //"ВСЕ ДАННЫЕ УСПЕШНО СОХРАНЕНЫ!!!";   // 16.04.2021 

            return View("Order", order);
        }

        [HttpPost]
        public ActionResult UpdateDelivery(Orders.Order order)
        {
            return View("Order", order);
        }

        [HttpPost]
        public string GetPriceAndDescriptionOnAgent(int orderNo, int host, int shipService, string postamatId)
        {
            return Orders.Order.GetPriceAndDescriptionOnAgent(orderNo, host, shipService, postamatId);
        }

        public ActionResult ShowCustomerOrders(int CustomerId, int Host, int OrderNo)
        {
            int cu = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;
            if (OrderNo > 0) return ShowOrderHost(OrderNo, Host);
            else
            {
                Orders.CustomerOrders co = Orders.CustomerOrders.BeginWork(CustomerId, Host,
                    DateTime.Today.AddMonths(-1).ToString("yyyy-MM-hh"), DateTime.Today.AddMonths(2500).ToString("yyyy-MM-hh"), cu, OrderNo);
                return View("CustomerOrders", co);
            }
        }

        [HttpGet]
        public ActionResult ShowOrders(int CustomerId, int Host, int OrderNo, int CurrentUser, string NextForm = "", int Reload = 0)
        {

            // Начало общей сессии по работе со связанными заказми
            int AppId = HttpContext.Session.GetJson<int>("AppId");
            int User = AppInstanceList.GetItemsById(AppId).CurrentUser.AccountId;

            // Общие сессионные данные для сессии по связанным заказам
            HttpContext.Session.SetJson("CommonKeyOfOrderList",
                                        new SessOrderList(User,
                                                            AppId,
                                                            OrdersMapper.rnd.Next())
                                        );

            SessOrderList CommonKeyOfOrderList = HttpContext.Session.GetJson<SessOrderList>("CommonKeyOfOrderList");


            string[] arr = Orders.Order.GetOrderListString(CurrentUser, CustomerId, Host, OrderNo, NextForm);
            string orderForm = arr[0];
            string strOrderList = arr[1];
            if (OrderNo == 0)
            {

                var ola = strOrderList.Split(",");
                OrderNo = (ola.Length > 0) ? Convert.ToInt32(ola[0]) : 0;
            }
            if (OrderNo != 0)
                switch (orderForm)
                {
                    case "frmConfirm":
                        return ShowOrderHost(OrderNo, Host, strOrderList, orderForm, Reload);
                    case "frmShipping":
                        return ShowOrderHost(OrderNo, Host, strOrderList, orderForm, Reload);
                    default:
                        return ShowOrderHost(OrderNo, Host, "", orderForm, Reload);
                }
            return null;
        }

        [HttpPost]
        public string CustomerGetData(int customerId, int host, int currentUser)
        {
            return Orders.Order.CustomerGetData(customerId, host, currentUser);
        }

        [HttpPost]
        public string GetCommentFromCustomerTable(int Customer, int Host, int TypeCard)
        {
            return Orders.Order.GetCommentFromCustomerTable(Customer, Host, TypeCard);
        }

        [HttpPost]
        public string CustomerPassportVerification(int customerId, int host)
        {
            return Orders.Order.CustomerPassportVerification(customerId, host);
        }

        [HttpPost]
        public ViewResult GetOrderList(int CustomerId, int Host, string DateFrom, string DateTo, int OrderNo, string Articul, int CurrenUser = 0)
        {
            int cu = (CurrenUser != 0) ? CurrenUser : AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;
            Orders.CustomerOrders co = Orders.CustomerOrders.BeginWork(CustomerId, Host, DateFrom, DateTo, cu, OrderNo);
            return View("CustomerOrdersList", co);
        }

        public ActionResult ShowOrderHost(int OrderNo, int HostId, string strOrderList = "", string NextForm = "", int ReloadOrder = 0)
        {
            //Orders.Order openedOrder = new Orders.Order();
            //openedOrder.FillOrder(OrderNo, HostId, openedOrder);

            Orders.Order openedOrder;
            OrderViews.OrderView openedOrderView;

            // Пытаемся считать из сеанса запись по идентификатору Order
            SessOrder so;
            so = HttpContext.Session.GetJson<SessOrder>("Order");

            // Пытаемся считать из сеанса данные по сессии по связанным заказам
            SessOrderList CommonKeyOfOrderList = HttpContext.Session.GetJson<SessOrderList>("CommonKeyOfOrderList");
            long rnd1 = CommonKeyOfOrderList.key;
            long user = CommonKeyOfOrderList.user;
            long? AccountId = CommonKeyOfOrderList.AccountId;

            
            if (1==0)
            {
                // Запрашиваем Заказ, сохраненные в Mapper(е) т.к. сеанс не был завершен и данные в Mapper должны были остаться.
                // При этом данные из Mapper по конкретному Заказу удаляются только при завершении сеанса.
                openedOrder = OrdersMapper.findOrderLocal(OrderNo, HostId, 0);
                //psp2 = PassportViews.findPassp(psp0.Customerid, psp0.Host, psp0.Key);

                //if (openedOrder == null)  // т.е. с данным заказом в данной сессии мы ещё не работали
                //    openedOrder = OrdersMapper.findOrder(OrderNo, HostId, so.key, user, AccountId);

                //psp0 = PassportsMapper.findPassp(sp.customerid, sp.host, sp.key);
                openedOrderView = OrderViews.OrderView.findOrd(OrderNo, HostId, 0);

                openedOrder = openedOrderView.getCloneOrd();
            }

            if (1 != 0)
            {
                if (so == null || ReloadOrder == 1)
                //if (1 == 1)
                {

                    // Получаем Заказ для редактирования из БД
                    openedOrder = OrdersMapper.findOrder(OrderNo, HostId, rnd1, user, AccountId);

                    //psp0 = OrdersMapper.findOrder(OrderNo, HostId);
                    // если  sp == null, т.е. в сеансе нет записи с идентификатором Order, то записываем в сеанс новую запись Order
                    HttpContext.Session.SetJson("Order", new SessOrder(openedOrder.OrderNo, openedOrder.Host, rnd1));

                    OrderViews.OrderView ov;
                    ov = OrderViews.ordViewList.Find(ord1 => (ord1.OrderNo == OrderNo) && (ord1.Host == HostId) && (ord1.key == rnd1));// initialOrderList.Find(ord1 => (ord1.Key == key && ord1.OrderNo == orderid && ord1.Host == host));

                    openedOrder = ov.getCloneOrd();


                    so = HttpContext.Session.GetJson<SessOrder>("Order");

                    //psp2 = PassportViews.findPassp(psp0.Customerid, psp0.Host, psp0.Key);

                }
                else
                {
                    // Запрашиваем Заказ, сохраненные в Mapper(е) т.к. сеанс не был завершен и данные в Mapper должны были остаться.
                    // При этом данные из Mapper по конкретному Заказу удаляются только при завершении сеанса.
                    openedOrder = OrdersMapper.findOrderLocal(OrderNo, HostId, so.key);
                    //psp2 = PassportViews.findPassp(psp0.Customerid, psp0.Host, psp0.Key);

                    if (openedOrder == null)  // т.е. с данным заказом в данной сессии мы ещё не работали
                        openedOrder = OrdersMapper.findOrder(OrderNo, HostId, so.key, user, AccountId);

                    //psp0 = PassportsMapper.findPassp(sp.customerid, sp.host, sp.key);
                    openedOrderView = OrderViews.OrderView.findOrd(OrderNo, HostId, so.key);

                    openedOrder = openedOrderView.getCloneOrd();


                }
            }
            // Анализируем расхождения между ViewModel (то с чем работает оператор) и DataMapper (серверные данные)
            //ViewBag.DiviationViewAndMapper = OrderViews.OrderView.checkDeviationOrd(OrderNo, HostId, (int)so.key); //"ВСЕ ДАННЫЕ УСПЕШНО СОХРАНЕНЫ!!!";

            //Orders.Order openedOrder = OrdersMapper.findOrder(OrderNo, HostId);

            //OD.FindAll(item => item.OrderNo == OrderNo).FindAll(item=>item.Host == Host);
            //return View("Order", OD.FindAll(item => item.OrderNo == OrderNo).FindAll(item => item.Host == Host)[0]);

            //string json = JsonConvert.SerializeObject(openedOrder);

            ////Orders.Order ord2 = new Orders.Order();
            //var ord2 = JsonConvert.DeserializeObject<Orders.Order>(json);

            //ViewData["History"] = Orders.Order.GetHistory(OrderNo, HostId, "");
            if (openedOrder.CurrentUser == 0)
                openedOrder.CurrentUser = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;

            if (!String.IsNullOrEmpty(strOrderList) && String.IsNullOrEmpty(openedOrder.OrderList)) openedOrder.OrderList = strOrderList;
            if (!String.IsNullOrEmpty(NextForm) && String.IsNullOrEmpty(openedOrder.OrderForm)) openedOrder.OrderForm = NextForm;
            return View("Order", openedOrder);
        }

        [HttpPost]
        public PartialViewResult GetHistory(int orderNo, int host, string histParams)
        {
            ViewData["History"] = Orders.Order.GetHistory(orderNo, host, histParams);
            return PartialView("~/Views/Order/Partial/OrderHistory.cshtml");
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
        public int CanForwardInPVZ(int OrderNo, int Host, int User)
        {
            return Orders.Order.CanForwardInPVZ(OrderNo, Host, User);
        }

        [HttpPost]
        public PartialViewResult GetListPickPointHistory(int orderNo, int host)
        {
            ViewData["PickPointHistory"] = Orders.Order.GetListPickPointHistory(orderNo, host);
            return PartialView("~/Views/Order/Partial/PickPointHistory.cshtml");
        }

        [HttpPost]
        public PartialViewResult OutletGetList(int currentUser, int orderNo, int host, int shipService)
        {
            ViewData["OutletList"] = Orders.Order.OutletGetList(currentUser, orderNo, host, shipService);
            return PartialView("~/Views/Order/Partial/OutletList.cshtml");
        }

        [HttpPost]
        public PartialViewResult GetChangeAddressHistory(int orderNo, int host)
        {
            ViewData["ChangeAddressHistory"] = Orders.Order.GetChangeAddressHistory(orderNo, host);
            return PartialView("~/Views/Order/Partial/OrderChangeAddressHistory.cshtml");
        }

        [HttpPost]
        public PartialViewResult GetRingsOnOrder(int orderNo, int host)
        {
            ViewData["RingsOnOrder"] = Orders.Order.GetRingsOnOrder(orderNo, host);
            return PartialView("~/Views/Order/Partial/RingsOnOrder.cshtml");
        }

        [HttpPost]
        public string GetMP3(int callId)
        {
            return Orders.Order.GetMP3(callId);
        }

        [HttpPost]
        public PartialViewResult ShowMap(string str, int scale, int currentUser)
        {
            ViewData["GeocodeMapResult"] = Orders.DeliveryInfo.ShowMap(str, scale, currentUser);
            return PartialView("~/Views/Order/Geocode/Map.cshtml");
        }

        [HttpPost]
        public string GetMap(int author, int user, string lat, string lng, int width, int heigth, int marker, int scale)
        {
            return Orders.DeliveryInfo.GetMap(author, user, lat, lng, width, heigth, marker, scale);
        }

        [HttpPost]
        public string GetMapFromAddrId(int author, int user, int addrId, int width, int heigth, int marker, int scale)
        {
            return Orders.DeliveryInfo.GetMapFromAddrId(author, user, addrId, width, heigth, marker, scale);
        }

        [HttpPost]
        public PartialViewResult ShippingGeocode(int author, int user, int addrId, int width, int heigth, int marker, int scale)
        {
            ViewBag.Map = GetMapFromAddrId(author, user, addrId, width, heigth, marker, scale);
            ViewBag.Scale = scale;
            ViewBag.GCMode = Orders.Order.GetGCMode();
            return PartialView("~/Views/Order/Partial/ShippingGeocode.cshtml");
        }

        [HttpPost]
        public string GetDataSet_Geocode_FindAddr_rai(string addrSearch, string gmode)
        {
            string table = Orders.DeliveryInfo.Geocode_FindAddr_rai(addrSearch, gmode);
            return table;
        }

        [HttpPost]
        public PartialViewResult ShowFindAddr(string searchAddr)
        {
            ViewBag.searchAddr = searchAddr;
            ViewBag.findedAddresses = Orders.DeliveryInfo.GetListGeocodeSearchResult(searchAddr);
            return PartialView("~/Views/Order/Geocode/MainGeocode.cshtml");
        }

        [HttpPost]
        public int Get_map_AddressItemSave(string addrSearch, string addrFound, string lat, string lng)
        {
            int cu = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;
            return Orders.DeliveryInfo.Get_map_AddressItemSave(addrSearch, addrFound, lat, lng, cu);
        }

        [HttpPost]
        public ViewResult Query(Orders.Order order)
        {
            ViewData["Customers"] = order.DoQuery();
            return View("Order", order);
        }

        [HttpPost]
        public int CheckAgent(int orderNo, int host, string kladr, int shipAgent)
        {
            return Orders.Order.CheckAgent(orderNo, host, kladr, shipAgent);
        }

        [HttpPost]
        public string ShowAddressOnMap(int addrId)
        {
            return Orders.Order.ShowAddressOnMap(addrId);
        }

        [HttpPost]
        public string[] GetOrderdeliveryZone(int orderNo, int host)
        {
            return Orders.Order.GetOrderdeliveryZone(orderNo, host);
        }

        [HttpPost]
        public string GetEstimatedDeliveryDate(int orderNo, int host)
        {
            return Orders.Order.GetEstimatedDeliveryDate(orderNo, host);
        }

        [HttpPost]
        public PartialViewResult GetShippingIntervals(int addrId, int shipService, int orderType, int orderNo, int host, int week, bool onlyShow = false)
        {
            int cu = AppClasses.AppInstanceList.GetItemsById(Convert.ToInt32(HttpContext.Session.GetJson<string>("AppId"))).CurrentUser.AccountId;
            ViewData["ShippingIntervals"] = Orders.Order.GetShippingIntervals(addrId, shipService, orderType, orderNo, host, week, cu);
            ViewBag.Week = week;
            if (onlyShow) return PartialView("~/Views/Order/Partial/ShippingIntervalsOnlyShow.cshtml");
            else return PartialView("~/Views/Order/Partial/ShippingIntervals.cshtml");
        }

        [HttpPost]
        public string InsertDeliveryAddr(string PostalCode, string Region, string RegionType, string Area, string AreaType, string City,
                                        string CityType, string Settelment, string SettelmentType, string Street, string StreetType,
                                        string House, string HouseType, string Block, string BlockType, string Lat, string Lon, int Author,
                                        int User, string FIAS, string KLADR, string CityArea, string CityDistrict, string CityDistrictType,
                                        int FIASLevel, string Flat, string Floor, string Entrance, string Intercom, string Name, string Surname,
                                        string Email, string Phone)
        {
            return Orders.DeliveryInfo.InsertDeliveryAddr(PostalCode, Region, RegionType, Area, AreaType, City,
                                                          CityType, Settelment, SettelmentType, Street, StreetType,
                                                          House, HouseType, Block, BlockType, Lat, Lon, Author,
                                                          User, FIAS, KLADR, CityArea, CityDistrict, CityDistrictType,
                                                          FIASLevel, Flat, Floor, Entrance, Intercom, Name, Surname,
                                                          Email, Phone);
        }

        [HttpPost]
        public string GetPersonalDiscount(int orderNo, int host, int customer)
        {
            return Orders.Order.GetPersonalDiscount(orderNo, host, customer);
        }

        [HttpPost]
        public PartialViewResult GetPickPointList(string kladr, int shipservice)
        {
            ViewData["PickPointList"] = Orders.Order.GetPickPointList(kladr, shipservice);
            ViewBag.ShipService = shipservice;
            return PartialView("~/Views/Order/Partial/PickPointList.cshtml");
        }

        [HttpPost]
        public string GetFirstContact(int OrderNo, int Host)
        {
            return Orders.Order.GetFirstContact(OrderNo, Host);
        }

        [HttpPost]
        public string GetOrderMessage(int User, int OrderNo, int Host, int ShipService, int TypeCard, string KLADR)
        {
            return Orders.Order.GetOrderMessage(User, OrderNo, Host, ShipService, TypeCard, KLADR);
        }

    }
}