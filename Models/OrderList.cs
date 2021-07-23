using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using MOweb.Models;
using System.Data;
using Newtonsoft.Json;
using AppClasses;

namespace Orders
{
    public struct GeocodeSearchResult
    {
        public string PostCode;
        public string KLADR;
        public string Address;
    }

    public struct GeocodeMapResult
    {
        public string FIASID;
        public string Address;
        public string UnrestrictedAdress;
        public string PostCode;
        public string Region;
        public string RegionType;
        public string Area;
        public string AreaType;
        public string City;
        public string CityType;
        public string Settelment;
        public string SettelmentType;
        public string Street;
        public string StreetType;
        public string House;
        public string HouseType;
        public string Block;
        public string BlockType;
        public string CityArea;
        public string CityDistrict;
        public string CityDistrictType;
        public string KLADR;
        public double Lat1;
        public double Lng1;
        public int FiasLevel;
        public string Map;
    }

    public class ItemInfo
    {
        public int OrderNo { get; set; }
        public int Host { get; set; }
        public int LineNo { get; set; }
        public string CSKU { get; set; }
        public string Variant { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ItemOpenParams { get; set; }
        public int CancelReasonId { get; set; }
        public string CancelReasonDescr { get; set; }
        public int inWare { get; set; }
        public string Url { get; set; }
        public int Changed { get; set; }
        public double Accumulative { get; set; }
        public double Coupon { get; set; }
        public double Mobile { get; set; }
        public double Other { get; set; }
        public double Payment { get; set; }
        public double Special { get; set; }

        public ItemInfo()
        {
            OrderNo = 0;
            Host = 0;
            LineNo = 0;
            CSKU = "";
            Variant = "";
            Description = "";
            Price = 0;
            Quantity = 0;
            ItemOpenParams = "";
            CancelReasonId = 0;
            CancelReasonDescr = "";
            inWare = 0;
            Url = "";
            Changed = 0;
            Accumulative = 0;
            Coupon = 0;
            Mobile = 0;
            Other = 0;
            Payment = 0;
            Special = 0;
        }
    }

    public class CustomerInfo
    {
        public int ID { get; set; }
        public int Host { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public CustomerInfo()
        {
            ID = 0;
            Host = 0;
            Name = "";
            Surname = "";
            Phone = "";
            Email = "";
        }
        /*
        public CustomerInfo(int id, string name, string surname, string phone, string email)
        {
            ID = id;
            Name = name;
            Surname = surname;
            Phone = phone;
            Email = email;
        }
        */

    }

    public class PaymentInfo
    {
        public int PaymentType { get; set; }
        public string Description { get; set; }

        public PaymentInfo()
        {
            PaymentType = 0;
            Description = "";
        }

        public PaymentInfo(int ptype, string descr)
        {
            PaymentType = ptype;
            Description = descr;
        }
    }

    public class ShippingInfo
    {
        public int ShippingService { get; set; }
        public string Description { get; set; }
        public double ShippingAmount { get; set; }
        public DateTime ShippingDateFrom { get; set; }
        public DateTime ShippingDateTo { get; set; }

        public ShippingInfo()
        {
            ShippingService = 0;
            Description = "";
            ShippingAmount = 0;
            ShippingDateFrom = default;
            ShippingDateTo = default;
        }

        public ShippingInfo(int shipserv, string descr, double shipamount, DateTime datefrom, DateTime dateto)
        {
            ShippingService = shipserv;
            Description = descr;
            ShippingAmount = shipamount;
            ShippingDateFrom = datefrom;
            ShippingDateTo = dateto;
        }
    }

    public class DeliveryInfo
    {
        public string DeliverySearchAddr { get; set; }
        public string DeliveryName { get; set; }
        public string DeliverySurname { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryPostCode { get; set; }
        public string DeliveryKLADR { get; set; }
        public string DeliveryRegion { get; set; }
        public string DeliveryRaion { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryPunkt { get; set; }
        public string DeliveryStreet { get; set; }
        public string DeliveryHouse { get; set; }
        public string DeliveryCorps { get; set; }
        public string DeliveryApartment { get; set; }
        public string DeliveryEntrance { get; set; }
        public string DeliveryIntercom { get; set; }
        public string DeliveryFloor { get; set; }
        public bool DeliveryIsOffice { get; set; }
        public string DeliveryHub { get; set; }
        //public string exec_InsertDeliveryAddr { get; set; }
        public int DeliveryId { get; set; }
        public int DeliveryAddrId { get; set; }
        public Int64 DeliveryAddrRouteId { get; set; }
        public Int64 DeliveryAddrDelivId { get; set; }



        public DeliveryInfo()
        {
            DeliverySearchAddr = "";
            DeliveryName = "";
            DeliverySurname = "";
            DeliveryPhone = "";
            DeliveryPostCode = "";
            DeliveryKLADR = "";
            DeliveryRegion = "";
            DeliveryRaion = "";
            DeliveryCity = "";
            DeliveryPunkt = "";
            DeliveryStreet = "";
            DeliveryHouse = "";
            DeliveryCorps = "";
            DeliveryApartment = "";
            DeliveryEntrance = "";
            DeliveryIntercom = "";
            DeliveryFloor = "";
            DeliveryIsOffice = false;
            DeliveryHub = "";
            //exec_InsertDeliveryAddr = "";
            DeliveryId = 0;
            DeliveryAddrId = 0;
            DeliveryAddrRouteId = 0;
            DeliveryAddrDelivId = 0;
        }

        public static List<GeocodeSearchResult> GetListGeocodeSearchResult(string searchAddr)
        /*Получение списка найденных адресов по строке адреса*/
        {
            List<GeocodeSearchResult> listGeocodeSearchResult = new List<GeocodeSearchResult>() { };
            try
            {
                using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO)) // = new SqlConnection(OrdersMapper.SqlConnectMO)  asa 27.07.2019
                {
                    connection.Open();
                    string QueryOrders = String.Format($"exec MO..KLADR_SearchNEWFias @AddrStr='{searchAddr}'");
                    using (SqlCommand command = new SqlCommand(QueryOrders, connection))
                    {
                        int col;
                        GeocodeSearchResult searchResult;
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            col = reader.GetOrdinal("txt - Индекс");
                            searchResult.PostCode = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("txt - KLADR");
                            searchResult.KLADR = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("txt - Адрес");
                            searchResult.Address = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            listGeocodeSearchResult.Add(searchResult);
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
                return listGeocodeSearchResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static string InsertDeliveryAddr(string PostalCode, string Region, string RegionType, string Area, string AreaType, string City,
                                                string CityType, string Settelment, string SettelmentType, string Street, string StreetType,
                                                string House, string HouseType, string Block, string BlockType, string Lat, string Lon, int Author,
                                                int User, string FIAS, string KLADR, string CityArea, string CityDistrict, string CityDistrictType,
                                                int FIASLevel, string Flat, string Floor, string Entrance, string Intercom, string Name, string Surname,
                                                string Email, string Phone)
        /*Добавление найденного адреса
         * Важными возвращаемыми значениями является сам адрес и RouteAddrId и DeliverAddrId
        */
        {
            Lat = Lat.Replace(" ", "").Replace(",", ".");
            Lon = Lon.Replace(" ", "").Replace(",", ".");
            string strQuery = String.Format($"EXEC [dbo].[InsertDeliveryAddr] "
                                            + $"@PostalCode='{PostalCode}', "
                                            + $"@Region='{Region}', "
                                            + $"@RegionType='{RegionType}', "
                                            + $"@Area='{Area}', "
                                            + $"@AreaType='{AreaType}', "
                                            + $"@City='{City}', "
                                            + $"@CityType='{CityType}', "
                                            + $"@Settelment='{Settelment}', "
                                            + $"@SettelmentType='{SettelmentType}', "
                                            + $"@Street='{Street}', "
                                            + $"@StreetType='{StreetType}', "
                                            + $"@House='{House}', "
                                            + $"@HouseType='{HouseType}', "
                                            + $"@Block='{Block}', "
                                            + $"@BlockType='{BlockType}', "
                                            + $"@Lat={Lat}, "
                                            + $"@Lon={Lon}, "
                                            + $"@Author={Author}, "
                                            + $"@User={User}, "
                                            + $"@FIAS='{FIAS}', "
                                            + $"@KLADR='{KLADR}', "
                                            + $"@CityArea='{CityArea}', "
                                            + $"@CityDistrict='{CityDistrict}', "
                                            + $"@CityDistrictType='{CityDistrictType}', "
                                            + $"@FIASLevel={FIASLevel}, "
                                            + $"@Flat='{Flat}', "
                                            + $"@Floor='{Floor}', "
                                            + $"@Entrance='{Entrance}', "
                                            + $"@Intercom='{Intercom}', "
                                            + $"@Name='{Name}', "
                                            + $"@Surname='{Surname}', "
                                            + $"@Email='{Email}', "
                                            + $"@Phone='{Phone}'");
            DataSet InsertDeliveryAddrDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(strQuery, connection);
                adapter.Fill(InsertDeliveryAddrDataSet, "InsertDeliveryAddr");
                connection.Close();
            }
            return JsonConvert.SerializeObject(InsertDeliveryAddrDataSet);
        }

        public static GeocodeMapResult ShowMap(string procParams, int scale, int currentUser)
        {
            GeocodeMapResult geocodeMapResult = new GeocodeMapResult();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                int col = 0;
                connection.Open();
                string sqlQuery = "exec KLADR_SearchNEWConcretFias '" + procParams + "'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        col = reader.GetOrdinal("id");
                        geocodeMapResult.FIASID = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("Address");
                        geocodeMapResult.Address = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("PostalCode");
                        geocodeMapResult.PostCode = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("Region");
                        geocodeMapResult.Region = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("RegionType");
                        geocodeMapResult.RegionType = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("Area");
                        geocodeMapResult.Area = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("AreaType");
                        geocodeMapResult.AreaType = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("City");
                        geocodeMapResult.City = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("CityType");
                        geocodeMapResult.CityType = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("Settelment");
                        geocodeMapResult.Settelment = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("SettelmentType");
                        geocodeMapResult.SettelmentType = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("Street");
                        geocodeMapResult.Street = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("StreetType");
                        geocodeMapResult.StreetType = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("House");
                        geocodeMapResult.House = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("HouseType");
                        geocodeMapResult.HouseType = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("Block");
                        geocodeMapResult.Block = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("BlockType");
                        geocodeMapResult.BlockType = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("CityArea");
                        geocodeMapResult.CityArea = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("CityDistrict");
                        geocodeMapResult.CityDistrict = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("CityDistrictType");
                        geocodeMapResult.CityDistrictType = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("KLADR");
                        geocodeMapResult.KLADR = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("Lat");
                        geocodeMapResult.Lat1 = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                        col = reader.GetOrdinal("Lon");
                        geocodeMapResult.Lng1 = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                        col = reader.GetOrdinal("FIASLevel");
                        geocodeMapResult.FiasLevel = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                    }
                    reader.Close();
                }
                connection.Close();
                geocodeMapResult.Map = GetMap(3, currentUser, geocodeMapResult.Lat1.ToString().Replace(",", "."), geocodeMapResult.Lng1.ToString().Replace(",", "."),
                                        650, 450, 1, scale);
            }
            return geocodeMapResult;
        }

        public static string GetMap(int author, int user, string lat, string lng, int width, int heigth, int marker, int scale)
        /* Получение картинки карты по координатам*/
        {
            string sqlQuery = String.Format($"exec MO..cc_getMap @Author={author}, @User={user}, @Lat={lat}, @Lng={lng}, @Width={width}, @Heigth={heigth}, " +
                                            $"@Marker={marker}, @Scale={scale}");
            byte[] mapResult = null;
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("Map");
                        mapResult = !(reader[col].GetType().Name == "DBNull") ? (byte[])reader[col] : null;
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return String.Format($"data:image/png;base64,{Convert.ToBase64String(mapResult)}");
        }

        public static string GetMapFromAddrId(int author, int user, int addrId, int width, int heigth, int marker, int scale)
        /* Получение картинки карты по координатам*/
        {
            string sqlQuery = String.Format($"exec MO..cc_getMapFromAddrId @Author={author}, @User={user}, @AddrId = {addrId}, @Width={width}, @Heigth={heigth}, " +
                                            $"@Marker={marker}, @Scale={scale}");
            byte[] mapResult = null;
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("Map");
                        mapResult = !(reader[col].GetType().Name == "DBNull") ? (byte[])reader[col] : null;
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return String.Format($"data:image/png;base64,{Convert.ToBase64String(mapResult)}");
        }

        public static string Geocode_FindAddr_rai(string searchAddr, string gmode)
        {
            try
            {
                DataSet addrSet = new DataSet();
                using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
                {
                    connection.Open();
                    SqlDataAdapter custAdapter = new SqlDataAdapter(String.Format($"EXEC MO.[dbo].[Geocode_FindAddr_rai] @SearchAddr='{searchAddr}', " +
                                                                                    $"@GCMode='{gmode}'"), connection);
                    custAdapter.SelectCommand.CommandTimeout = 300;
                    custAdapter.Fill(addrSet, "Geocode_FindAddr_rai");
                    connection.Close();
                }
                string table = "";
                if (addrSet.Tables.Count > 0)
                {
                    table = "<table id=\"table_Geocode_FindAddr_rai\" class=\"table table-striped table-bordered table-hover table-condensed table-select\">";
                    for (int i = 0; i < addrSet.Tables["Geocode_FindAddr_rai"].Rows.Count; i++)
                    {
                        table += String.Format($"<tr onclick=\"shippingSelectFromAddrList(this, " +
                                                $"{addrSet.Tables["Geocode_FindAddr_rai"].Rows[i][1].ToString().Replace(",", ".")}, " +
                                                $"{addrSet.Tables["Geocode_FindAddr_rai"].Rows[i][2].ToString().Replace(",", ".")})\">" +
                                                $"<td>{addrSet.Tables["Geocode_FindAddr_rai"].Rows[i][0]}</td>" +
                                                $"<td hidden>{addrSet.Tables["Geocode_FindAddr_rai"].Rows[i][1]}</td>" +
                                                $"<td hidden>{addrSet.Tables["Geocode_FindAddr_rai"].Rows[i][2]}</td>" +
                                                $"<td>{addrSet.Tables["Geocode_FindAddr_rai"].Rows[i][3]}</td></tr>");
                    }
                    table += "</table>";
                }
                return table;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "";
            }
        }

        public static int Get_map_AddressItemSave(string addrSearch, string addrFound, string lat, string lng, int currentUser)
        {
            string sqlQuery = String.Format($"EXEC [dbo].[map_AddressItemSave] @AddrSearch='{addrSearch}'" +
                                            $", @AddrFound='{addrFound}', @Lat={lat}, @Lng={lng}" +
                                            $", @Author=3, @User={currentUser}");
            int addrId = 0;
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("Result");
                        addrId = !(reader[col].GetType().Name == "DBNull") ? reader.GetInt32(col) : 0;
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return addrId;
        }



    }

    public class CustomerOrders
    {
        public string Currency { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        //public int Opened { get; set; }
        //public int InWork { get; set; }
        //public int Closed { get; set; }
        public int CurrentUser { get; set; }
        public int NeedOrder { get; set; }
        public CustomerInfo Customer { get; set; }
        public List<OrderInfo> Orders { get; set; }
        public CustomerOrders()
        {
            Currency = "";
            DateFrom = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-hh");
            DateTo = DateTime.Today.ToString("yyyy-MM-hh");
            //Opened = 1;
            //InWork = 1;
            //Closed = 1;
            CurrentUser = 0;
            NeedOrder = 0;
            Customer = new CustomerInfo();
            Orders = new List<OrderInfo> { };
        }

        public static CustomerOrders BeginWork(int Customer, int Host, string DateFrom, string DateTo, int CurrentUser, int NeedOrder)
        {
            string sNeedOrder = NeedOrder > 0 ? NeedOrder.ToString() : "null";
            string QueryOrder = String.Format($"exec MO.[dbo].[cc_Work_Begin] @Author=3, @User={CurrentUser}, @Customer={Customer}, @Host={Host}," +
                                                    $"@dat1='{DateFrom.Replace("-", "")}', @dat2='{DateTo.Replace("-", "")}'," +
                                                    $"@NeedLog=0, @NeedOrder={sNeedOrder}, @NeedArticul=null");
            //DataSet customerData = new DataSet();
            //string sdp = "";
            //using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            //{
            //    connection.Open();
            //    SqlDataAdapter custAdapter = new SqlDataAdapter(QueryOrder, connection);
            //    custAdapter.SelectCommand.CommandTimeout = 300;
            //    custAdapter.Fill(customerData, "CustomerData");
            //    connection.Close();
            //}
            //sdp = JsonConvert.SerializeObject(customerData);

            CustomerOrders co = new CustomerOrders();
            co.Customer.ID = Customer;
            co.Customer.Host = Host;
            co.DateFrom = DateFrom;
            co.DateTo = DateTo;
            co.NeedOrder = NeedOrder;
            co.CurrentUser = CurrentUser;
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryOrder, connection))
                {
                    int col;
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            col = reader.GetOrdinal("Currency");
                            co.Currency = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        }
                        if (reader.NextResult())
                        {
                            while (reader.Read()) { }
                        }
                        if (reader.NextResult())
                        {
                            while (reader.Read()) { }
                        }
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                string s = reader[0].ToString();
                            }
                        }
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                OrderInfo oi = new OrderInfo();
                                col = reader.GetOrdinal("int - Номер");
                                oi.OrderNo = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                                col = reader.GetOrdinal("int,hidden - Хост");
                                oi.Host = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                                col = reader.GetOrdinal("txt - Адрес");
                                oi.Address = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                                col = reader.GetOrdinal("txt - Тип заказа");
                                oi.OrderType = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                                col = reader.GetOrdinal("dat - Дата заказа");
                                oi.OrderDate = !reader.IsDBNull(col) ? reader.GetDateTime(col).ToString("yyyy.MM.dd HH:mm") : "";
                                col = reader.GetOrdinal("txt - Акция");
                                oi.Campaign = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                                col = reader.GetOrdinal("txt - Статус");
                                oi.State = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                                col = reader.GetOrdinal("dat - Дата изменения статуса");
                                oi.DateStateChange = !reader.IsDBNull(col) ? reader.GetDateTime(col).ToString("yyyy.MM.dd HH:mm:ss") : "";
                                col = reader.GetOrdinal("money - Сумма заказа");
                                oi.OrderAmount = !reader.IsDBNull(col) ? reader.GetDouble(col).ToString() : "";
                                col = reader.GetOrdinal("money - Стоимость доставки");
                                oi.ShippingAmount = !reader.IsDBNull(col) ? reader.GetDouble(col).ToString() : "";
                                col = reader.GetOrdinal("hidden - CanChange");
                                oi.CanChange = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                                col = reader.GetOrdinal("txt,hidden - NextForm");
                                oi.NextForm = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                                co.Orders.Add(oi);
                            }
                        }
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return co;
        }
    }
    public class Order
    {
        public int CurrentUser { get; set; }
        public string Logo { get; set; }
        public int OrderNo { get; set; }
        public int Host { get; set; }
        public string Currency { get; set; }
        public int OrderType { get; set; }
        public int OutLine { get; set; }
        public int State { get; set; }
        public string StateDescr { get; set; }
        public string OrderDate { get; set; }
        public double OrderAmount { get; set; }
        public double CashlessAmount { get; set; }
        public string EstimatedDeliveryDate { get; set; }
        public string Campaign { get; set; }
        public string Driver { get; set; }
        public string CarNum { get; set; }
        public int CountItems { get; set; }
        public string ListPriceAgent { get; set; }
        public string CancelReasons { get; set; }
        public string InDispatchReasons { get; set; }
        public string PartialCancelReasons { get; set; }
        public string History { get; set; }
        public string OpenWithParams { get; set; }
        public string RobotComment { get; set; }
        public string CustomerComment { get; set; }
        public string EmplComment { get; set; }
        public string ForOperComment { get; set; }
        public int PassDataCorrect { get; set; }
        public int NeedCheckPassData { get; set; }
        public string OrderList { get; set; }
        public string OrderForm { get; set; }
        public int Important { get; set; }
        public CustomerInfo Customer { get; set; }
        public PaymentInfo Payment { get; set; }
        public ShippingInfo Shipping { get; set; }
        public DeliveryInfo Delivery { get; set; }
        public List<ItemInfo> OrderItems { get; set; }

        public Order()
        {
            CurrentUser = 0;
            Logo = "";
            OrderNo = 0;
            Host = 0;
            Currency = "";
            OrderType = 0;
            OutLine = 0;
            State = 0;
            StateDescr = "";
            OrderDate = "";
            EstimatedDeliveryDate = "";
            OrderAmount = 0;
            CashlessAmount = 0;
            Campaign = "";
            Driver = "";
            CarNum = "";
            CountItems = 0;
            ListPriceAgent = "";
            CancelReasons = "";
            InDispatchReasons = "";
            PartialCancelReasons = "";
            History = "";
            OpenWithParams = "";
            RobotComment = "";
            CustomerComment = "";
            EmplComment = "";
            ForOperComment = "";
            PassDataCorrect = 0;
            NeedCheckPassData = 0;
            OrderList = "";
            OrderForm = "";
            Important = 0;
            Customer = new CustomerInfo();
            Payment = new PaymentInfo();
            Shipping = new ShippingInfo();
            Delivery = new DeliveryInfo();
            OrderItems = new List<ItemInfo> { };// { new ItemInfo() };

        }

        public Order(int orderno, int host)
        {
            Logo = "";
            OrderNo = orderno;
            Host = host;
            OrderType = 0;
            OutLine = 0;
            State = 0;
            StateDescr = "";
            OrderDate = "";
            EstimatedDeliveryDate = "";
            OrderAmount = 0;
            CashlessAmount = 0;
            Campaign = "";
            Driver = "";
            CarNum = "";
            CountItems = 0;
            ListPriceAgent = "";
            CancelReasons = "";
            InDispatchReasons = "";
            History = "";
            OpenWithParams = "";
            Customer = new CustomerInfo();
            Payment = new PaymentInfo();
            Shipping = new ShippingInfo();
            Delivery = new DeliveryInfo();
            OrderItems = new List<ItemInfo> { };// { new ItemInfo() };
        }


        public DataSet DoQuery()
        {
            DataSet customerDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter("SELECT top 100 * FROM MO.dbo.cc_ref_Customer (nolock)", connection);
                custAdapter.Fill(customerDataSet, "Customers");
                connection.Close();
            }
            return customerDataSet;
        }

        public static string GetGCMode()
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                string sqlQuery = "select Value from MO..cc_system (nolock) where Name = 'GeoCodeServices'";
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("Value");
                        result = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return result;
        }

        public static DataSet GetHistory(int orderNo, int host, string histParam)
        {
            try
            {
                DataSet historyDataSet = new DataSet();
                using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
                {
                    connection.Open();
                    SqlDataAdapter custAdapter = new SqlDataAdapter(String.Format($"select * from MO.dbo.cc_Order_Get_HistoryNEW ({orderNo}, {host}, '{histParam}')"), connection);
                    custAdapter.Fill(historyDataSet, "History");
                    connection.Close();
                }
                return historyDataSet;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static string GetMP3(int callId)
        {
            byte[] result = null;
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                string sqlQuery = String.Format($"EXEC [dbo].[cc_CallOCall_Get_Call_MP3] {callId}");
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("mp3");
                        result = (byte[])reader[col];
                    }
                    reader.Close();
                }
                connection.Close();
            }
            if (result != null) return String.Format($"data:audio/mp3;base64,{Convert.ToBase64String(result)}");
            else return "";
        }

        public static DataSet GetChangeAddressHistory(int orderNo, int host)
        {
            DataSet historyDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(String.Format($"exec MO..cc_ListAddrOnOrder {orderNo}, {host}"), connection);
                custAdapter.Fill(historyDataSet, "ChangeAddressHistory");
                connection.Close();
            }
            return historyDataSet;
        }

        public static DataSet GetRingsOnOrder(int orderNo, int host)
        {
            DataSet historyDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(String.Format($"exec MO..cc_GetRingsOnOrder {orderNo}, {host}"), connection);
                custAdapter.Fill(historyDataSet, "RingsOnOrder");
                connection.Close();
            }
            return historyDataSet;
        }

        public static DataSet GetListPickPointHistory(int orderNo, int host)
        {
            DataSet pickPointHistoryDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(String.Format($"exec MO..cc_Order_ListPVZHistory {orderNo}, {host}"), connection);
                custAdapter.Fill(pickPointHistoryDataSet, "PickPointHistory");
                connection.Close();
            }
            return pickPointHistoryDataSet;
        }

        public static DataSet OutletGetList(int currentUser, int orderNo, int host, int shipService)
        {
            DataSet pickPointListDataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(String.Format($"exec MO..cc_OutLet_GetList @Author=3, @User={currentUser}, " +
                                                                              $"@OrderNo={orderNo}, @Host={host}, @ShipService={shipService}"), connection);
                custAdapter.Fill(pickPointListDataSet, "OutletList");
                connection.Close();
            }
            return pickPointListDataSet;
        }

        public static string GetPickPointList(string kladr, int shipservice)
        {
            DataSet pickPointLost = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(String.Format($"exec MO..map_DistanceToPickPontByKLADR '{kladr}', {shipservice}"), connection);
                custAdapter.Fill(pickPointLost, "PickPointList");
                connection.Close();
            }
            return JsonConvert.SerializeObject(pickPointLost);
        }

        /*допилить*/
        public static DataSet GetShippingIntervals(int addrId, int agentService, int orderType, int orderNo, int host, int week, int currentUser)
        {
            int daysStart = 7 * week;
            DataSet shippingIntervals = new DataSet();
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(String.Format($"exec MO..[map_AddridAreaDescription_NEW] 3, {currentUser}, {addrId}, {agentService}, {orderType}, {orderNo}, {host}, {daysStart}"), connection);
                //SqlDataAdapter custAdapter = new SqlDataAdapter(String.Format($"exec MO..[map_AddridAreaDescription_NEWNEW] 3, {currentUser}, {addrId}, {agentService}, {orderType}, {orderNo}, {host}, {daysStart}"), connection);
                custAdapter.Fill(shippingIntervals, "ShippingIntervals");
                connection.Close();
            }
            return shippingIntervals;
        }

        public static int CanForwardInPVZ(int OrderNo, int Host, int User)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                string sqlQuery = String.Format($"exec MO..cc_OrderShipping_CanForwardInPVZ @OrderNo={OrderNo}, @Host={Host}, @User={User}");
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("Can");
                        result = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return result;
        }

        public static int CheckAgent(int orderNo, int host, string kladr, int shipAgent)
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                string sqlQuery = String.Format($"exec MO..cc_CheckShipAgentService {orderNo}, {host}, '{kladr}', {shipAgent}");
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("CountMark");
                        result = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return result;
        }

        public static string GetPriceAndDescriptionOnAgent(int orderNo, int host, int shipService, string postamatId)
        {
            string aDescr = "";
            double aPrice = 0.0;
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                string sqlQuery = String.Format($"exec MO..cc_GetPriceAndDescriptionOnAgent {orderNo}, {host}, {shipService}, '{postamatId}'");
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("AgentDescr");
                        aDescr = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("Price");
                        aPrice = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                    }
                    reader.Close();
                }
                connection.Close();
            }
            string sdp = "";
            DataSet ds = new DataSet();
            object[] values = new object[2];
            values.SetValue(aDescr, 0);
            values.SetValue(aPrice, 1);
            ds.Tables.Add("ShipDescrPrice");
            ds.Tables["ShipDescrPrice"].Columns.Add("AgentDescr", typeof(string));
            ds.Tables["ShipDescrPrice"].Columns.Add("Price", typeof(double));
            ds.Tables["ShipDescrPrice"].Rows.Add(values);
            sdp = JsonConvert.SerializeObject(ds);
            return sdp;
        }

        public static string ShowAddressOnMap(int addrId)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                string sqlQuery = String.Format($"exec MO..map_GetURLByAddrId {addrId}");
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("url");
                        result = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return result;
        }

        public static string[] GetOrderdeliveryZone(int orderNo, int host)
        {
            string[] result = new string[2];
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                string sqlQuery = String.Format($"exec MO..cc_GetZone {orderNo}, {host}");
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("Res");
                        result[0] = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                        col = reader.GetOrdinal("Res1");
                        result[1] = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return result; ;
        }

        public static string GetEstimatedDeliveryDate(int orderNo, int host)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                string sqlQuery = String.Format($"exec MO..cc_EstimatedDeliveryDate {orderNo}, {host}");
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("html");
                        result = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return result; ;
        }

        public static string GetPersonalDiscount(int orderNo, int host, int customer)
        {
            string result = "";
            string param = String.Format($"<data customer=\"{customer}\" orderno=\"{orderNo}\" orderhost=\"{host}\"/>");
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                string sqlQuery = String.Format($"exec MO..lp2_DisplayData '{param}'");
                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    int col = 0;
                    while (reader.Read())
                    {
                        col = reader.GetOrdinal("htmlbody");
                        result = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return result;
        }

        public static string CustomerGetData(int customerId, int host, int currentUser)
        {
            DataSet customerData = new DataSet();
            string sdp = "";
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(String.Format($"exec MO..[cc_Customer_GetData] @Customer={customerId}, @Host={host}, @Author=3, @User={currentUser} "), connection);
                custAdapter.Fill(customerData, "CustomerData");
                connection.Close();
            }
            sdp = JsonConvert.SerializeObject(customerData);
            return sdp;
        }

        public static string CustomerPassportVerification(int customerId, int host)
        {
            DataSet customerData = new DataSet();
            string sdp = "";
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                SqlDataAdapter custAdapter = new SqlDataAdapter(String.Format($"exec MO..[IDX_last_PD_INN] @Customer={customerId}, @Host={host}"), connection);
                custAdapter.Fill(customerData, "CustomerPassportVerification");
                connection.Close();
            }
            sdp = JsonConvert.SerializeObject(customerData);
            return sdp;
        }

        public void FillOrder(int orderno, int host, Order order)
        {
            order.OrderNo = orderno;
            order.Host = host;
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                string QueryOrder = String.Format($"exec MO..[cc_MOWEB_Order_GetData] @OrderNo={orderno}, @Host={host}");

                connection.Open();
                using (SqlCommand command = new SqlCommand(QueryOrder, connection))
                {
                    int col;
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        /*заполняем поля заказа*/
                        DataSet openWithParams = new DataSet();
                        while (reader.Read())
                        {
                            col = reader.GetOrdinal("Logo");
                            order.Logo = !reader.IsDBNull(col) ? String.Format($"data:image/png;base64,{Convert.ToBase64String((byte[])reader[col])}") : "";
                            col = reader.GetOrdinal("OrderNo");
                            order.OrderNo = reader.GetInt32(col);
                            col = reader.GetOrdinal("Host");
                            order.Host = reader.GetInt32(col);
                            col = reader.GetOrdinal("Currency");
                            order.Currency = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("OrderType");
                            order.OrderType = reader.GetInt32(col);
                            col = reader.GetOrdinal("OutLine");
                            order.OutLine = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                            col = reader.GetOrdinal("OrderDate");
                            order.OrderDate = reader.GetString(col);
                            col = reader.GetOrdinal("StateId");
                            order.State = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                            col = reader.GetOrdinal("StateDescr");
                            order.StateDescr = !reader.IsDBNull(col) ? reader.GetString(col) : "НЕТ ЗАПИСИ В ТАБЛИЦЕ СТАТУСОВ !!!";
                            col = reader.GetOrdinal("Driver");
                            order.Driver = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("CarNum");
                            order.CarNum = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("CountItems");
                            order.CountItems = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;

                            col = reader.GetOrdinal("EstimatedDeliveryDate");
                            order.EstimatedDeliveryDate = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("Campaign");
                            order.Campaign = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("OrderAmount");
                            order.OrderAmount = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                            col = reader.GetOrdinal("CashlessAmountSumm");
                            order.CashlessAmount = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                            col = reader.GetOrdinal("RobotComment");
                            order.RobotComment = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("CustomerComment");
                            order.CustomerComment = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("EmplComment");
                            order.EmplComment = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("ForOperComment");
                            order.ForOperComment = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("PassDataCorrect");
                            order.PassDataCorrect = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                            col = reader.GetOrdinal("NeedCheckPassData");
                            order.NeedCheckPassData = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                            col = reader.GetOrdinal("Important");
                            order.Important = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;

                            col = reader.GetOrdinal("Customer");
                            order.Customer.ID = reader.GetInt32(col);
                            col = reader.GetOrdinal("FirstName");
                            order.Customer.Name = reader.GetString(col);
                            col = reader.GetOrdinal("LastName");
                            order.Customer.Surname = reader.GetString(col);
                            col = reader.GetOrdinal("CallOCallMobile");
                            order.Customer.Phone = reader.GetString(col);
                            col = reader.GetOrdinal("email");
                            order.Customer.Email = reader.GetString(col);

                            col = reader.GetOrdinal("PaymentType");
                            order.Payment.PaymentType = reader.GetInt32(col);
                            col = reader.GetOrdinal("Description");
                            order.Payment.Description = reader.GetString(col);

                            col = reader.GetOrdinal("ShipAgentService");
                            order.Shipping.ShippingService = reader.GetInt32(col);
                            col = reader.GetOrdinal("shipAgentDescr");
                            order.Shipping.Description = reader.GetString(col);
                            col = reader.GetOrdinal("ShippingAmount");
                            order.Shipping.ShippingAmount = reader.GetDouble(col);
                            col = reader.GetOrdinal("ShippingDateFrom");
                            if (!reader.IsDBNull(col))
                                order.Shipping.ShippingDateFrom = reader.GetDateTime(col);
                            else
                                order.Shipping.ShippingDateFrom = default;
                            col = reader.GetOrdinal("ShippingDateTo");
                            if (!reader.IsDBNull(col))
                                order.Shipping.ShippingDateTo = reader.GetDateTime(col);
                            else
                                order.Shipping.ShippingDateTo = default;

                            col = reader.GetOrdinal("Name");
                            order.Delivery.DeliveryName = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("Surname");
                            order.Delivery.DeliverySurname = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("CallOCallPhone");
                            order.Delivery.DeliveryPhone = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("PostCode");
                            order.Delivery.DeliveryPostCode = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("KLADR");
                            order.Delivery.DeliveryKLADR = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("Region");
                            order.Delivery.DeliveryRegion = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("Raion");
                            order.Delivery.DeliveryRaion = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("City");
                            order.Delivery.DeliveryCity = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("Punkt");
                            order.Delivery.DeliveryPunkt = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("Street");
                            order.Delivery.DeliveryStreet = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("House");
                            order.Delivery.DeliveryHouse = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("Corps");
                            order.Delivery.DeliveryCorps = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("Apartment");
                            order.Delivery.DeliveryApartment = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("Entrance");
                            order.Delivery.DeliveryEntrance = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("Intercom");
                            order.Delivery.DeliveryIntercom = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("Floor");
                            order.Delivery.DeliveryFloor = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("IsOffice");
                            order.Delivery.DeliveryIsOffice = reader.GetInt32(col) > 0 ? true : false;
                            col = reader.GetOrdinal("InsideHub");
                            order.Delivery.DeliveryHub = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                            col = reader.GetOrdinal("DeliveryId");
                            order.Delivery.DeliveryId = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                            col = reader.GetOrdinal("DeliveryAddrId");
                            order.Delivery.DeliveryAddrId = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                            col = reader.GetOrdinal("DeliveryAddrRouteId");
                            order.Delivery.DeliveryAddrRouteId = !reader.IsDBNull(col) ? reader.GetInt64(col) : 0;
                            col = reader.GetOrdinal("DeliveryAddrDelivId");
                            order.Delivery.DeliveryAddrDelivId = !reader.IsDBNull(col) ? reader.GetInt64(col) : 0;
                        }

                        //order.OrderItems = new List<ItemInfo>() { };
                        if (reader.NextResult())
                        {
                            /* заполняем поля товаров */
                            while (reader.Read())
                            {
                                ItemInfo item = new ItemInfo();
                                col = reader.GetOrdinal("OrderNo");
                                item.OrderNo = reader.GetInt32(col);
                                col = reader.GetOrdinal("Host");
                                item.Host = reader.GetInt32(col);
                                col = reader.GetOrdinal("LineNo");
                                item.LineNo = reader.GetInt32(col);
                                col = reader.GetOrdinal("CscuName");
                                item.CSKU = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                                col = reader.GetOrdinal("VariantCode");
                                item.Variant = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                                col = reader.GetOrdinal("ItemName");
                                item.Description = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                                col = reader.GetOrdinal("PriceInclVat");
                                item.Price = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                                col = reader.GetOrdinal("Quantity");
                                item.Quantity = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                                col = reader.GetOrdinal("inWare");
                                //item.inWare = !reader.IsDBNull(col) ? Decimal.ToInt32(reader.GetDecimal(col)) : 0;
                                item.inWare = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                                col = reader.GetOrdinal("url");
                                item.Url = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                                col = reader.GetOrdinal("Reason");
                                item.CancelReasonId = !reader.IsDBNull(col) ? reader.GetInt32(col) : 0;
                                col = reader.GetOrdinal("ReasonDescr");
                                item.CancelReasonDescr = !reader.IsDBNull(col) ? reader.GetString(col) : "";
                                col = reader.GetOrdinal("Changed");
                                item.Changed = !reader.IsDBNull(col) ? reader.GetByte(col) : 0;
                                col = reader.GetOrdinal("Accumulative");
                                item.Accumulative = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                                col = reader.GetOrdinal("Coupon");
                                item.Coupon = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                                col = reader.GetOrdinal("Mobile");
                                item.Mobile = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                                col = reader.GetOrdinal("Other");
                                item.Other = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                                col = reader.GetOrdinal("Payment");
                                item.Payment = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                                col = reader.GetOrdinal("Special");
                                item.Special = !reader.IsDBNull(col) ? reader.GetDouble(col) : 0;
                                item.ItemOpenParams = item.Quantity.ToString() + ";" + item.CancelReasonId.ToString() + ";" + item.CancelReasonDescr;

                                order.OrderItems.Add(item);
                            }
                        }
                        // History
                        if (reader.NextResult())
                        {
                            order.History = ""; // GetStringDataSetFromReader(reader, "History");
                        }
                        // List price for agent
                        if (reader.NextResult())
                        {
                            order.ListPriceAgent = GetStringDataSetFromReader(reader, "ListPriceAgent");
                        }
                        // Cancel reasons
                        if (reader.NextResult())
                        {
                            order.CancelReasons = GetStringDataSetFromReader(reader, "CancelReasons");
                        }
                        if (reader.NextResult())
                        {
                            order.InDispatchReasons = GetStringDataSetFromReader(reader, "InDispatchReasons");
                        }
                        if (reader.NextResult())
                        {
                            order.PartialCancelReasons = GetStringDataSetFromReader(reader, "PartialCancelReasons");
                        }
                        object[] values = new object[8];
                        values.SetValue(order.OrderAmount, 0);
                        values.SetValue(order.State, 1);
                        values.SetValue(order.Shipping.ShippingService, 2);
                        values.SetValue(order.Shipping.Description, 3);
                        values.SetValue(order.Shipping.ShippingAmount, 4);
                        values.SetValue(order.Shipping.ShippingDateFrom, 5);
                        values.SetValue(order.Shipping.ShippingDateTo, 6);
                        values.SetValue(order.StateDescr, 7);
                        openWithParams.Tables.Add("OpenWithParams");
                        openWithParams.Tables["OpenWithParams"].Columns.Add("OrderAmount", typeof(double));
                        openWithParams.Tables["OpenWithParams"].Columns.Add("State", typeof(int));
                        openWithParams.Tables["OpenWithParams"].Columns.Add("ShippingService", typeof(int));
                        openWithParams.Tables["OpenWithParams"].Columns.Add("AgentDescription", typeof(string));
                        openWithParams.Tables["OpenWithParams"].Columns.Add("ShippingAmount", typeof(double));
                        openWithParams.Tables["OpenWithParams"].Columns.Add("ShippingDateFrom", typeof(DateTime));
                        openWithParams.Tables["OpenWithParams"].Columns.Add("ShippingDateTo", typeof(DateTime));
                        openWithParams.Tables["OpenWithParams"].Columns.Add("StateDescr", typeof(string));
                        openWithParams.Tables["OpenWithParams"].Rows.Add(values);
                        order.OpenWithParams = JsonConvert.SerializeObject(openWithParams);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return;
        }


        public static string GetStringDataSetFromReader(SqlDataReader reader, string dataSetName)
        {
            if (reader.HasRows)
            {
                DataSet dataSet = new DataSet();
                dataSet.Tables.Add(String.Format($"{dataSetName}"));
                int clmn = 0;
                object[] values = new object[reader.GetColumnSchema().Count];
                for (clmn = 0; clmn < reader.GetColumnSchema().Count; clmn++)
                {
                    dataSet.Tables[String.Format($"{dataSetName}")].Columns.Add(reader.GetColumnSchema()[clmn].ColumnName, reader.GetColumnSchema()[clmn].DataType);
                }
                while (reader.Read())
                {
                    for (clmn = 0; clmn < reader.GetColumnSchema().Count; clmn++)
                    {
                        values.SetValue(reader[clmn], clmn);
                    }
                    dataSet.Tables[String.Format($"{dataSetName}")].Rows.Add(values);
                }
                return JsonConvert.SerializeObject(dataSet);
            }
            else return "";
        }

        public static string GetOrderForm(int User, int Customer, int Host, int OrderNo)
        {
            string res = "";
            string resQuery = String.Format($"select MO.[dbo].[GetOrderForm](3, {User}, {Customer}, {Host}, {OrderNo})");
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(resQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        res = reader.GetString(0);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return res;
        }

        public static string GetCommentFromCustomerTable(int Customer, int Host, int TypeCard)
        {
            string res = "";
            string resQuery = String.Format($"exec MO..[cc_Call_Get_WorkDescList] @Customer={Customer}, @Host={Host}, @TypeClientCard={TypeCard}");
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(resQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        res = reader.GetString(reader.GetOrdinal("Description"));
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return res;
        }

        public static string GetFirstContact(int OrderNo, int Host)
        {
            string res = "";
            string resQuery = String.Format($"exec MO..[FC_ShowFC] @OrderNo={OrderNo}, @Host={Host}");
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(resQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        res = reader.GetString(reader.GetOrdinal("Res"));
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return res;
        }

        public static string GetOrderMessage(int User, int OrderNo, int Host, int ShipService, int TypeCard, string KLADR)
        {
            string res = "";
            string resQuery = String.Format($"exec MO..[cc_Order_GetMessage] @Author=3, @User={User}, @OrderNo={OrderNo}, @Host={Host}," +
                                            $"@ShipService={ShipService}, @TypeClientCard={TypeCard}, @KLADR='{KLADR}'");
            using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(resQuery, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        res = reader.GetString(reader.GetOrdinal("Message"));
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return res;
        }

        public static string[] GetOrderListString(int User, int Customer, int Host, int OrderNo, string NextForm)
        {
            string stringOrderList = "";
            string QueryOrder = "";
            int col = 0;
            if (String.IsNullOrEmpty(NextForm)) NextForm = GetOrderForm(User, Customer, Host, OrderNo);
            if (NextForm == "frmConfirm")
            {
                QueryOrder = String.Format($"exec MO..cc_Confirm_BeginWork @User={User}, @Customer={Customer}, @Host={Host}");
                using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(QueryOrder, connection))
                    {
                        var reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            if (reader.NextResult())
                            {
                                while (reader.Read())
                                {
                                    col = reader.GetOrdinal("OrderNo");
                                    if (!reader.IsDBNull(col))
                                        stringOrderList += reader.GetSqlInt32(col).ToString() + ",";
                                }
                            }
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }
            else if (NextForm == "frmShipping")
            {
                var strOrderNo = (OrderNo != 0) ? OrderNo.ToString() : "NULL";
                QueryOrder = String.Format($"exec MO..cc_Shipping_BeginWork @Author=3, @User={User}, @Customer={Customer}, @Host={Host}, @NeedOrder={strOrderNo}");
                using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(QueryOrder, connection))
                    {
                        var reader = command.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                col = reader.GetOrdinal("OrderNo");
                                if (!reader.IsDBNull(col))
                                    stringOrderList += reader.GetSqlInt32(col).ToString() + ",";
                            }
                        }
                        reader.Close();
                    }
                    if (stringOrderList.Length > 0)
                    {
                        QueryOrder = String.Format($"EXEC MO..cc_WriteOrderOpenLog_Mult @OrderList = '{stringOrderList}', @Host = {Host}, @Customer = {Customer}" +
                                                    $", @Author = 3, @User = {User}, @TypeClientCard = 2 ");
                        using (SqlCommand command = new SqlCommand(QueryOrder, connection))
                        {
                            var reader = command.ExecuteReader();
                            reader.Close();
                        }                        
                    }
                    connection.Close();
                }
            }
            if (String.IsNullOrEmpty(stringOrderList)) stringOrderList = OrderNo.ToString();
            if (stringOrderList.Substring(stringOrderList.Length - 1, 1) == ",")
                stringOrderList = stringOrderList.Remove(stringOrderList.Length - 1, 1);
            return new string[] { NextForm, stringOrderList };
        }

        public static void OrderDeliveryChange(Order order)
        {
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
            //using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            //{
            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand(strQuery, connection))
            //    {
            //        var reader = command.ExecuteReader();
            //        reader.Close();
            //    }
            //    connection.Close();
            //}
            return;
        }

    }

    public class OrderInfo
    {
        public int OrderNo;
        public int Host;
        public string OrderType;
        public string OrderDate;
        public string DateStateChange;
        public string State;
        public string OrderAmount;
        public string ShippingAmount;
        public string Campaign;
        public string Address;
        public string CouponAmount;
        public string PaymentAmount;
        public int ItemCount;
        public int CanChange;
        public string NextForm;

        public OrderInfo()
        {
            OrderNo = 0;
            Host = 0;
            OrderType = "";
            OrderDate = "";
            DateStateChange = "";
            State = "";
            OrderAmount = "";
            ShippingAmount = "";
            Campaign = "";
            Address = "";
            CouponAmount = "";
            PaymentAmount = "";
            ItemCount = 0;
            CanChange = 0;
            NextForm = "";
        }
    }
    /*
    public class OrderInfoList
    {
        public int Customer;
        public int Host;
        public DateTime DateFrom;
        public DateTime DateTo;
        public int ActiveOrder;
        public List<OrderInfo> ListOfOrderInfo;

        public OrderInfoList()
        {
            Customer = 0;
            Host = 0;
            DateFrom = DateTime.Today.AddMonths(-1);
            DateTo = DateTime.Today;
            ActiveOrder = 0;
            ListOfOrderInfo = null;
        }
    }
    */


    public class OrderList
    {
        //public int Customer;
        //public int Host;
        public List<Order> ListOrders;//= new List<Order>() { };

        public OrderList(int cust, int host, string strstate)
        {
            ListOrders = new List<Order>() { };
            //List<Order> ListOrders = new List<Order>() { };
            List<ItemInfo> listItems = new List<ItemInfo>() { };
            //using (SqlConnection connection = new SqlConnection(OrdersMapper.SqlConnectMO))
            //{
            //    string QueryOrders = "\n " + "declare @Customer int = 420125999																						"
            //                        + "\n " + "declare @Host int = 1																								"
            //                        + "\n " + "declare @State varchar(500) = null --'40,72,89'																				"
            //                        + "\n " + "declare @OrderList varchar(max) = null--'2002173559,'           															"
            //                        + "\n " + "select oh.OrderNo, oh.Host, oh.Customer, rc.FirstName, rc.LastName, rc.CallOCallMobile, rc.email,					"
            //                        + "\n " + "	cash.PaymentType, cash.[Description], ship.*																		"
            //                        + "\n " + "from MO..cc_OrderHead (nolock) oh																					"
            //                        + "\n " + "left join MO..cc_Ref_Customer (nolock) rc																			"
            //                        + "\n " + "	on oh.Customer = rc.id																								"
            //                        + "\n " + "		and oh.Host = rc.Host																							"
            //                        + "\n " + "outer apply																											"
            //                        + "\n " + "(																													"
            //                        + "\n " + "	select top 1 PaymentType, 																							"
            //                        + "\n " + "		(select [Description] from MO..cc_Ref_PaymentType (nolock) where id = oc.PaymentType) [Description]				"
            //                        + "\n " + "	from MO..cc_Order_Cash (nolock) oc																					"
            //                        + "\n " + "	where OrderNo = oh.OrderNO																							"
            //                        + "\n " + "		and Host = oh.Host																								"
            //                        + "\n " + "	order by id desc																									"
            //                        + "\n " + ") cash																												"
            //                        + "\n " + "outer apply																											"
            //                        + "\n " + "(																													"
            //                        + "\n " + "	select top 1 ShipAgentService,																						"
            //                        + "\n " + "		(select [Description] from MO..cc_Ref_ShippingAgent_Service (nolock) where id = os.ShipAgentService) [Descr],	"
            //                        + "\n " + "		ShippingAmount, ShippingDateFrom, ShippingDateTo																"
            //                        + "\n " + "	from MO..cc_Order_Shipping (nolock) os																				"
            //                        + "\n " + "	where OrderNo = oh.OrderNO																							"
            //                        + "\n " + "		and Host = oh.Host																								"
            //                        + "\n " + "	order by id desc																									"
            //                        + "\n " + ") ship																												"
            //                        + "\n " + "where oh.Customer = @Customer																						"
            //                        + "\n " + "	and oh.Host = @Host																									"
            //                        + "\n " + "	and (@State is null or oh.[State] in (Select ID from MO.dbo.cc_SplitStringToStrings_delimiter(@State, ',')))		"
            //                        + "\n " + "	and (@OrderList is null or oh.OrderNO in (Select ID from MO.dbo.cc_SplitStringToStrings_delimiter(@OrderList, ',')))";

            //    string QueryItems = "declare @t table (OrderNo int, Host int) \n";

            //    connection.Open();
            //    using (SqlCommand command = new SqlCommand(QueryOrders, connection))
            //    {

            //        var reader = command.ExecuteReader();
            //        while (reader.Read())
            //        {

            //            QueryItems = QueryItems + "insert into @t select " + reader["OrderNo"] + ", " + reader["Host"] + "\n";

            //            int col;

            //            col = reader.GetOrdinal("OrderNo");
            //            int orderNo = reader.GetInt32(col);
            //            col = reader.GetOrdinal("Host");
            //            int hst = reader.GetInt32(col);

            //            col = reader.GetOrdinal("ShippingDateFrom");
            //            DateTime dateFrom;
            //            if (!reader.IsDBNull(col))
            //                dateFrom = reader.GetDateTime(col);
            //            else
            //                dateFrom = default;
            //            col = reader.GetOrdinal("ShippingDateFrom");
            //            DateTime dateTo;
            //            if (!reader.IsDBNull(col))
            //                dateTo = reader.GetDateTime(col);
            //            else
            //                dateTo = default;

            //            Order order = new Order();
            //            //Order order = new Order(null, orderNo, hst, 0,
            //            //                        new CustomerInfo(reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6)),
            //            //                        new PaymentInfo(reader.GetInt32(7), reader.GetString(8)),
            //            //                        new ShippingInfo(reader.GetInt32(9), reader.GetString(10), reader.GetDouble(11), dateFrom, dateTo),
            //            //                        new DeliveryInfo("", "", "", 0, "", "", "", "", "", "", "", "", "", "", "", "", "", false),
            //            //                        new List<ItemInfo>() { });

            //            ListOrders.Add(order);
            //        }
            //        //Console.WriteLine(QueryItems);
            //        reader.Close();
            //    }

            //    QueryItems = QueryItems + "\n" + "select oi.OrderNo, oi.Host, oi.CscuName, oi.VariantCode,"
            //                            + "\n" + "	oi.ItemName, oi.PriceInclVat, oi.Quantity, '' url	  "
            //                            + "\n" + "from @t t												  "
            //                            + "\n" + "left join MO..cc_OrderItems (nolock) oi				  "
            //                            + "\n" + "	on t.OrderNo = oi.OrderNo							  "
            //                            + "\n" + "		and t.Host = oi.Host							  "
            //                            + "\n" + "order by oi.OrderNo, oi.Host, oi.[LineNo]				  ";
            //    using (SqlCommand command = new SqlCommand(QueryItems, connection))
            //    {
            //        var reader = command.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            //listItems.Add(new ItemInfo(reader.GetInt32(0), reader.GetInt32(1),
            //            //                            reader.GetString(2), reader.GetString(3), reader.GetString(4),
            //            //                            reader.GetDouble(5), reader.GetInt32(6), reader.GetString(7)));
            //            listItems.Add(new ItemInfo());
            //        }
            //        reader.Close();
            //    }
            //}


            //foreach (Order order in ListOrders)
            //{
            //    order.OrderItems = listItems.FindAll(item => item.OrderNo == order.OrderNo).FindAll(item => item.Host == order.Host);
            //}
        }
    }
}
